using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DragonBehavior : MonoBehaviour
{
    [Header("Sound")]
    public SFXMusic soundWalk;
    public SFXMusic soundRoar;
    public SFXMusic soundFly;
    public SFXMusic soundEarthquake;
    public SFXMusic soundGroundhitExplosion;
    public SFXMusic soundDeath;
    public SFXMusic soundFlame;
    public SFXMusic soundHurt;
    [Header("Parameter")]
    [Tooltip("Health Base")]
    public float healthBase = 100f;
    [Tooltip("Health Current")]
    public float healthCur = 0f;
    [Header("Points")]
    [Tooltip("Start point")]
    [SerializeField] Transform startPoint;
    [Tooltip("Way points")]
    [SerializeField] Transform[] wayPoints;
    [Tooltip("Fire points")]
    [SerializeField] Transform[] firePoints;
    [Tooltip("Die Point")]
    [SerializeField] Transform diePoint;
    [Header("FX")]
    [SerializeField] GameObject dieFX;
    [Header("Damage")]
    [SerializeField] GameObject dragonFire;
    [SerializeField] GameObject dragonRock;
    [Header("Camera")]
    [SerializeField] ShakeCamera shakeCam;
    [SerializeField] Camera cam;
    [SerializeField] CameraController cameraController;
    Animator animator;
    Rigidbody2D rb2D;
    SpriteRenderer sprite;
    Transform firePoint;
    Transform player;
    Vector3 rockPoint;
    float rockQuantity = 5;
    bool isWalking = false;
    int curPoint = 0;
    [HideInInspector] public bool death = false;
    bool angry = false;
    float speed = 1f;
    bool hurt = false;
    bool flipFace = false;
    bool dragonCanFight = false;
    int curBehavior = -1;
    bool startDragonRoar = false;
    public int stateDragon = 0;
    protected readonly int walkAnimator = Animator.StringToHash("Walk");
    protected readonly int flyAnimator = Animator.StringToHash("Fly");
    protected readonly int atkAnimator = Animator.StringToHash("Attack");
    protected readonly int hitAnimator = Animator.StringToHash("Hurt");
    protected readonly int dieAnimator = Animator.StringToHash("Die");
    protected readonly int roarAnimator = Animator.StringToHash("Roar");
    protected readonly int earthquakeAnimator = Animator.StringToHash("Earthquake");
    public Action stopAttackPlayer;
    void Start()
    {
        // cameraController = GameObject.Find("CameraHolder").GetComponent<CameraController>();
        // cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        // shakeCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthCur = healthBase;
    }
    private void Update()
    {
        if (stateDragon == 0) return;
        flipFace = sprite.flipX;
        if (flipFace) firePoint = firePoints[0];
        else if (!flipFace) firePoint = firePoints[1];
        if (!startDragonRoar)
        {
            cam.orthographicSize = 10f;
            cameraController.offset = new Vector3(0f, 6f, -10f);
        }
    }
    private void FixedUpdate()
    {
        if (stateDragon == 0) return;
        else if (stateDragon == 1 && !startDragonRoar)
        {
            DragonRoar();
        }
        else if (stateDragon == 2)
        {
            FlowBehavior();
        }
    }
    private void FlowBehavior()
    {
        if (dragonCanFight && !death)
        {
            if (curBehavior % 4 == 0)
            {
                StartCoroutine(Attack1());
            }
            if (curBehavior % 4 == 1)
            {
                //curBehavior++;
                StartCoroutine(Attack2());
            }
            if (curBehavior % 4 == 2)
            {
                //curBehavior++;
                StartCoroutine(Attack3());
            }
            if (curBehavior % 4 == 3)
            {
                //curBehavior++;
                StartCoroutine(Attack4());
            }
        }
    }
    private void DragonRoar()
    {
        startDragonRoar = true;
        cameraController.followPlayer = false;
        cam.orthographicSize = 10f;
        cameraController.offset = new Vector3(-10f, 6f, -10f);
        gameObject.transform.position = startPoint.position;
        animator.SetBool(walkAnimator, true);
        FacePoint(wayPoints[0].position);
        StartCoroutine(MovePosition1(gameObject.transform.position, wayPoints[0].position));

    }
    private void EndBehavior()
    {
        stateDragon = 2;
        startDragonRoar = false;
        dragonCanFight = true;
        curBehavior++;
    }
    IEnumerator MovePosition1(Vector3 start, Vector3 end)
    {
        while (Vector3.Distance(gameObject.transform.position, end) > 2f)
        {
            gameObject.transform.Translate((end - start).normalized * Time.deltaTime * 2f);
            yield return null;
        }
        animator.SetBool(walkAnimator, false);
        animator.SetTrigger(roarAnimator);
    }
    private void shakeScreenStart()
    {
        shakeCam.shakeCamera(2f, 0.25f);
    }
    IEnumerator Attack1()
    {
        //fire ball
        dragonCanFight = false;
        yield return new WaitForSeconds(1.5f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        FacePoint(player.position);
        animator.SetTrigger(atkAnimator);
    }
    private void StartAttack1()
    {
        GameObject fireBall = Instantiate(dragonFire, firePoint.position, Quaternion.identity);
        fireBall.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (!sprite.flipX ? 1f : -1f) * 450f * speed);
        if (angry)
        {
            fireBall = Instantiate(dragonFire, firePoint.position + Vector3.left, Quaternion.identity);
            fireBall.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (!sprite.flipX ? 1f : -1f) * 450f);
        }
    }
    IEnumerator Attack2()
    {
        //fire ball
        dragonCanFight = false;
        yield return new WaitForSeconds(2.5f);
        rb2D.gravityScale = 0f;
        animator.SetBool(flyAnimator, true);
        DragonSoundFly();
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 15f);
        StartCoroutine(FlyPosition(gameObject.transform.position, wayPoints[1].position));
    }
    IEnumerator FlyPosition(Vector3 start, Vector3 end)
    {
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        while (Vector3.Distance(gameObject.transform.position, end - new Vector3(3f, 0f, 0f)) > 3.5f)
        {
            gameObject.transform.Translate((end - start).normalized * Time.deltaTime * 10f);
            yield return null;
        }
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        animator.SetBool(flyAnimator, false);
        yield return new WaitForSeconds(0.5f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        FacePoint(player.position);
        StartCoroutine(FollowPlayer(gameObject.transform.position, player.position));
    }
    IEnumerator FollowPlayer(Vector3 start, Vector3 end)
    {
        while (Vector3.Distance(gameObject.transform.position, end) > 3.5f)
        {
            //Debug.Log(Vector3.Distance(gameObject.transform.position, end));
            gameObject.transform.Translate((end - start).normalized * Time.deltaTime * 30f * speed);
            yield return null;
        }
        DragonSoundGroudHitExplosion();
        DragonSoundEarthquake();
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        rb2D.gravityScale = 10f;
        shakeScreenStart();
        EndBehavior();
    }
    IEnumerator Attack3()
    {
        //fire ball
        dragonCanFight = false;
        yield return new WaitForSeconds(1.5f);
        rb2D.gravityScale = 10f;
        animator.SetBool(walkAnimator, true);
        FacePoint(wayPoints[2].position);
        StartCoroutine(MovePosition3(gameObject.transform.position, wayPoints[2].position));
    }
    IEnumerator MovePosition3(Vector3 start, Vector3 end)
    {
        while (Vector3.Distance(gameObject.transform.position, end) > 2f)
        {
            gameObject.transform.Translate((end - start).normalized * Time.deltaTime * 4f);
            yield return null;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        FacePoint(player.position);
        animator.SetBool(walkAnimator, false);
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger(earthquakeAnimator);
    }
    private void earthQuake()
    {
        for (int i = 0; i < rockQuantity; i++)
        {
            rockPoint = new Vector3(wayPoints[2].position.x + Random.Range(-18f, 25f), wayPoints[2].position.y + Random.Range(10f, 20f), 0f);
            Instantiate(dragonRock, rockPoint, Quaternion.identity);
        }
        shakeCam.shakeCamera(0.5f, 0.25f);
    }
    IEnumerator Attack4()
    {
        //fire ball
        dragonCanFight = false;
        yield return new WaitForSeconds(1.5f);
        rb2D.gravityScale = 10f;
        animator.SetBool(walkAnimator, true);
        FacePoint(wayPoints[3].position);
        StartCoroutine(MovePosition4(gameObject.transform.position, wayPoints[3].position));
    }
    IEnumerator MovePosition4(Vector3 start, Vector3 end)
    {
        while (Vector3.Distance(gameObject.transform.position, end) > 2f)
        {
            gameObject.transform.Translate((end - start).normalized * Time.deltaTime * 2f);
            yield return null;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        FacePoint(player.position);
        animator.SetBool(walkAnimator, false);
        EndBehavior();
    }
    private void FacePoint(Vector3 target)
    {
        Vector3 faceVector = target - gameObject.transform.position;
        if (faceVector.x < 0) sprite.flipX = true;
        else sprite.flipX = false;
    }
    public void takeDamage(float damage)
    {
        if (healthCur <= 0) return;
        animator.SetTrigger(hitAnimator);
        healthCur -= damage;
        //setHealthBoss(healthCur, healthBase);
        checkHpBoss();
    }
    public void checkHpBoss()
    {
        if (healthCur <= 0)
        {
            death = true;
            StopAllCoroutines();
            rb2D.gravityScale = 10f;
            animator.SetTrigger(dieAnimator);
            stopAttackPlayer();
        }
        else if ((healthCur / healthBase) < 0.3f)
        {
            angry = true;
            speed = 1.5f;
            rockQuantity = 12f;
        }
        else
        {
            angry = false;
            speed = 1f;
            rockQuantity = 7f;
        }
    }
    public void BossDie()
    {
        Instantiate(dieFX, diePoint.position, Quaternion.identity);
        //setActiveUIBoss(false);
        cameraController.followPlayer = true;
        transform.parent.gameObject.SetActive(false);
        //Destroy(transform.parent.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            takeDamage(5f);
            Destroy(other.gameObject);
        }
    }
    public void DragonSoundWalk()
    {
        soundWalk.PlayMusic();
    }
    public void DragonSoundRoar()
    {
        soundRoar.PlayMusic();
    }
    public void DragonSoundFly()
    {
        soundFly.PlayMusic();
    }
    public void DragonSoundGroudHitExplosion()
    {
        soundGroundhitExplosion.PlayMusic();
    }
    public void DragonSoundAttack()
    {
        soundFlame.PlayMusic();
    }
    public void DragonSoundDeath()
    {
        soundDeath.PlayMusic();
    }
    public void DragonSoundEarthquake()
    {
        soundEarthquake.PlayMusic();
    }
    public void DragonSoundHurt()
    {
        soundHurt.PlayMusic();
    }
}
