using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    enum TransitionParameter
    {
        Move, Jump, Die, WallSlide, Dash, Attack,
    }
    [SerializeField] InputManager inputManager;
    [HideInInspector] public bool tutorialMapEnable = true;
    public ParticleSystem runParticle;

    [Header("Player Parameter")]
    public float baseHealth;
    public float baseMana;
    public float attackDamage;
    [SerializeField] float speed;

    // Abilities
    [Header("Abilities Enable")]
    public bool attackEnable = false;
    public bool wallJumpEnable = false;
    public bool dashEnable = false;
    public bool chargeFlameEnable = false;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float xWallJumpForce;
    [SerializeField] float yWallJumpForce;
    [SerializeField] float wallSlidingSpeed;
    public bool isGrounded;
    public Transform groundCheck;
    public bool isTouchingFront;
    public Transform frontCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Dash")]
    [SerializeField] float baseDashingTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float baseDashCoolDownTime;
    [SerializeField] ParticleSystem dashParticle;
    private float dashCoolDownTime;
    private bool groundedOrSlideAfterDash;

    [Header("Normal Attack")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float baseAttackCoolDownTime;
    private float attackCoolDownTime;

    [Header("Charge Flame")]
    [SerializeField] float chargeFlameForce;
    [SerializeField] float chargeFlameRadius;
    [SerializeField] Transform chargeFlamePoint;
    [SerializeField] GameObject chargeFlamePrefab;
    [SerializeField] float damageChargeFlame;
    [SerializeField] ParticleSystem ChargeFlameExplosion;
    public float timeToChargeFlame;
    private GameObject chargeFlameObject = null;


    [Header("Take Damage")]
    [SerializeField] float knockBackForce;
    [SerializeField] float baseTakeDamageCoolDown;
    [SerializeField] ParticleSystem takeDamageParticle;
    public float takeDamageCoolDown;

    [Header("First Take Item")]
    [SerializeField] GameObject FirstTakeItem;

    [Header("State")]
    public bool wallSliding;
    public bool isJumpWall;
    public bool wallJumping;
    public bool isDash = false;
    private Animator animator;
    private Rigidbody2D rb;
    private TargetCheck targetCheck;    // For find closeset target
    private float dashingTime;          // = baseDashingTime - Time.deltaTime per frame
    public float health;
    public float mana;
    private GameObject currentFirstItemIsTaking;
    [HideInInspector] public bool healthPotionIsTaken = false;
    [HideInInspector] public bool fullHealthPotionIsTaken = false;
    [HideInInspector] public bool manaPotionIsTaken = false;
    [HideInInspector] public bool fullManaPotionIsTaken = false;
    [HideInInspector] public bool itemIsTaken = false;

    // Delegate
    public Action<float, float> setHealthUI;
    public Action<float, float> setManaUI;
    public Action<float> setItemCollectedPlusUI;
    public Action setAbilityProgressUI;
    public Action shakeCamera;
    public Action<bool> setActiveAttack;
    public Action<bool> setActiveDash;
    // Guides
    public Action setActiveNormalAttackGuideUI;
    public Action setActiveWallJumpGuideUI;
    public Action setActiveDashGuideUI;
    public Action setActiveChargeFlameGuideUI;
    public Action setActiveHealthPotionGuideUI;
    public Action setActiveFullHealthPotionGuideUI;
    public Action setActiveManaPotionGuideUI;
    public Action setActiveFullManaPotionGuideUI;
    public Action setActiveItemGuideUI;
    public Action<string, bool> setActiveNotHaveKeyUI;
    public Action<string> setActiveHaveKeyUI;
    // Noti
    public Action notiAttackFail;
    // Save point
    public Action<GameObject> enterSavePoint;
    public Action exitSavePoint;
    // Die
    public Action<bool> setActiveInputManager;
    // CoolDownUI
    public Action<float> coolDownAttackUI;
    public Action<float> coolDownDashUI;
    // Teleport
    public Action setActiveMap1;
    public Action setActiveMap2;
    public Action setStatePlayerDeath;
    public Action getFinishAnimationDie;
    bool isSettingDieForPlayer = false;
    void Awake() {
        health = baseHealth;
        mana = baseMana;    
        Time.timeScale = 1f;
    }
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        targetCheck = this.gameObject.GetComponentInChildren<TargetCheck>();
        dashingTime = baseDashingTime;
        setHealthUI(health, baseHealth);
        setManaUI(mana, baseMana);
    }

    public void moveRight()
    {
        // Fix don't die when die is true
        // if (attackCoolDownTime == 0 && !animator.GetBool(TransitionParameter.Die.ToString()))
        if (attackCoolDownTime <= baseAttackCoolDownTime * 0.75 && !animator.GetBool(TransitionParameter.Die.ToString()))
        {
            runParticle.Play();
            // += for jumping bounce from wall and = for limit speed
            if (rb.velocity.x >= speed)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity += new Vector2(speed * 0.3f, 0f);
            }
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            animator.SetBool(TransitionParameter.Move.ToString(), true);
        }
    }

    public void moveLeft()
    {
        // Fix don't die when die is true
        // if (attackCoolDownTime == 0 && !animator.GetBool(TransitionParameter.Die.ToString()))
        if (attackCoolDownTime <= baseAttackCoolDownTime * 0.75 && !animator.GetBool(TransitionParameter.Die.ToString()))
        {
            runParticle.Play();
            // += for jumping bounce from wall and = for limit speed
            if (rb.velocity.x <= -speed)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity += new Vector2(-speed * 0.3f, 0f);
            }
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            animator.SetBool(TransitionParameter.Move.ToString(), true);
        }
    }

    public void jump()
    {
        if (isGrounded)
        {
            runParticle.Play();
            rb.velocity = Vector2.up * jumpForce;
        }
        else if (wallSliding && wallJumpEnable)
        {
            runParticle.Play();
            animator.Play("Jump", 0, 0f);
            if (!inputManager.moveLeft && !inputManager.moveRight)
            {
                if (transform.right.x > 0)
                {
                    rb.velocity = new Vector2(-xWallJumpForce * 0.2f, yWallJumpForce);
                }
                else
                {
                    rb.velocity = new Vector2(xWallJumpForce * 0.2f, yWallJumpForce);
                }
            }
            else
            {
                if (transform.right.x > 0)
                {
                    rb.velocity = new Vector2(-xWallJumpForce, yWallJumpForce);
                }
                else
                {
                    rb.velocity = new Vector2(xWallJumpForce, yWallJumpForce);
                }
            }
        }
    }

    public void idle()
    {
        if (isGrounded & !isDash)
        {
            runParticle.Stop();
            animator.SetBool(TransitionParameter.Move.ToString(), false);
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    public void Die()
    {
        isSettingDieForPlayer = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        // for (int i = 0; i < gameObject.transform.childCount; i++)
        // {
        //     if (gameObject.transform.GetChild(i).name.Equals("CollissionCheck"))
        //     {
        //         gameObject.transform.GetChild(i).gameObject.SetActive(false);
        //         break;
        //     }
        // }
        animator.SetBool(TransitionParameter.Die.ToString(), true);
        animator.SetTrigger("DieTrigger");
        setActiveInputManager(false);
        setStatePlayerDeath();
    }

    public void shoot()
    {
        if (attackEnable && !isDash)
        {
            GameObject target = targetCheck.getClosestTarget();
            if (target != null && attackCoolDownTime == 0)
            {
                if (target.transform.position.x - transform.position.x > 0)
                {
                    this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                else
                {
                    this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                attackCoolDownTime = baseAttackCoolDownTime;
                coolDownAttackUI(baseAttackCoolDownTime);
                // Run animation Attack and Stop when CoolDownTime = 0
                animator.SetBool(TransitionParameter.Attack.ToString(), true);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().directToObjective(target);
                
            }else if (target == null){
                notiAttackFail();
            }
        }
    }

    public void takeDamage(float damage)
    {
        if (isSettingDieForPlayer) return;
        if (takeDamageCoolDown <= 0)
        {
            takeDamageCoolDown = baseTakeDamageCoolDown;
            animator.SetTrigger("TakeDamage");
            takeDamageParticle.Play();
            gameObject.GetComponent<TimeStop>().Stoptime(0.05f, 10f, 0.1f);
            if (health > damage)
            {
                health -= damage;
            }
            else
            {
                health = 0;
                Die();
            }
            // Need to set UI (use delegate)
            setHealthUI(health, baseHealth);
        }
    }

    public void fullHealth(GameObject obj = null)
    {
        health = baseHealth;
        // Need to set UI (use delegate)
        setHealthUI(health, baseHealth);
        if (!fullHealthPotionIsTaken){
            StartCoroutine(waitUntilPlayerIsGroundedToFullHealth(obj));
            fullHealthPotionIsTaken = true;
        }
        else
        {
            GetComponent<SoundPlayer>().playSoundTakeItem();
        }
    }

    IEnumerator waitUntilPlayerIsGroundedToFullHealth(GameObject obj){
        while(!isGrounded || obj.activeInHierarchy){
            yield return null;
        }
        idle();
        animator.SetBool("FirstTakeItem", true);
        currentFirstItemIsTaking = FirstTakeItem.transform.Find("FullHealthPotion").gameObject;
        currentFirstItemIsTaking.SetActive(true);
        setActiveFullHealthPotionGuideUI();
    }

    public void plusHealthOne(GameObject obj = null)
    {
        if (health < baseHealth)
        {
            health += 1;
        }
        // Need to set UI (use delegate)
        setHealthUI(health, baseHealth);
        if (!healthPotionIsTaken){
            StartCoroutine(waitUntilPlayerIsGroundedToHealth(obj));
            healthPotionIsTaken = true;
        }
        else
        {
            GetComponent<SoundPlayer>().playSoundTakeItem();
        }
    }

    IEnumerator waitUntilPlayerIsGroundedToHealth(GameObject obj){
        while(!isGrounded || obj.activeInHierarchy){
            yield return null;
        }
        idle();
        animator.SetBool("FirstTakeItem", true);
        currentFirstItemIsTaking = FirstTakeItem.transform.Find("HealthPotion").gameObject;
        currentFirstItemIsTaking.SetActive(true);
        setActiveHealthPotionGuideUI();
    }

    public void fullMana(GameObject obj = null)
    {
        mana = baseMana;
        // Need to set UI (use delegate)
        setManaUI(mana, baseMana);
        if (!fullManaPotionIsTaken){
            // setActiveFullManaPotionGuideUI();
            StartCoroutine(waitUntilPlayerIsGroundedToFullMana(obj));
            fullManaPotionIsTaken = true;
        }
        else
        {
            GetComponent<SoundPlayer>().playSoundTakeItem();
        }
    }
    IEnumerator waitUntilPlayerIsGroundedToFullMana(GameObject obj){
        while(!isGrounded || obj.activeInHierarchy){
            yield return null;
        }
        idle();
        animator.SetBool("FirstTakeItem", true);
        currentFirstItemIsTaking = FirstTakeItem.transform.Find("FullManaPotion").gameObject;
        currentFirstItemIsTaking.SetActive(true);
        setActiveFullManaPotionGuideUI();
    }

    public void plusManaOne(GameObject obj = null)
    {
        if (mana < baseMana)
        {
            mana += 1;
        }
        // Need to set UI (use delegate)
        setManaUI(mana, baseMana);
        if (!manaPotionIsTaken){
            StartCoroutine(waitUntilPlayerIsGroundedToMana(obj));
            manaPotionIsTaken = true;
        }
        else
        {
            GetComponent<SoundPlayer>().playSoundTakeItem();
        }
    }

    IEnumerator waitUntilPlayerIsGroundedToMana(GameObject obj){
        while(!isGrounded || obj.activeInHierarchy){
            yield return null;
        }
        idle();
        animator.SetBool("FirstTakeItem", true);
        currentFirstItemIsTaking = FirstTakeItem.transform.Find("ManaPotion").gameObject;
        currentFirstItemIsTaking.SetActive(true);
        setActiveManaPotionGuideUI();
    }

    public void takeOneMana()
    {
        if (mana > 0)
        {
            mana -= 1;
        }
        setManaUI(mana, baseMana);
    }

    public void collectItem(GameObject obj = null){
        setItemCollectedPlusUI(1);
        if (!itemIsTaken){
            itemIsTaken = true;
            StartCoroutine(waitUntilPlayerIsGroundedToItem(obj));
        }
        else
        {
            GetComponent<SoundPlayer>().playSoundTakeItem();
        }
    }

    IEnumerator waitUntilPlayerIsGroundedToItem(GameObject obj){
        while(!isGrounded || obj.activeInHierarchy){
            yield return null;
        }
        idle();
        animator.SetBool("FirstTakeItem", true);
        currentFirstItemIsTaking = FirstTakeItem.transform.Find("Item").gameObject;
        currentFirstItemIsTaking.SetActive(true);
        setActiveItemGuideUI();
    }

    public void dash()
    {
        if (dashEnable && dashCoolDownTime == 0 && groundedOrSlideAfterDash && dashingTime == baseDashingTime)
        {
            runParticle.Play();
            if (dashParticle.isStopped)
                dashParticle.Play();
            isDash = true;
            coolDownDashUI(baseDashCoolDownTime);
        }
        // Dash in Update
    }

    public void instanceFlameCharging()
    {
        if (chargeFlameObject == null)
        {
            chargeFlameObject = Instantiate(chargeFlamePrefab, chargeFlamePoint);
        }
    }

    public void destroyFlameCharging()
    {
        Destroy(chargeFlameObject);
    }

    public void chargeFlame()
    {
        if (chargeFlameEnable)
        {
            if (mana > 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(firePoint.position, chargeFlameRadius);
                foreach (Collider2D hit in colliders)
                {
                    Rigidbody2D body = hit.gameObject.GetComponent<Rigidbody2D>();
                    if (body != null)
                    {
                        CreepOnTheGround creep = body.gameObject.GetComponent<CreepOnTheGround>();
                        if (creep != null)
                        {
                            creep.RecieveDamage(damageChargeFlame);
                        }
                        addExplosionForce2D(body, chargeFlameForce, chargeFlamePoint.position, chargeFlameRadius);
                    }else{
                        if (hit.gameObject.CompareTag("SpecialCreep")){
                            Destroy(hit.gameObject, 0.3f);
                        }
                    }
                    
                }
                takeOneMana();
                shakeCamera();
            }
            Destroy(chargeFlameObject);
            ChargeFlameExplosion.Play();
            GetComponent<SoundPlayer>().playSoundChargeFlame();
            animator.SetBool("ChargeFlame", false);
        }
    }

    void addExplosionForce2D(Rigidbody2D body, float explosionForce, Vector2 explosionPosition, float explosionRadius)
    {
        Vector2 direction = (Vector2)body.transform.position - explosionPosition;
        float wearoff = 1 - (direction.magnitude / explosionRadius);
        if (wearoff < 0)
            wearoff = 0;
        if (direction.x < 1 && direction.x > 0)
        {
            body.AddForce(Vector2.right * explosionForce * 0.5f);
        }
        else if (direction.x > -1 && direction.x <= 0)
        {
            body.AddForce(Vector2.left * explosionForce * 0.5f);
        }
        else
        {
            body.AddForce(direction.normalized * explosionForce * wearoff);
        }
        // Debug.Log(direction.normalized * explosionForce * wearoff);
        // Debug.Log((Vector2)body.transform.position - explosionPosition);
    }

    public void enableAttack(GameObject obj)
    {
        setActiveAttack(true);
        attackEnable = true;
        setAbilityProgressUI();
        StartCoroutine(waitUntilPlayerIsGroundedToEnableAttack(obj));
    }
    IEnumerator waitUntilPlayerIsGroundedToEnableAttack(GameObject obj){
        while(!isGrounded || obj.activeInHierarchy){
            yield return null;
        }
        idle();
        animator.SetBool("FirstTakeItem", true);
        currentFirstItemIsTaking = FirstTakeItem.transform.Find("NormalAttack").gameObject;
        currentFirstItemIsTaking.SetActive(true);
        setActiveNormalAttackGuideUI();
    }

    public void enableWallJump(GameObject obj)
    {
        wallJumpEnable = true;
        setAbilityProgressUI();
        StartCoroutine(waitUntilPlayerIsGroundedToEnableWallJump(obj));
    }
    IEnumerator waitUntilPlayerIsGroundedToEnableWallJump(GameObject obj){
        while(!isGrounded || obj.activeInHierarchy){
            yield return null;
        }
        idle();
        animator.SetBool("FirstTakeItem", true);
        currentFirstItemIsTaking = FirstTakeItem.transform.Find("WallJump").gameObject;
        currentFirstItemIsTaking.SetActive(true);
        setActiveWallJumpGuideUI();
    }

    public void enableDash(GameObject obj)
    {
        setActiveDash(true);
        dashEnable = true;
        setAbilityProgressUI();
        StartCoroutine(waitUntilPlayerIsGroundedToEnableDash(obj));
    }
    IEnumerator waitUntilPlayerIsGroundedToEnableDash(GameObject obj){
        while(!isGrounded || obj.activeInHierarchy){
            yield return null;
        }
        idle();
        animator.SetBool("FirstTakeItem", true);
        currentFirstItemIsTaking = FirstTakeItem.transform.Find("Dash").gameObject;
        currentFirstItemIsTaking.SetActive(true);
        setActiveDashGuideUI();
    }

    public void enableChargeFlame(GameObject obj){
        chargeFlameEnable = true;
        setAbilityProgressUI();
        StartCoroutine(waitUntilPlayerIsGroundedToEnableChargeFlame(obj));
    }
    IEnumerator waitUntilPlayerIsGroundedToEnableChargeFlame(GameObject obj){
        while(!isGrounded || obj.activeInHierarchy){
            yield return null;
        }
        idle();
        animator.SetBool("FirstTakeItem", true);
        currentFirstItemIsTaking = FirstTakeItem.transform.Find("ChargeFlame").gameObject;
        currentFirstItemIsTaking.SetActive(true);
        setActiveChargeFlameGuideUI();
    }

    void FixedUpdate()
    {
        // Set Animation between isGround or not
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            // Set Dash can be used
            groundedOrSlideAfterDash = true;
            animator.SetBool(TransitionParameter.Jump.ToString(), false);
        }
        else
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), true);
        }

        // Wall Sliding
        if (isTouchingFront && !isGrounded && rb.velocity.y <= 0)
        {
            // Set Dash can be used
            groundedOrSlideAfterDash = true;
            wallSliding = true;
            animator.SetBool(TransitionParameter.WallSlide.ToString(), true);
        }
        else
        {
            wallSliding = false;
            animator.SetBool(TransitionParameter.WallSlide.ToString(), false);
        }
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
        }

        // Dash
        // Dash CoolDown
        if (dashCoolDownTime > 0)
        {
            dashCoolDownTime -= Time.deltaTime;
            if (dashCoolDownTime < 0)
            {
                dashCoolDownTime = 0;
            }
        }
        // Dash Action
        if (isDash)
        {
            if (dashingTime > 0)
            {
                animator.SetBool(TransitionParameter.Dash.ToString(), true);
                dashingTime -= Time.deltaTime;
                if (gameObject.transform.right == Vector3.right)
                {

                    rb.velocity = Vector2.right * dashSpeed * 50;
                    // rb.velocity = Vector2.zero;
                    // transform.Translate(Vector2.right * dashSpeed);
                }
                else
                {
                    // rb.velocity = Vector2.zero;
                    // transform.Translate(Vector2.right * dashSpeed);
                    rb.velocity = Vector2.left * dashSpeed * 50;

                }
            }
            else
            {
                animator.SetBool(TransitionParameter.Dash.ToString(), false);
                // Dash in not flat surface
                rb.velocity = new Vector2(0f, Mathf.Clamp01(rb.velocity.y));
                dashingTime = baseDashingTime;
                isDash = false;
                // Set Cool Down
                dashCoolDownTime = baseDashCoolDownTime;
                groundedOrSlideAfterDash = false;
            }

        }

        // Attack CoolDown
        if (attackCoolDownTime > 0)
        {
            attackCoolDownTime -= Time.deltaTime;
            if (attackCoolDownTime <= baseAttackCoolDownTime * 0.75){
                animator.SetBool(TransitionParameter.Attack.ToString(), false);
            }
            if (attackCoolDownTime < 0)
            {
                attackCoolDownTime = 0;
                animator.SetBool(TransitionParameter.Attack.ToString(), false);
            }
        }

        // TakeDamageCoolDown
        if (takeDamageCoolDown > 0)
        {
            takeDamageCoolDown -= Time.deltaTime;
        }
    }

    public void setBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    public void knockBack(Vector3 objectPosition)
    {

        // Stop dash to avoid 2 collider collide each other.
        if (isDash)
        {
            dashingTime = 0;
        }
        Vector3 direction = transform.position - objectPosition;
        if (direction.x > 0)
        {
            rb.velocity = new Vector2(knockBackForce, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-knockBackForce, 0f);
        }
        // rb.velocity += new Vector2(direction.x, 0f) * knockBackForce;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Toxic"))
        {
            float damage = other.GetComponent<DamageEnemy>().damage;
            takeDamage(damage);
        }
    }

    public void setDeactiveCurrentItemIsTaking(){
        currentFirstItemIsTaking.SetActive(false);
    }
    public void finishAnimationDie()
    {
        getFinishAnimationDie();
    }
}
