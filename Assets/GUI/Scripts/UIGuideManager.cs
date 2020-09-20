using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIGuideManager : MonoBehaviour
{
    enum nameOfUI
    {
        NormalAttack, WallJump, Dash, ChargeFlame, HealthPotion, FullHealthPotion, ManaPotion, FullManaPotion,
        Item,
    }
    [SerializeField] GameObject UINormalAttack;
    [SerializeField] GameObject UIWallJump;
    [SerializeField] GameObject UIDash;
    [SerializeField] GameObject UIChargeFlame;
    [SerializeField] GameObject UIHealthPotion;
    [SerializeField] GameObject UIFullHealthPotion;
    [SerializeField] GameObject UIManaPotion;
    [SerializeField] GameObject UIFullManaPotion;
    [SerializeField] GameObject UIItem;
    [SerializeField] GameObject UINotHaveRedKey;
    [SerializeField] GameObject UINotHaveGreenKey;
    [SerializeField] GameObject UINotHaveBlueKey;
    [SerializeField] GameObject UINotHaveJikanStone;
    public Action afterSetInactiveUI; // active inputmanager
    bool UIGuideIsActive;
    string nameCurrentUI;

    void Awake()
    {
        UIGuideIsActive = false;
    }

    void Update()
    {
        if (UIGuideIsActive)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    setInactiveUI(nameCurrentUI);
                    UIGuideIsActive = false;
                }
            }
        }
    }

    void setInactiveUI(string nameCurrentUI)
    {
        if (nameCurrentUI.Equals(nameOfUI.NormalAttack.ToString()))
        {
            setActiveUINormalAttack(false);
        }
        else if (nameCurrentUI.Equals(nameOfUI.WallJump.ToString()))
        {
            setActiveUIWallJump(false);
        }
        else if (nameCurrentUI.Equals(nameOfUI.Dash.ToString()))
        {
            setActiveUIDash(false);
        }
        else if (nameCurrentUI.Equals(nameOfUI.ChargeFlame.ToString()))
        {
            setActiveUIChargeFlame(false);
        }
        else if (nameCurrentUI.Equals(nameOfUI.HealthPotion.ToString()))
        {
            setActiveUIHealthPotion(false);
        }
        else if (nameCurrentUI.Equals(nameOfUI.FullHealthPotion.ToString()))
        {
            setActiveUIFullHealthPotion(false);
        }
        else if (nameCurrentUI.Equals(nameOfUI.ManaPotion.ToString()))
        {
            setActiveUIManaPotion(false);
        }
        else if (nameCurrentUI.Equals(nameOfUI.FullManaPotion.ToString()))
        {
            setActiveUIFullManaPotion(false);
        }
        else if (nameCurrentUI.Equals(nameOfUI.Item.ToString()))
        {
            setActiveUIItem(false);
        }
    }

    public void setActiveUINormalAttack(bool state)
    {
        UINormalAttack.SetActive(state);
        // if (state){
        //     StartCoroutine(waitToSetInactive(UINormalAttack));
        // }
        if (!state)
        {
            afterSetInactiveUI();
        }
        else
        {
            nameCurrentUI = nameOfUI.NormalAttack.ToString();
            UIGuideIsActive = true;
        }
    }

    public void setActiveUIWallJump(bool state)
    {
        UIWallJump.SetActive(state);
        // if (state){
        //     StartCoroutine(waitToSetInactive(UIWallJump));
        // }
        if (!state)
        {
            afterSetInactiveUI();
        }
        else
        {
            nameCurrentUI = nameOfUI.WallJump.ToString();
            UIGuideIsActive = true;
        }
    }

    public void setActiveUIDash(bool state)
    {
        UIDash.SetActive(state);
        // if (state){
        //     StartCoroutine(waitToSetInactive(UIDash));
        // }
        if (!state)
        {
            afterSetInactiveUI();
        }
        else
        {
            nameCurrentUI = nameOfUI.Dash.ToString();
            UIGuideIsActive = true;
        }
    }

    public void setActiveUIChargeFlame(bool state)
    {
        UIChargeFlame.SetActive(state);
        // if (state){
        //     StartCoroutine(waitToSetInactive(UIChargeFlame));
        // }
        if (!state)
        {
            afterSetInactiveUI();
        }
        else
        {
            nameCurrentUI = nameOfUI.ChargeFlame.ToString();
            UIGuideIsActive = true;
        }
    }

    public void setActiveUIHealthPotion(bool state)
    {
        UIHealthPotion.SetActive(state);
        if (!state)
        {
            afterSetInactiveUI();
        }
        else
        {
            nameCurrentUI = nameOfUI.HealthPotion.ToString();
            UIGuideIsActive = true;
        }
    }

    public void setActiveUIFullHealthPotion(bool state)
    {
        UIFullHealthPotion.SetActive(state);
        if (!state)
        {
            afterSetInactiveUI();
        }
        else
        {
            nameCurrentUI = nameOfUI.FullHealthPotion.ToString();
            UIGuideIsActive = true;
        }
    }

    public void setActiveUIManaPotion(bool state)
    {
        UIManaPotion.SetActive(state);
        if (!state)
        {
            afterSetInactiveUI();
        }
        else
        {
            nameCurrentUI = nameOfUI.ManaPotion.ToString();
            UIGuideIsActive = true;
        }
    }

    public void setActiveUIFullManaPotion(bool state)
    {
        UIFullManaPotion.SetActive(state);
        if (!state)
        {
            afterSetInactiveUI();
        }
        else
        {
            nameCurrentUI = nameOfUI.FullManaPotion.ToString();
            UIGuideIsActive = true;
        }
    }

    public void setActiveUIItem(bool state)
    {
        UIItem.SetActive(state);
        if (!state)
        {
            afterSetInactiveUI();
        }
        else
        {
            nameCurrentUI = nameOfUI.Item.ToString();
            UIGuideIsActive = true;
        }
    }

    IEnumerator waitToSetInactive(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        obj.SetActive(false);
    }

    public void setActiveNotHaveRedKey(bool state)
    {
        UINotHaveRedKey.SetActive(state);
    }

    public void setActiveNotHaveGreenKey(bool state)
    {
        UINotHaveGreenKey.SetActive(state);
    }

    public void setActiveNotHaveBlueKey(bool state)
    {
        UINotHaveBlueKey.SetActive(state);
    }

    public void setActiveNotHaveJikanStone(bool state)
    {
        UINotHaveJikanStone.SetActive(state);
    }
}
