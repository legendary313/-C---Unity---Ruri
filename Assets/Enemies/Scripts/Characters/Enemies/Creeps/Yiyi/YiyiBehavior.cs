using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class YiyiBehavior : CreepOnTheGround
{
    public LayerMask groundedLayerMask;
    public float groundedRaycastDistance = 0.1f;
    public float timeWaitEnemyCheckTarget = 0.5f;
    public bool spriteFaceLeft = false;

    [Header("Movement")]
    public float speed;
    private float gravity = 10.0f;


    [Header("Enemy Sight")]
    [Tooltip("tầm nhìn ")]
    [Range(0.0f, 360.0f)]
    public float viewDirection = 0.0f;
    [Range(0.0f, 160.0f)]
    public float viewFov = 90.0f;
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
    [Header("Bullet")]
    public GameObject prefabBullet;

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
        gravity = Mathf.Abs(Physics2D.gravity.y);
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
        if (dead && !hit)
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
        //Debug.Log(1);
        if (player == null) return;
        Vector3 dir = player.transform.position - transform.position;
        if (dir.sqrMagnitude > viewDistance * viewDistance)
        {
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
        moveVector3 = player.transform.position - transform.position;
        animator.SetBool(runAnimator, true);
        animator.SetBool(walkAnimator, false);
        if ((player.transform.position - transform.position).sqrMagnitude <= meleeRange * meleeRange)
        {
            if (player.GetComponentInParent<PlayerController>().health > 0){
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
        Debug.Log("start atk");
        // if (attackDash)
        //     moveVector3 = new Vector2(spriteForward.x * attackForce.x, attackForce.y);
        //Debug.Log(moveVector3);
        if (spriteRenderer.flipX) objDamageArea.transform.localPosition = new Vector3(Mathf.Abs(objDamageArea.transform.localPosition.x) * -1, objDamageArea.transform.localPosition.y, objDamageArea.transform.localPosition.z);
        else objDamageArea.transform.localPosition = new Vector3(Mathf.Abs(objDamageArea.transform.localPosition.x), objDamageArea.transform.localPosition.y, objDamageArea.transform.localPosition.z);
        objDamageArea.SetActive(true);
        GameObject objBullet = Instantiate(prefabBullet, objDamageArea.transform.position, Quaternion.identity, transform.parent);
        //objBullet.GetComponent<Rigidbody2D>().AddRelativeForce((player.transform.position - objDamageArea.transform.position).normalized * speed * 10f, ForceMode2D.Impulse);
        float alpha = Vector2.Angle((player.transform.position - objDamageArea.transform.position).normalized, Vector2.down);
        float heigh_bullet_player = 0.0f;
        float x_max = 0.0f;
        // float angle = viewFov / 2f + 10f;
        float angle = 45f + 10f;
        float v_max = 8f;
        if (alpha > 90)
        {
            alpha = alpha - 90;
            heigh_bullet_player = -Vector2.Distance(player.transform.position, objDamageArea.transform.position) * Mathf.Sin(alpha * Mathf.Deg2Rad);
            x_max = Vector2.Distance(player.transform.position, objDamageArea.transform.position) * Mathf.Cos(alpha * Mathf.Deg2Rad);
        }
        else
        {
            heigh_bullet_player = Vector2.Distance(player.transform.position, objDamageArea.transform.position) * Mathf.Cos(alpha * Mathf.Deg2Rad);
            x_max = Vector2.Distance(player.transform.position, objDamageArea.transform.position) * Mathf.Sin(alpha * Mathf.Deg2Rad);
        }
        //Debug.Log(heigh_bullet_player);
        Debug.DrawLine(player.transform.position, objDamageArea.transform.position, Color.red, 3f);
        //Debug.Log((gravity * x_max * x_max));
        //Debug.Log((2 * (x_max * Mathf.Tan(angle * Mathf.Deg2Rad) + heigh_bullet_player) * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Cos(angle * Mathf.Deg2Rad)));
        //Debug.Log(Mathf.Tan(angle * Mathf.Deg2Rad) * x_max + heigh_bullet_player);
        //heigh_bullet_player = -heigh_bullet_player;
        v_max = Mathf.Sqrt(Mathf.Abs((gravity * x_max * x_max) / (2 * (x_max * Mathf.Tan(angle * Mathf.Deg2Rad) + heigh_bullet_player) * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Cos(angle * Mathf.Deg2Rad))));
        objBullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * (moveVector3.x < 0 ? -1f : 1f), Mathf.Sin(angle * Mathf.Deg2Rad)) * v_max, ForceMode2D.Impulse);
        //Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, degree) * (player.transform.position - objDamageArea.transform.position).normalized);
        // Debug.Log(x_max);
        //objBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * v_max * 100f);
        //Debug.Log(dir);
        //objBullet.GetComponent<Rigidbody2D>().AddForce(dir * 100f);
    }
    public void EndAttack()
    {
        isWaiting = false;
        endAttack = true;
        target = null;
        objDamageArea.SetActive(false);
        Debug.Log("end attack");
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
