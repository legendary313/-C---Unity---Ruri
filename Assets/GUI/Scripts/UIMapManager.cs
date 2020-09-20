using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// public struct MinimapGame
// {
//     public GameObject darkArea;
//     public string state;
// };
public class UIMapManager : MonoBehaviour
{

    [Header("UI Main")]
    [SerializeField] UIEventSystem uiEventSystem;
    [Header("Map")]
    [SerializeField] Scrollbar mapHorizontal;
    [SerializeField] Scrollbar mapVertical;
    [Header("Minimap Manager")]
    [SerializeField] MinimapManager minimapManager;
    [Header("Minimap")]
    [Tooltip("Map 1")]
    [SerializeField] GameObject[] darkAreaObject1;
    [Tooltip("Map 2")]
    [SerializeField] GameObject[] darkAreaObject2;
    List<GameObject> darkAreaObjectMap1;
    List<GameObject> darkAreaObjectMap2;
    public float totalCheckPointMap1;
    public float totalCheckPointMap2;
    public float curCheckPointMap1;
    public float curCheckPointMap2;
    void Awake()
    {
        // chỉ cần save cái string là xong, chạy hàm SaveDataMinimap là nhận được 1 chuỗi để save  ./.
        // start game thì chạy hàm LoadDateMinimap(string) ./.
        // chuỗi để test!
        //string strTestMinimap = "Point (1)-1|Point (2)-1|Point (3)-1|Point (4)-1|Point (5)-1|Point (6)-1|Point (7)-1|Point (8)-1|Point (9)-1|Point (10)-1|Point (11)-1|Point (12)-1|Point (13)-1|Point (14)-1|Point (15)-1|Point (16)-1|Point (17)-1|Point (18)-1|";
        //string strTestMinimap = "0|0|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|0|0|1|1|0|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|1|";
        uiEventSystem.setIconPlayerCenterMinimap = setIconPlayerCenterMinimap;
        darkAreaObjectMap1 = new List<GameObject>();
        darkAreaObjectMap2 = new List<GameObject>();
        setBlackOnMap();
        totalCheckPointMap1 = darkAreaObjectMap1.Count;
        curCheckPointMap1 = 0;
        totalCheckPointMap2 = darkAreaObjectMap2.Count;
        curCheckPointMap2 = 0;
    }
    public void setBlackOnMap()
    {
        foreach (GameObject item in darkAreaObject1)
        {
            darkAreaObjectMap1.Add(item);
        }
        foreach (GameObject item in darkAreaObject2)
        {
            darkAreaObjectMap2.Add(item);
        }
    }
    void Update()
    {
        // curCheckPoint = calCurCheckPoint();
        // Debug.Log(curCheckPoint);
        // Debug.Log(SaveDataMinimap());
    }
    public void LoadDataMinimap(string minimapData, int index)
    {
        //Data form: Point (0)-0|Point (1)-1|
        //string pattern = @"|";
        //GameObject[] pointObject = GameObject.FindGameObjectsWithTag("MiniMap");
        // string[] arrData = minimapData.Split('|');
        // foreach (string item in arrData)
        // {
        //     if (!item.Equals(""))
        //     {
        //         string[] arrMinimapData = item.Split('-');
        //         foreach (GameObject obj in darkAreaObject)
        //         {
        //             if (obj.name.Equals(arrMinimapData[0]))
        //             {
        //                 MinimapGame temp;
        //                 temp.darkArea = obj;
        //                 temp.state = arrMinimapData[1];
        //                 darkAreaMinimap.Add(temp);
        //                 LoadMinimap(temp.darkArea, temp.state.Equals("0") ? false : true);
        //                 //Debug.Log(temp.darkArea + temp.state);
        //             }
        //         }
        //     }
        // }
        string[] arrData = minimapData.Split('|');
        // Debug.Log(minimapData);
        // Debug.Log("Size " + darkAreaObject.Count.ToString());
        int count = 0;
        if (index == 1)
        {
            foreach (string item in arrData)
            {
                if (!item.Equals(""))
                {
                    darkAreaObjectMap1[count].SetActive(item.Equals("0") ? false : true);
                    count++;
                }
            }
        }
        else
        {
            foreach (string item in arrData)
            {
                if (!item.Equals(""))
                {
                    darkAreaObjectMap2[count].SetActive(item.Equals("0") ? false : true);
                    count++;
                }
            }
        }

    }
    public float calCurCheckPoint(int index)
    {
        float count = 0;
        if (index == 1)
        {
            foreach (GameObject item in darkAreaObjectMap1)
            {
                if (!item.activeSelf)
                {
                    count++;
                }
            }
            return count;
        }
        else
        {
            foreach (GameObject item in darkAreaObjectMap2)
            {
                if (!item.activeSelf)
                {
                    count++;
                }
            }
            return count;
        }
    }
    public void LoadMinimap(GameObject point, bool state)
    {
        point.SetActive(state);
    }
    public string getDataMinimap(int index)
    {
        //GameObject[] pointObject = GameObject.FindGameObjectsWithTag("MiniMap");
        string data = "";
        if (index == 1)
        {
            foreach (GameObject item in darkAreaObjectMap1)
            {
                //data += item.name + "-" + (item.activeSelf ? "1" : "0") + "|";
                data += (item.activeSelf ? "1" : "0") + "|";
            }
            return data;
        }
        else
        {
            foreach (GameObject item in darkAreaObjectMap2)
            {
                //data += item.name + "-" + (item.activeSelf ? "1" : "0") + "|";
                data += (item.activeSelf ? "1" : "0") + "|";
            }
            return data;
        }
    }

    public float getProgressMap(int index)
    {
        if (index == 1)
        {
            return calCurCheckPoint(1) / totalCheckPointMap1;
        }
        else
        {
            return calCurCheckPoint(2) / totalCheckPointMap2;
        }
    }

    public void setIconPlayerCenterMinimap()
    {
        if (PlayerPrefs.GetInt("PlayingMap") == 1)
        {
            float[] posIconPlayer = minimapManager.getLocationIconPlayerMinimap(1);
            mapHorizontal.value = Mathf.Lerp(0, 1, posIconPlayer[0]);
            mapVertical.value = Mathf.Lerp(0, 1, posIconPlayer[1]);
        }
        else
        {
            float[] posIconPlayer = minimapManager.getLocationIconPlayerMinimap(2);
            mapHorizontal.value = Mathf.Lerp(0, 1, posIconPlayer[0]);
            mapVertical.value = Mathf.Lerp(0, 1, posIconPlayer[1]);
        }
    }
}
