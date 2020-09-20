using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class UIQuestManager : MonoBehaviour
{
    public TextMeshProUGUI textMainQuest;
    public TextMeshProUGUI textNameMainQuest;
    public TextMeshProUGUI textSubQuest;
    public TextMeshProUGUI textNameSubQuest;
    int countJikanActived,countSpiritActived;
    // Update is called once per frame
    void Start()
    {
        loadDataSubQuest("1 1");
        textNameMainQuest.text = "Main Quest:";
        textNameSubQuest.text = "Sub Quest:";
        textMainQuest.text = "Find the Water for Ivern.";
        textSubQuest.text = "Actived Jikan: " + countJikanActived.ToString() + "/2\nActived Spirit: "+countSpiritActived.ToString()+"/3";
    }

    public void loadDataSubQuest(string data)
    {
        if(data.Equals(""))
        {
            countJikanActived = 0;
            countSpiritActived = 0;
        }
        else{
            string[] datas = data.Split(' ');
            countJikanActived = Int32.Parse(datas[0]);
            countSpiritActived = Int32.Parse(datas[1]);
        }
    }

    public string saveDataSubQuest()
    {
        string data = countJikanActived.ToString() + " " + countSpiritActived.ToString();
        return data;
    }
}
