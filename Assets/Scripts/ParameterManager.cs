using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Game Parameter", menuName="Game Parameter")]
public class ParameterManager : ScriptableObject
{
    public Transform[] startPointMap1;
    public Transform[] startPointMap2;
    public float gateMap;
    private void Awake()
    {
        gateMap = PlayerPrefs.GetFloat("PlayerPosition");
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
