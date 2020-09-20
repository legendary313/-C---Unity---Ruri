using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.Rendering.Universal
public class LightManager : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] GameObject nightLight;
    [SerializeField] GameObject dayLight;
    [SerializeField] GameObject caveLight;
    [SerializeField] GameObject playerLight;
    [SerializeField] GameObject godTreeLight;
    [Header("Effect")]
    [SerializeField] GameObject effectNight;
    [Header("Player Sight")]
    [SerializeField] PlayerSight playerSight;
    int lightState = -1;
    private void Awake()
    {
        playerSight.getValueLight = getValueLight;
    }
    void Start()
    {
        //godTreeLight.GetComponent<Light2D>();
        //onNightLight();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void getValueLight(string value)
    {
        if (value.Equals("SunLightTrigger") && lightState != 0)
        {
            onDayLight();
            return;
        }
        if (value.Equals("CaveLightTrigger") && lightState != 2)
        {
            onCaveLight();
            return;
        }
        if (value.Equals("NightLightTrigger") && lightState != 1)
        {
            onNightLight();
            return;
        }
    }
    public void onNightLight()
    {
        caveLight.SetActive(false);
        dayLight.SetActive(false);
        nightLight.SetActive(true);
        playerLight.SetActive(true);
        effectNight.SetActive(true);
        godTreeLight.SetActive(true);
        lightState = 1;
        nightLight.GetComponent<Animator>().SetTrigger("On");
    }
    public void onDayLight()
    {
        nightLight.SetActive(false);
        caveLight.SetActive(false);
        dayLight.SetActive(true);
        playerLight.SetActive(false);
        effectNight.SetActive(false);
        godTreeLight.SetActive(false);
        lightState = 0;
        dayLight.GetComponent<Animator>().SetTrigger("On");
        StartCoroutine(waitDaytoNight(100f));
    }
    public void onCaveLight()
    {
        nightLight.SetActive(false);
        dayLight.SetActive(false);
        godTreeLight.SetActive(false);
        caveLight.SetActive(true);
        playerLight.SetActive(true);
        effectNight.SetActive(true);
        lightState = 2;
    }
    IEnumerator waitDaytoNight(float time)
    {
        yield return new WaitForSeconds(time);
        if (lightState == 0)
        {
            onNightLight();
        }
    }
}
