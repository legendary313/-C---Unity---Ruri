using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeMap : MonoBehaviour
{
    float curPosition = 0;
    [Header("Start Position")]
    public Transform[] changeMap1Point; // 2point
    public Transform[] changeMap2Point; // 2point
    [SerializeField] Image loadingFill;
    [SerializeField] TextMeshProUGUI loadingText;
    [Header("UI")]
    [SerializeField] UIEventSystem uIEventSystem;
    [SerializeField] FloatingJoystick joystick;

    PlayerController player;
    public Action<string> closeDoor;
    string strMap = "";

    private void Awake()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
        uIEventSystem.enableChangeMap = enableChangeMap;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("GateMap2-1"))
        {
            doUIChangeMap();
            strMap = other.gameObject.name;
            // PlayerPrefs.SetInt("PassToMap", 1);
            // PlayerPrefs.SetInt("PlayerPosition", 1);
            // player.setActiveMap1();
            // TeleportToLocation(changeMap1Point[0].position);
            // PlayerPrefs.SetInt("PlayingMap", 1);
        }
        else if (other.gameObject.name.Equals("GateMap2-2"))
        {
            doUIChangeMap();
            strMap = other.gameObject.name;
            // PlayerPrefs.SetInt("PassMap", 1);
            // PlayerPrefs.SetInt("PlayerPosition", 2);
            // player.setActiveMap1();
            // TeleportToLocation(changeMap1Point[1].position);
            // PlayerPrefs.SetInt("PlayingMap", 1);
        }
        else if (other.gameObject.name.Equals("GateMap1-1"))
        {   
            doUIChangeMap();
            strMap = other.gameObject.name;
            // PlayerPrefs.SetInt("PassMap", 2);
            // PlayerPrefs.SetInt("PlayerPosition", 1);
            // player.setActiveMap2();
            // TeleportToLocation(changeMap2Point[0].position);
            // PlayerPrefs.SetInt("PlayingMap", 2);
        }
        else if (other.gameObject.name.Equals("GateMap1-2"))
        {
            doUIChangeMap();
            strMap = other.gameObject.name;
            // PlayerPrefs.SetInt("PassMap", 2);
            // PlayerPrefs.SetInt("PlayerPosition", 2);
            // player.setActiveMap2();
            // TeleportToLocation(changeMap2Point[1].position);
            // PlayerPrefs.SetInt("PlayingMap", 2);
        }
    }

    void TeleportToLocation(Vector3 location){
        gameObject.transform.parent.gameObject.transform.position = location;
    }
    void doUIChangeMap()
    {
        uIEventSystem.InactiveAll();
        joystick.StopMovement();
        uIEventSystem.ActiveUIChangeMap(6f);
    }
    void enableChangeMap()
    {
        playerChangeMap();
    }
    void playerChangeMap()
    {
       if (strMap.Equals("GateMap2-1"))
        {
            PlayerPrefs.SetInt("PassToMap", 1);
            PlayerPrefs.SetInt("PlayerPosition", 1);
            player.setActiveMap1();
            TeleportToLocation(changeMap1Point[0].position);
            PlayerPrefs.SetInt("PlayingMap", 1);
            closeDoor(strMap);
        }
        else if (strMap.Equals("GateMap2-2"))
        {
            PlayerPrefs.SetInt("PassMap", 1);
            PlayerPrefs.SetInt("PlayerPosition", 2);
            player.setActiveMap1();
            TeleportToLocation(changeMap1Point[1].position);
            PlayerPrefs.SetInt("PlayingMap", 1);
            closeDoor(strMap);
        }
        else if (strMap.Equals("GateMap1-1"))
        {   
            PlayerPrefs.SetInt("PassMap", 2);
            PlayerPrefs.SetInt("PlayerPosition", 1);
            player.setActiveMap2();
            TeleportToLocation(changeMap2Point[0].position);
            PlayerPrefs.SetInt("PlayingMap", 2);
            closeDoor(strMap);
        }
        else if (strMap.Equals("GateMap1-2"))
        {
            PlayerPrefs.SetInt("PassMap", 2);
            PlayerPrefs.SetInt("PlayerPosition", 2);
            player.setActiveMap2();
            TeleportToLocation(changeMap2Point[1].position);
            PlayerPrefs.SetInt("PlayingMap", 2);
            closeDoor(strMap);
        } 
    }
    // IEnumerator LoadAsynchronouslyGameScene(string map)
    // {
    //     uIEventSystem.ScreenWhiteToBlack();
    //     //yield return new WaitUntil(() => uIEventSystem.chekcBlackBGFinish);
    //     while (!uIEventSystem.chekcBlackBGFinish)
    //     {
    //         Debug.Log(uIEventSystem.chekcBlackBGFinish);
    //         yield return null;
    //     }
    //     Debug.Log(uIEventSystem.chekcBlackBGFinish);
    //     uIEventSystem.ActiveUILoadScene();
    //     AsyncOperation operation = SceneManager.LoadSceneAsync(map);
    //     while (!operation.isDone)
    //     {
    //         float progress = operation.progress / 0.9f;
    //         //Debug.Log(progress);
    //         loadingFill.fillAmount = progress;
    //         loadingText.text = "Loading " + Mathf.Round(progress * 100).ToString() + "%";
    //         yield return null;
    //     }
    //     //yield return new WaitForSeconds(3f);
    // }
}
