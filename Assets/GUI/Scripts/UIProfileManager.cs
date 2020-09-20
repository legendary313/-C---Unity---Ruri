using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIProfileManager : MonoBehaviour
{
    [SerializeField] GameObject prefabsUITextRow;
    [Header("Icon")]
    [SerializeField] Sprite[] icon;
    [Header("Value")]
    public string[] title;

    [HideInInspector] public List<RowLabel> curPrefab = new List<RowLabel>();
    RowLabel line;
    UIListRowText uIListRowText;

    public delegate string GetHeathAndBaseHealth();
    public GetHeathAndBaseHealth getHeathAndBaseHealth;
    public delegate string GetManaAndBaseMana();
    public GetManaAndBaseMana getManaAndBaseMana;
    void Start()
    {
        for (int i = 0; i < icon.Length; i++)
        {
            line.index = i;
            line.obj = Instantiate(prefabsUITextRow, Vector3.zero, Quaternion.identity, gameObject.transform);
            if (i == 1)
            {
                line.title = ("HP  " + getHeathAndBaseHealth()).Split(':');
            }
            else if (i == 2)
            {
                line.title = ("MP  " + getManaAndBaseMana()).Split(':');
            }
            else
            {
                line.title = title[i].Split(':');
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

    public void setHealthUI(float health, float baseHealth)
    {
        if (curPrefab.Count != 0)
        {
            uIListRowText = curPrefab[1].obj.GetComponent<UIListRowText>();
            uIListRowText.setText("HP  " + health.ToString() + "/" + baseHealth);
        }
    }

    public void setManaUI(float mana, float baseMana)
    {
        if (curPrefab.Count != 0)
        {
            uIListRowText = curPrefab[2].obj.GetComponent<UIListRowText>();
            uIListRowText.setText("MP:  " + mana.ToString() + "/" + baseMana.ToString());
        }
    }
}
