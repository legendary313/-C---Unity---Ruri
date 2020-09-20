using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] UIEventSystem uIEventSystem;
    [Header("Target Detection")]
    [SerializeField] TargetDetection targetDetection;
    [Header("Boss")]
    [SerializeField] GameObject dragonObject;
    [SerializeField] DragonBehavior dragonBehavior;
    [Header("Boss Location")]
    [SerializeField] Transform bossLocation;
    [Header("Pillar")]
    [SerializeField] Transform[] pillars;
    [SerializeField] Transform heighPillar;
    [SerializeField] Transform endPillar;
    [Header("Camera")]
    [SerializeField] ShakeCamera shakeCamera;
    [SerializeField] CameraController cameraController;
    [SerializeField] Camera cam;
    [Header("Boss Zone")]
    [SerializeField] BossZone bossZone;
    [Header("Sound")]
    [SerializeField] SoundTrigger soundTrigger;
    [SerializeField] BackgroundMusic mainBgMusic;
    [SerializeField] BackgroundMusic soundAttackIntro;
    [SerializeField] BackgroundMusic soundAttackLoop;
    [SerializeField] BackgroundMusic soundAttackWin;
    [SerializeField] SFXMusic soundPillar;
    [Header("Trigger")]
    [SerializeField] GameObject soundTriggerObject;
    [SerializeField] GameObject soundRoarOnMap;
    // [Header("Boss Health")]
    // public float healthCur = 0f;
    // public float healthBase;
    float healthCur = 0f;
    float healthBase = 0f;
    float healthNext;
    public bool canSpawn = true;
    public bool death = false;
    public Action<float, float> setHealthBoss;
    public Action<bool> setActiveUIBoss;
    private void Awake()
    {
        bossZone.startAttackPlayer = startAttackPlayer;
        dragonBehavior.stopAttackPlayer = stopAttackPlayer;
        soundTrigger.getStateSoundBoss = getStateSoundBoss;
    }
    void Start()
    {
        //shakeCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeCamera>();
        //cameraController = GameObject.Find("CameraHolder").GetComponent<CameraController>();
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        healthNext = healthCur;
        healthCur = dragonBehavior.healthCur;
        healthBase = dragonBehavior.healthBase;
        if (healthCur != healthNext)
        {
            setHealthBoss(healthCur, healthBase);
        }
        // if (boss != null && pillarTop)
        // {
        //     StopAllCoroutines();
        //     dragon = GameObject.FindGameObjectWithTag("Boss").GetComponent<DragonBehavior>();
        //     if (!dragon.death)
        //     {
        //         if(!flagActiveUIBoss)
        //         {
        //             flagActiveUIBoss = true;
        //             setActiveUIBoss(true);
        //         }
        //         healthNext = healthCur;
        //         healthCur = dragon.healthCur;
        //         healthBase = dragon.healthBase;
        //         if (healthCur != healthNext)
        //         {
        //             setHealthBoss(healthCur, healthBase);
        //         }
        //         killBoss = 1;
        //     }
        //     else
        //     {
        //         healthCur = 0f;
        //         healthBase = dragon.healthBase;
        //         setHealthBoss(healthCur, healthBase);
        //         StartCoroutine(disableUIBoss(2f));
        //         killBoss = 2;
        //         boss = null;
        //     }
        //     //Debug.Log(killBoss);
        // }
        // if (targetDetection.targetDetection && boss == null && killBoss == 0)
        // {
        //     canSpawn = true;
        //     if (bossActive)
        //     {
        //         //bossActive = false;
        //         boss = Instantiate(bossPrefab, bossLocation.position, Quaternion.identity);
        //         boss.transform.parent = gameObject.transform;
        //     }
        //     // boss = Instantiate(bossPrefab, bossLocation.position, Quaternion.identity);
        //     // boss.transform.parent = gameObject.transform;
        // }

        // if (!targetDetection.targetDetection)
        // {
        //     //Destroy(boss);
        //     canSpawn = false;
        // }

        // if (!pillarMove && bossZone.findPlayer && canSpawn && killBoss < 2)
        // {
        //     pillarMove = true;
        //     StartCoroutine(PillarActive(5f));
        // }
        // if (!pillarMove && killBoss == 2)
        // {
        //     pillarMove = true;
        //     StartCoroutine(PillarNotActive(5f));
        // }
    }
    void startAttackPlayer()
    {
        //pillarMove = true;
        if (canSpawn)
            StartCoroutine(PillarActive(5f));
    }
    void stopAttackPlayer()
    {
        audioBossStop();
        soundTriggerObject.SetActive(false);
        soundAttackWin.PlayMusic();
        death = true;
        canSpawn = false;
        StartCoroutine(PillarNotActive(5f));
    }
    IEnumerator disableUIBoss(float time)
    {
        yield return new WaitForSeconds(time);
        setActiveUIBoss(false);
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag.Equals("BossZone") && !killBoss)
    //     {
    //         StartCoroutine(PillarActive(3f));
    //     }
    // }
    IEnumerator PillarActive(float speed)
    {
        soundPillar.PlayMusic();
        uIEventSystem.InactiveAll();
        joystick.StopMovement();
        while (pillars[0].position.y < heighPillar.position.y)
        {
            pillars[0].transform.Translate(Vector3.up * Time.deltaTime * speed);
            pillars[1].transform.Translate(Vector3.up * Time.deltaTime * speed);
            shakeCamera.shakeCamera(0.1f, 0.1f);
            cam.orthographicSize = 10f;
            cameraController.offset = new Vector3(0f, 6f, -10f);
            yield return null;
        }
        // pillarMove = false;
        // bossActive = true;
        // pillarTop = true;
        soundPillar.StopMusic();
        dragonObject.SetActive(true);
        setActiveUIBoss(true);
        dragonBehavior.stateDragon = 1;
        uIEventSystem.ActiveUIPlayGame();
    }
    IEnumerator PillarNotActive(float speed)
    {
        soundPillar.PlayMusic();
        while (pillars[0].position.y > endPillar.position.y)
        {
            pillars[0].transform.Translate(Vector3.down * Time.deltaTime * speed);
            pillars[1].transform.Translate(Vector3.down * Time.deltaTime * speed);
            shakeCamera.shakeCamera(0.1f, 0.1f);
            cam.orthographicSize = 10f;
            cameraController.offset = new Vector3(0f, 6f, -10f);
            yield return null;
        }
        soundPillar.StopMusic();
        dragonObject.SetActive(false);
        cam.orthographicSize = 5f;
        cameraController.offset = new Vector3(0f, 2f, -10f);
        StartCoroutine(disableUIBoss(1.5f));
        // pillarMove = false;
        // bossActive = false;
    }
    void getStateSoundBoss(bool val)
    {
        if (val)
        {
            StartCoroutine(audioBossPlay());
        }
        else
        {
            audioBossStop();
        }
    }
    IEnumerator audioBossPlay()
    {
        mainBgMusic.PauseMusic();
        soundAttackIntro.PlayMusic();
        yield return new WaitForSeconds(5f);
        soundAttackLoop.PlayMusic();
    }
    void audioBossStop()
    {
        soundAttackIntro.StopMusic();
        soundAttackLoop.StopMusic();
        soundAttackWin.StopMusic();
        mainBgMusic.PlayMusic();
    }
    public void LoadDataBoss(string str)
    {
        if (str == "0") canSpawn = false;
        else canSpawn = true;
        if (!canSpawn)
        {
            gameObject.SetActive(false);
            soundRoarOnMap.SetActive(false);
        }
        else
        {
            soundRoarOnMap.SetActive(true);
        }
    }
    public string SaveDataBoss()
    {
        return (canSpawn ? "1" : "0");
    }
}
