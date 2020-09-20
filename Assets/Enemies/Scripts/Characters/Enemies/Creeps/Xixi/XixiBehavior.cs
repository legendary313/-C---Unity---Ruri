using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class XixiBehavior : CreepOnTheGround
{
    public LayerMask groundedLayerMask;
    public float groundedRaycastDistance = 0.1f;
    public float timeWaitEnemyCheckTarget = 0.5f;
    public bool spriteFaceLeft = false;

    [Header("Movement")]
    public float speed;
    public float gravity = 10.0f;


    [Header("Enemy Sight")]
    [Tooltip("tầm nhìn ")]
    [Range(0.0f, 360.0f)]
    public float viewDirection = 0.0f;
    [Range(0.0f, 360.0f)]
    public float viewFov = 360.0f;
    public float viewDistance;
    public float meleeRange = 3.0f;


    [Tooltip("chạy nhanh tới ")]
    public bool attackDash;
    [Tooltip("Lực cắn!")]
    public Vector2 attackForce;

    [Header("Way points")]
    public Transform[] wayPoints;
    protected int curWayPoint;

    [Header("Damage Area")]
    public GameObject objDamageArea;
    [Header("Create Damage")]
    public GameObject XiXiBitePrefab;

    [Header("Audio")]
    //public RandomAudioPlayer shootingAudio;
    public RandomAudioPlayer meleeAttackAudio;
    public RandomAudioPlayer dieAudio;
    public RandomAudioPlayer footStepAudio;

    [Header("Misc")]
    [Tooltip("Time in seconds during which the enemy flicker after being hit")]
    public float flickeringDuration;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected GameObject player;
    protected Vector3 moveVector3;
    protected Vector3 targetShootPosition;
    protected Vector2 spriteForward;
    protected Rigidbody2D rgd2D;

    protected GameObject xixiBite;
    protected float widthSprite;


    protected readonly int walkAnimator = Animator.StringToHash("Walk");
    protected readonly int runAnimator = Animator.StringToHash("Run");
    protected readonly int atkAnimator = Animator.StringToHash("Attack");
    protected readonly int hitAnimator = Animator.StringToHash("Hit");
    protected readonly int dieAnimator = Animator.StringToHash("Die");
    protected override void Awake()
    {
        posEnemyCurrent = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rgd2D = GetComponent<Rigidbody2D>();
        // rgd2D.gravityScale = gravity;

        widthSprite = spriteRenderer.bounds.extents.x;

        spriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
        if (spriteRenderer.flipX) spriteForward = -spriteForward;
        moveVector3 = spriteForward;
        base.Awake();
    }
    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = null;
        creep_hp_cur = enemy_hp_total;
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        if (dead)
        {
            animator.SetBool(walkAnimator, false);
            animator.SetBool(runAnimator, false);
            dieAudio.PlayRandomSound();
            animator.SetTrigger(dieAnimator);
            return;
        }
        if (hit)
        {
            return;
        }
        if (!endAttack)
        {
            return;
        }
        UpdateFacing();
        ScanForPlayer();
        if (target != null)
        {
            FollowTarget();
        }
        else
        {
            Patrol();
        }
        base.FixedUpdate();
    }
    public void UpdateFacing()
    {
        bool faceLeft = moveVector3.x < 0f;
        bool faceRight = moveVector3.x > 0f;

        if (faceLeft)
        {
            SetFacingData(-1);
        }
        else if (faceRight)
        {
            SetFacingData(1);
        }
    }
    public void SetFacingData(int facing)
    {
        if (facing == -1)
        {
            spriteRenderer.flipX = !spriteFaceLeft;
            spriteForward = spriteFaceLeft ? Vector2.right : Vector2.left;
        }
        else if (facing == 1)
        {
            spriteRenderer.flipX = spriteFaceLeft;
            spriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
        }
    }
    public void Patrol()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + new Vector2(0f, groundedRaycastDistance));
        isGrounded = Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, -groundedRaycastDistance), groundedLayerMask);

        if (isGrounded)
        {
            if (wayPoints.Length <= 0) return;
            moveVector3 = wayPoints[curWayPoint % wayPoints.Length].position - transform.position;
            if (!isWaiting)
            {
                transform.Translate(moveVector3.normalized * Time.deltaTime * speed);
                animator.SetBool(walkAnimator, true);
                animator.SetBool(runAnimator, false);
                if ((wayPoints[curWayPoint % wayPoints.Length].position - transform.position).sqrMagnitude < 2f)
                {
                    isWaiting = true;
                    StartCoroutine(WaitEnemyCheckTarget(timeWaitEnemyCheckTarget));
                }
            }
        }
    }
    private IEnumerator WaitEnemyCheckTarget(float time)
    {
        animator.SetBool(walkAnimator, false);
        yield return new WaitForSeconds(time);
        curWayPoint++;
        isWaiting = false;
    }
    private void ScanForPlayer()
    {
        // Debug.Log(player);
        if (player == null) return;
        Vector3 dir = player.transform.position - transform.position;
        if (dir.sqrMagnitude > viewDistance * viewDistance)
        {
            //Debug.Log(dir.sqrMagnitude);
            if (dir.sqrMagnitude > viewDistance * viewDistance * viewDistance)
            {
                enemyFollowPlayer = false;
            }
            target = null;
            return;
        }
        float valX = 0.0f;
        if (spriteFaceLeft)
        {
            valX = Mathf.Sign(spriteForward.x) * -viewDirection;
        }
        else
        {
            valX = Mathf.Sign(spriteForward.x) * viewDirection;
        }
        Vector3 testForward = Quaternion.Euler(0, 0, valX) * spriteForward;
        float angle = Vector3.Angle(testForward, dir);
        if (angle > viewFov * 0.5f)
        {
            target = null;
            return;
        }
        target = player.transform;
        enemyFollowPlayer = true;
    }
    private void FollowTarget()
    {
        if (target == null) return;
        if (!endAttack) return;
        moveVector3 = player.transform.position - transform.position;
        animator.SetBool(runAnimator, true);
        animator.SetBool(walkAnimator, false);
        // if (Mathf.Abs(player.transform.position.x - transform.position.x) <= meleeRange)
        // Debug.Log(Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position));
        if (Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position) <= meleeRange * 1.5f)
        {
            if (player.GetComponentInParent<PlayerController>().health > 0)
            {
                endAttack = false;
                animator.SetTrigger(atkAnimator);
                //animator.SetBool(runAnimator, false);    
            }
        }
        else
        {
            transform.Translate(moveVector3.normalized * Time.deltaTime * speed);
        }
    }
    public void OrientToTarget()
    {
        if (target == null)
            return;

        Vector3 toTarget = target.position - transform.position;

        if (Vector2.Dot(toTarget, spriteForward) < 0)
        {
            SetFacingData(Mathf.RoundToInt(-spriteForward.x));
        }
    }
    public void StartAttack()
    {
        // Debug.Log("start atk");
        if (attackDash)
            moveVector3 = new Vector2(spriteForward.x * attackForce.x, attackForce.y);
        //Debug.Log(moveVector3);
        if (spriteRenderer.flipX) objDamageArea.transform.localPosition = new Vector3(Mathf.Abs(objDamageArea.transform.localPosition.x) * -1, objDamageArea.transform.localPosition.y, objDamageArea.transform.localPosition.z);
        else objDamageArea.transform.localPosition = new Vector3(Mathf.Abs(objDamageArea.transform.localPosition.x), objDamageArea.transform.localPosition.y, objDamageArea.transform.localPosition.z);
        // objDamageArea.GetComponent<Collider2D>().enabled = true;
        //objDamageArea.SetActive(true);
        // xixiBite = Instantiate(XiXiBitePrefab, objDamageArea.transform.position, objDamageArea.transform.rotation, transform.parent);
        StartCoroutine(SpawnBite());
    }

    IEnumerator SpawnBite()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        xixiBite = Instantiate(XiXiBitePrefab, objDamageArea.transform.position, objDamageArea.transform.rotation, transform.parent);
    }

    public void EndAttack()
    {
        isWaiting = false;
        endAttack = true;
        target = null;
        Destroy(xixiBite);
        //objDamageArea.SetActive(false);
        // objDamageArea.GetComponent<Collider2D>().enabled = false;
        // Debug.Log("end attack");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hit = true;
            animator.SetTrigger(hitAnimator);
            RecieveDamage(5f); // truyền damage của bullet vào đây,
            Destroy(other.gameObject);
        }
    }
    public void EndHit()
    {
        hit = false;
    }
    public void PlayFootStep()
    {
        footStepAudio.PlayRandomSound();
    }
    public void PlayAttackAudio()
    {
        meleeAttackAudio.PlayRandomSound();
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Vector3 forward = spriteFaceLeft ? Vector2.left : Vector2.right;
        forward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * forward;

        if (GetComponent<SpriteRenderer>().flipX) forward.x = -forward.x;

        Vector3 endpoint = transform.position + (Quaternion.Euler(0, 0, viewFov * 0.5f) * forward);

        Handles.color = new Color(1.0f, 0, 0, 0.1f);
        Handles.DrawSolidArc(transform.position, -Vector3.forward, (endpoint - transform.position).normalized, viewFov, viewDistance);

        Handles.color = new Color(0, 1f, 0, 0.1f);
        Handles.DrawSolidArc(transform.position, Vector3.forward, (endpoint - transform.position).normalized, 360f - viewFov, viewDistance);

        Handles.color = new Color(1f, 1f, 0f, 0.1f);
        Handles.DrawSolidDisc(transform.position, Vector3.back, meleeRange);
    }
#endif
}
