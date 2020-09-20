using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class GameManager : MonoBehaviour
{
    // public ParameterManager gameParameter;
    [SerializeField] UIEventSystem uIEventSystem;
    [SerializeField] UIPlayGameManager uIPlayGame;
    [SerializeField] UIMenuManager uIMenuManager;
    [SerializeField] UIGuideManager uIGuideManager;
    [SerializeField] UIPauseManager uIPauseManager;
    [SerializeField] UIMapManager uIMapManager;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] SaveLoadManager saveLoadManager;
    [SerializeField] InputManager inputManager;
    [SerializeField] ItemManager itemManager;
    [SerializeField] KeyDoorManager keyDoorManager;
    [SerializeField] GodTreeBehavior godTreeBehavior;
    [SerializeField] PlayerController player;
    [SerializeField] CameraController cameraController;
    [Header("Boss")]
    [SerializeField] SpawnBoss dragon;

    // Achievement
    [Header("Achievement")]
    [SerializeField] float itemCollectableAmount;
    [HideInInspector] public float itemCollectedAmount = 0;
    [HideInInspector] public float killedCreepAmount = 0;
    [HideInInspector] public float abilityCompletePercent = 0;
    [HideInInspector] public float completeMap1Progress = 0;
    [HideInInspector] public float completeMap2Progress = 0;
    private GameObject playerSavePoint;
    [Header("Map")]
    [SerializeField] GameObject Map1;
    [SerializeField] GameObject Map2;
    [Header("Joystick")]
    [SerializeField] FloatingJoystick joystick;
    
    [Header("Door")]
    [SerializeField] ChangeMap changeMap;
    [SerializeField] DoorBehavior[] doors;

    void Awake()
    {
        player.setHealthUI = setHealthUI;
        player.setManaUI = setManaUI;
        player.setItemCollectedPlusUI = setItemCollectedUI;
        player.setAbilityProgressUI = setAbilityProgressUI;
        player.shakeCamera = shakeCamera;
        player.setActiveAttack = setActiveAttack;
        player.setActiveDash = setActiveDash;
        player.notiAttackFail = notiAttackFail;
        // Check Point
        player.enterSavePoint = playerEnterSavePoint;
        player.exitSavePoint = setInactiveUISave;
        // Save
        // player.saveGameWithoutUIInMap = saveGameWithoutUIInMap;
        // Guide UI
        player.setActiveNormalAttackGuideUI = setActiveNormalAttackGuideUI;
        player.setActiveWallJumpGuideUI = setActiveWallJumpGuideUI;
        player.setActiveDashGuideUI = setActiveDashGuideUI;
        player.setActiveChargeFlameGuideUI = setActiveChargeFlameGuideUI;
        player.setActiveHealthPotionGuideUI = setActiveHealthPotionGuideUI;
        player.setActiveFullHealthPotionGuideUI = setActiveFullHealthPotionGuideUI;
        player.setActiveManaPotionGuideUI = setActiveManaPotionGuideUI;
        player.setActiveFullManaPotionGuideUI = setActiveFullManaPotionGuideUI;
        player.setActiveItemGuideUI = setActiveItemGuideUI;
        player.setActiveNotHaveKeyUI = setActiveNotHaveKeyUI;
        player.setActiveHaveKeyUI = setActiveHaveKeyUI;
        player.setStatePlayerDeath = setStatePlayerDeath;
        // Die
        player.setActiveInputManager = setActiveInputManager;
        player.getFinishAnimationDie = LoadSceneAfterDie;
        // CoolDownUI
        player.coolDownAttackUI = coolDownAttackUI;
        player.coolDownDashUI = coolDownDashUI;
        // Teleport
        player.setActiveMap1 = setActiveMap1;
        player.setActiveMap2 = setActiveMap2;
        // After Die
        // player.loadGameAfterDead = LoadGame;
        uIMenuManager.getHealthAndBaseHealthDel = getHealthAndBaseHealth;
        uIMenuManager.getManaAndBaseManaDel = getManaAndBaseMana;
        uIMenuManager.getCompleteMap1PercentDel = getCompleteMap1Percent;
        uIMenuManager.getCompleteMap2PercentDel = getCompleteMap2Percent;
        uIMenuManager.getItemCollectedPercentDel = getItemCollectedPercent;
        uIMenuManager.getKilledCreepAmountDel = getKilledCreep;
        uIMenuManager.getAbilityCompletePercentDel = getAbilityCompletePercent;
        // uIGuideManager
        uIGuideManager.afterSetInactiveUI = setActiveInputManagerAfterDeactiveUIGuide;
        // uiPauseGame
        uIPauseManager.setResolution = setResolution;
        // keyDoorManager
        keyDoorManager.setActiveHaveKeyUI = setActiveHaveKeyUI;
        enemyManager.enemyDie = setKilledCreepUI;
        dragon.setHealthBoss = setHealthBoss;
        dragon.setActiveUIBoss = setActiveUIBoss;
        Screen.SetResolution(960, 540, true);
        // uIEventSystem
        uIEventSystem.activeUIMenu = setCompleteMapProgress;
        //SaveDataMinimap();
        //LoadMinimap(curState);
        //Screen.SetResolution(800, 450, true);

        //Change Map
        changeMap.closeDoor = closeDoor;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("SavedGame") == 1)
        {
            LoadGame();
        }
        else
        {
            LoadGameDefault();
        }

        if (PlayerPrefs.GetInt("PlayingMap") == 1)
        {
            setActiveMap1();
        }
        else
        {
            setActiveMap2();
        }
    }

    void playerEnterSavePoint(GameObject savePoint)
    {
        playerSavePoint = savePoint;
        setActiveUISave();
    }

    public void SaveGame()
    {
        if (player.mana > 0)
        {
            player.takeOneMana();
            setInactiveUISave();
            SavePointEffect savePointEffect = playerSavePoint.GetComponent<SavePointEffect>();
            savePointEffect.runSave1Particle();

            PlayerData playerData = new PlayerData(player, godTreeBehavior);
            AchievementData achievement = new AchievementData(this);
            ItemData itemData = new ItemData(itemManager, uIMapManager, keyDoorManager);
            saveLoadManager.SaveGame(playerData, achievement, itemData);
            savePointEffect.runSave2Particle();
            setNotiSavedGame();
            if (player.tutorialMapEnable)
            {
                uIPlayGame.setActiveArrowUIMap(true);
                player.tutorialMapEnable = false;
            }
        }
        else
        {
            setNotiCanNotSaveGame();
        }
    }

    // public void saveGameWithoutUIInMap(Vector3 position, int index)
    // {
    //     PlayerData playerData = new PlayerData(player, godTreeBehavior);
    //     playerData.position[0] = position.x;
    //     playerData.position[1] = position.y;
    //     playerData.position[2] = position.z;
    //     AchievementData achievement = new AchievementData(this);
    //     ItemData itemData = new ItemData(itemManager, uIMapManager, keyDoorManager);
    //     saveLoadManager.SaveGameInMap(playerData, achievement, itemData, index);
    // }

    // public void addPositionIntoSharedData(int playingMap, int index)
    // {
    //     int indexPoint = PlayerPrefs.GetInt("PlayerPosition");
    //     PlayerPrefs.SetInt("PlayerPosition", 0);

    //     Vector3 position;
    //     if (playingMap == 1){
    //         position = player.gameObject.GetComponentInChildren<ChangeMap>().changeMap1Point[indexPoint - 1].position;
    //     }else{
    //         position = player.gameObject.GetComponentInChildren<ChangeMap>().changeMap2Point[indexPoint - 1].position;
    //     }
    //     saveLoadManager.addPositionIntoFile(position);
    // }
    public void LoadGameDefault()
    {
        loadDataForMeetGodTree("0");
        loadDataForMiniMap("", 1);
        PlayerPrefs.SetInt("PlayingMap", 1);
    }

    public void LoadGame()
    {
        setActiveInputManager(true);
        GameData gameData = saveLoadManager.loadSharedData();
        ItemData itemData = saveLoadManager.loadPrivateData();
        PlayerPrefs.SetInt("PlayingMap", gameData.playingMap);
        loadDataForPlayer(gameData);
        loadDataForAchievement(gameData);
        loadDataForMeetGodTree(gameData.godTreeData);
        loadDataForItem(itemData);
        loadDataForMiniMap(itemData.listOfPointInMiniMap1, 1);
        loadDataForMiniMap(itemData.listOfPointInMiniMap2, 2);
        setUIAfterLoad();
    }
    void loadDataForPlayer(GameData gameData)
    {
        player.health = gameData.health;
        player.mana = gameData.mana;

        player.attackEnable = gameData.attackEnable;
        player.wallJumpEnable = gameData.wallJumpEnable;
        player.dashEnable = gameData.dashEnable;
        player.chargeFlameEnable = gameData.chargeFlameEnable;
        player.transform.position = new Vector3(gameData.position[0], gameData.position[1], gameData.position[2]);

        player.healthPotionIsTaken = gameData.healthPotionIsTaken;
        player.fullHealthPotionIsTaken = gameData.fullHealthPotionIsTaken;
        player.manaPotionIsTaken = gameData.manaPotionIsTaken;
        player.fullManaPotionIsTaken = gameData.fullManaPotionIsTaken;
        player.itemIsTaken = gameData.itemIsTaken;
        player.tutorialMapEnable = gameData.tutorialMapEnable;
    }

    void loadDataForAchievement(GameData gameData)
    {
        abilityCompletePercent = gameData.abilityCompletePercent;
        itemCollectedAmount = gameData.itemCollectedAmount;
        killedCreepAmount = gameData.killedCreepAmount;
        completeMap1Progress = gameData.completeMap1Progress;
        completeMap2Progress = gameData.completeMap2Progress;
    }

    void loadDataForMeetGodTree(string godTreeData)
    {
        godTreeBehavior.loadMeetGodTreeData(godTreeData);
    }
    void loadDataForItem(ItemData itemData)
    {
        itemManager.setInactiveItem(itemData.listInactiveDiamond, "Diamond");
        itemManager.setInactiveItem(itemData.listInactiveHealthPotion, "HealthPotion");
        itemManager.setInactiveItem(itemData.listInactiveFullHealthPotion, "FullHealthPotion");
        itemManager.setInactiveItem(itemData.listInactiveManaPotion, "ManaPotion");
        itemManager.setInactiveItem(itemData.listInactiveFullManaPotion, "FullManaPotion");
        itemManager.setInactiveItem(itemData.listInactiveAbilityEnable, "AbilityEnable");
        // KeyDoor
        keyDoorManager.setInactiveItem(itemData.listInactiveKey, "Key");
        loadDataForKeyHolder();
    }

    void loadDataForKeyHolder()
    {
        KeyHolder keyHolder = player.gameObject.GetComponentInChildren<KeyHolder>();
        keyDoorManager.loadDataForKeyHolder(keyHolder);
    }

    void loadDataForMiniMap(string listOfPointInMinimap, int index)
    {
        uIMapManager.LoadDataMinimap(listOfPointInMinimap, index);
    }

    void setUIAfterLoad()
    {
        setControlUI();
        setProfileUI();
        setAchievementUI();
        setKeyUI();
    }

    void setActiveInputManager(bool state)
    {
        inputManager.enabled = state;
    }

    void setControlUI()
    {
        if (player.attackEnable)
        {
            setActiveAttack(true);
        }
        else
        {
            setActiveAttack(false);
        }

        if (player.dashEnable)
        {
            setActiveDash(true);
        }
        else
        {
            setActiveDash(false);
        }
    }

    void setProfileUI()
    {
        setHealthUI(player.health, player.baseHealth);
        setManaUI(player.mana, player.baseMana);
    }
    void setAchievementUI()
    {
        setCompleteMap1Progress();
        setCompleteMap2Progress();
        setItemCollectedUI(0);
        setKilledCreepUI(0);
        setAbilityProgressUI();
    }

    void setKeyUI()
    {
        keyDoorManager.setKeyUI();
    }

    void setCompleteMapProgress()
    {
        setCompleteMap1Progress();
        setCompleteMap2Progress();
    }

    // Save UI
    void setActiveUISave()
    {
        uIPlayGame.setActiveUISave(true);
    }

    void setInactiveUISave()
    {
        uIPlayGame.setActiveUISave(false);
    }

    void setNotiSavedGame()
    {
        uIPlayGame.setActiveUINotiGameSaved();
    }

    void setNotiCanNotSaveGame()
    {
        uIPlayGame.setActiveUINotiCanNotSaveGame();
    }

    // Guilde UI
    // Set inputManage.enabled = false to make player can't control until click OK
    void setActiveNormalAttackGuideUI()
    {
        uIGuideManager.setActiveUINormalAttack(true);
        inputManager.enabled = false;
    }

    void setActiveWallJumpGuideUI()
    {
        uIGuideManager.setActiveUIWallJump(true);
        inputManager.enabled = false;
    }

    void setActiveDashGuideUI()
    {
        uIGuideManager.setActiveUIDash(true);
        inputManager.enabled = false;
    }

    void setActiveChargeFlameGuideUI()
    {
        uIGuideManager.setActiveUIChargeFlame(true);
        inputManager.enabled = false;
    }

    void setActiveHealthPotionGuideUI()
    {
        uIGuideManager.setActiveUIHealthPotion(true);
        inputManager.enabled = false;
    }

    void setActiveFullHealthPotionGuideUI()
    {
        uIGuideManager.setActiveUIFullHealthPotion(true);
        inputManager.enabled = false;
    }

    void setActiveManaPotionGuideUI()
    {
        uIGuideManager.setActiveUIManaPotion(true);
        inputManager.enabled = false;
    }

    void setActiveFullManaPotionGuideUI()
    {
        uIGuideManager.setActiveUIFullManaPotion(true);
        inputManager.enabled = false;
    }

    void setActiveItemGuideUI()
    {
        uIGuideManager.setActiveUIItem(true);
        inputManager.enabled = false;
    }

    void setActiveNotHaveKeyUI(string keyType, bool state)
    {
        if (keyType.Equals("Red"))
        {
            uIGuideManager.setActiveNotHaveRedKey(state);
        }
        else if (keyType.Equals("Green"))
        {
            uIGuideManager.setActiveNotHaveGreenKey(state);
        }
        else if (keyType.Equals("Blue"))
        {
            uIGuideManager.setActiveNotHaveBlueKey(state);
        }
        else if (keyType.Equals("Jikan1"))
        {
            uIGuideManager.setActiveNotHaveJikanStone(state);
        }
    }

    void setActiveHaveKeyUI(string keyType)
    {
        uIPlayGame.setActiveHaveKeyUI(keyType);
    }

    // CoolDownUI
    void coolDownAttackUI(float coolDownTime)
    {
        uIPlayGame.UICoolDownAttack(coolDownTime);
    }

    void coolDownDashUI(float coolDownTime)
    {
        uIPlayGame.UICoolDownDash(coolDownTime);
    }

    void setActiveUIBoss(bool val)
    {
        uIPlayGame.setActiveUIBoss(val);
    }
    void setHealthBoss(float health, float baseHealth)
    {
        uIPlayGame.setHealthBoss(health / baseHealth);
    }
    void setHealthUI(float health, float baseHealth)
    {
        uIPlayGame.setHealthUI(health / baseHealth);
        uIMenuManager.setHealthUI(health, baseHealth);
    }
    void setManaUI(float mana, float baseMana)
    {
        uIPlayGame.setManaUI(mana / baseMana);
        uIMenuManager.setManaUI(mana, baseMana);
    }

    void setCompleteMap1Progress()
    {
        float map1CompleteProgress = uIMapManager.getProgressMap(1); // Read from uIMapManager
        uIMenuManager.setCompleteMap1Progress(map1CompleteProgress);
    }

    void setCompleteMap2Progress()
    {
        float map2CompleteProgress = uIMapManager.getProgressMap(2);
        uIMenuManager.setCompleteMap2Progress(map2CompleteProgress);
    }

    void setItemCollectedUI(float plusAmount)
    {
        itemCollectedAmount += plusAmount;
        uIPlayGame.setItemCollectedUI(itemCollectedAmount);
        uIMenuManager.setItemCollectedUI(itemCollectedAmount, itemCollectableAmount);
    }

    void setKilledCreepUI(float plusAmount)
    {
        killedCreepAmount += plusAmount;
        uIMenuManager.setKilledCreepUI(killedCreepAmount);
    }

    void setAbilityProgressUI()
    {
        uIMenuManager.setAbilityProgressUI();
    }

    void setActiveAttack(bool state)
    {
        uIPlayGame.setActiveAttack(state);
    }

    void setActiveDash(bool state)
    {
        uIPlayGame.setActiveDash(state);
    }

    // void setInactiveItem(int[] listInactiveItem, string nameItem)
    // {
    //     itemManager.setActiveItem(listInactiveItem, nameItem);
    // }

    // void setInactiveKeyOrDoor(int[] listInactiveItem, string nameItem){
    //     keyDoorManager.setActiveItem(listInactiveItem, nameItem);
    // }

    // InputManager
    void setActiveInputManagerAfterDeactiveUIGuide()
    {
        inputManager.enabled = true;
        player.setBool("FirstTakeItem", false);
        player.setDeactiveCurrentItemIsTaking();
    }

    // Map
    void setActiveMap1()
    {
        Map1.SetActive(true);
        Map2.SetActive(false);
    }

    void setActiveMap2()
    {
        Map1.SetActive(false);
        Map2.SetActive(true);
    }

    string getHealthAndBaseHealth()
    {
        return player.health.ToString() + "/" + player.baseHealth.ToString();
    }

    string getManaAndBaseMana()
    {
        return player.mana.ToString() + "/" + player.baseMana.ToString();
    }

    float getCompleteMap1Percent()
    {
        return uIMapManager.getProgressMap(1);
    }

    float getCompleteMap2Percent()
    {
        return uIMapManager.getProgressMap(2);
    }

    string getItemCollectedPercent()
    {
        return itemCollectedAmount.ToString() + "/" + itemCollectableAmount.ToString();
    }

    float getKilledCreep()
    {
        return killedCreepAmount;
    }

    public int[] getListInactiveItem(string nameItem)
    {
        return itemManager.getListInactiveItem(nameItem);
    }

    public int[] getListInactiveKeyOrDoor(string nameItem)
    {
        return keyDoorManager.getListInactiveKeyOrDoor(nameItem);
    }

    float getAbilityCompletePercent()
    {
        abilityCompletePercent = 0;
        if (player.attackEnable)
        {
            abilityCompletePercent += 1;
        }
        if (player.wallJumpEnable)
        {
            abilityCompletePercent += 1;
        }
        if (player.dashEnable)
        {
            abilityCompletePercent += 1;
        }
        if (player.chargeFlameEnable)
        {
            abilityCompletePercent += 1;
        }
        return abilityCompletePercent * 25;
    }

    void shakeCamera()
    {
        cameraController.ShakeCamera();
    }

    void notiAttackFail()
    {
        uIPlayGame.NotiAttackFail();
    }
    private void setResolution(string value)
    {
        if (value.Equals("qHD"))
        {
            Screen.SetResolution(960, 540, true);
        }
        else if (value.Equals("HD"))
        {
            Screen.SetResolution(1280, 720, true);
        }
        else if (value.Equals("fullHD"))
        {
            Screen.SetResolution(1920, 1080, true);
        }
    }
    void setStatePlayerDeath()
    {
        uIEventSystem.InactiveAll();
        joystick.StopMovement();
        uIEventSystem.ActiveUIDie(2f);
    }
    void closeDoor(string gate)
    {
        if (gate.Equals("GateMap2-1"))
        {
            doors[0].setStateLockDoor(false);
        }
        else if (gate.Equals("GateMap2-2"))
        {
            doors[1].setStateLockDoor(false);
        }
        else if (gate.Equals("GateMap1-1"))
        {   
            doors[2].setStateLockDoor(false);
        }
        else if (gate.Equals("GateMap1-2"))
        {
            doors[3].setStateLockDoor(false);
        } 
    }
    public void LoadSceneAfterDie()
    {
        StartCoroutine(waitLoadSceneAfterDie());
    }
    IEnumerator waitLoadSceneAfterDie()
    {
        yield return new WaitForSeconds(2f);
        //string path = Application.persistentDataPath + "/player.data";
        SceneManager.LoadSceneAsync("MainGame");
    }
}
