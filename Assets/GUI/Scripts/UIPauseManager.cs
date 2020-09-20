using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseManager : MonoBehaviour
{
    [SerializeField] Toggle qHD_toggle;
    [SerializeField] Toggle HD_toggle;
    [SerializeField] Toggle fullHD_toggle;
    public Action<string> setResolution;
    void Start()
    {
        setResolution("qHD");
    }

    // Update is called once per frame
    void Update()
    {
        //CheckToggleValue();
    }
    public void qHD_ClickButton()
    {
        if (qHD_toggle.isOn)
        {
            //Debug.Log("qHD");
            setResolution("qHD");
        }
    }
    public void HD_ClickButton()
    {
        if (HD_toggle.isOn)
        {
            //Debug.Log("HD");
            setResolution("HD");
        }
    }
    public void fullHD_ClickButton()
    {
        if (fullHD_toggle.isOn)
        {
            //Debug.Log("fullHD");
            setResolution("fullHD");
        }
    }
}
