using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIListRowText : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI tmp;
    // [HideInInspector] public Sprite icon = null;
    // [HideInInspector] public string tmpContent = "";
    // void Update()
    // {
    //     if (icon != null) img.sprite = icon;
    //     if (!tmpContent.Equals("")) tmp.text = tmpContent;
    // }

    public void setText(string content){
        tmp.text = content;
    }

    public void setIcon(Sprite icon){
        img.sprite = icon;
    }
}
