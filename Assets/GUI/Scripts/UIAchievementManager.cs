using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIAchievementManager : MonoBehaviour
{
    [SerializeField] GameObject prefabsUITextRow;
    [Header("Icon")]
    [SerializeField] Sprite[] icon;
    [Header("Value")]
    public string[] title;

    [HideInInspector] public List<RowLabel> curPrefab = new List<RowLabel>();
    RowLabel line;
    UIListRowText uIListRowText;
    public delegate float GetCompleteMap1Percent();
    public GetCompleteMap1Percent getCompleteMap1PercentDel;
    public delegate float GetCompleteMap2Percent();
    public GetCompleteMap2Percent getCompleteMap2PercentDel;
    public delegate string GetItemCollectedPercent();
    public GetItemCollectedPercent getItemCollectedPercentDel;
    public delegate float GetKilledCreepAmount();
    public GetKilledCreepAmount getKilledCreepAmountDel;
    public delegate float GetCompleteAbilityPercent();
    public GetCompleteAbilityPercent getCompleteAbilityPercentDel;

    void Start()
    {
        for (int i = 0; i < icon.Length; i++)
        {
            line.index = i;
            line.obj = Instantiate(prefabsUITextRow, Vector3.zero, Quaternion.identity, gameObject.transform);
            if (i == 0)
            {
                string completeMap1Percent = Math.Round((getCompleteMap1PercentDel() * 100), 2).ToString();
                line.title = ("Map1 " + completeMap1Percent + "%").Split(':');
            }else if (i == 1){
                string completeMap2Percent = Math.Round((getCompleteMap2PercentDel() * 100), 2).ToString();
                line.title = ("Map2 " + completeMap2Percent + "%").Split(':');
            }
            else if (i == 2)
            {
                string abilityCompletePercent = getCompleteAbilityPercentDel().ToString();
                line.title = ("Ability Progress  " + abilityCompletePercent + "%").Split(':');
            }
            else if (i == 3)
            {
                string itemCollectedPercent = getItemCollectedPercentDel();
                line.title = ("Item Collected  " + itemCollectedPercent).Split(':');
            }
            else if (i == 4)
            {
                float killedCreepAmount = getKilledCreepAmountDel();
                line.title = ("Killed Creep  " + killedCreepAmount.ToString()).Split(':');
            }
            line.icon = icon[i];
            curPrefab.Add(line);
            uIListRowText = curPrefab[i].obj.GetComponent<UIListRowText>();
            string content = "";
            foreach (string item in curPrefab[i].title)
            {
                content += item + " ";
            }
            uIListRowText.setText(content);
            uIListRowText.setIcon(curPrefab[i].icon);
        }
    }

    public void setMap1CompleteProgress(float percent)
    {
        if (curPrefab.Count != 0){
            string completeMap1Percent = Math.Round((percent * 100), 2).ToString();
            curPrefab[0].obj.GetComponent<UIListRowText>().setText("Map1  " + completeMap1Percent + "%");
        }
    }

    public void setMap2CompleteProgress(float percent)
    {
        if (curPrefab.Count != 0){
            string completeMap2Percent = Math.Round((percent * 100), 2).ToString();
            curPrefab[1].obj.GetComponent<UIListRowText>().setText("Map2  " + completeMap2Percent + "%");
        }
    }

    public void setAbilitiesProgress()
    {
        if (curPrefab.Count != 0)
        {
            string abilityCompletePercent = getCompleteAbilityPercentDel().ToString();
            curPrefab[2].obj.GetComponent<UIListRowText>().setText("Ability Progress  " + abilityCompletePercent + "%");
        }
    }

    public void setItemCollected(float currentAmount, float total)
    {
        if (curPrefab.Count != 0)
        {
            curPrefab[3].obj.GetComponent<UIListRowText>().setText("Item Collected  "
                                                            + currentAmount.ToString()
                                                            + "/"
                                                            + total.ToString()
                                                            );
        }
    }


    public void setKilledCreepUI(float amount)
    {
        if (curPrefab.Count != 0)
        {
            curPrefab[4].obj.GetComponent<UIListRowText>().setText("Killed Creep  " + amount.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
