using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayGameManager : MonoBehaviour
{
    [Header("Boss")]
    [SerializeField] Image bossHealthFill;
    [SerializeField] Image bossHealthBlur;
    [SerializeField] GameObject UIBoss;
    [Header("Player")]
    [SerializeField] Image healthBar;
    [SerializeField] Image manaBar;
    [SerializeField] TextMeshProUGUI itemText;
    [SerializeField] GameObject notiAttack;
    [SerializeField] GameObject AttackButton;
    [SerializeField] GameObject DashButton;
    [SerializeField] GameObject UISave;
    [SerializeField] GameObject UINotiGameSaved;
    [SerializeField] GameObject UINotiCanNotSaveGame;
    [Header("CoolDown")]
    [SerializeField] Image cooldownAttack;
    [SerializeField] Image cooldownDash;
    [Header("Key")]
    [SerializeField] Image RedKey;
    [SerializeField] Image GreenKey;
    [SerializeField] Image Jikan1;
    [Header("Toturial")]
    [SerializeField] GameObject arrowUIMap;

    public void setActiveArrowUIMap(bool state){
        arrowUIMap.SetActive(state);
    }

    public void setActiveHaveKeyUI(string keyType){
        if (keyType.Equals("Red")){
            RedKey.color = new Color(RedKey.color.r, RedKey.color.g, RedKey.color.b, 255f);
        }else if (keyType.Equals("Green")){
            GreenKey.color = new Color(GreenKey.color.r, GreenKey.color.g, GreenKey.color.b, 255f);
        }else if (keyType.Equals("Jikan1")){
            Jikan1.color = new Color(Jikan1.color.r, Jikan1.color.g, Jikan1.color.b, 255f);
        }
    }

    // private void setAllActiveHaveKeyUI(string keyType){
    //     RedKey.color = new Color(RedKey.color.r, RedKey.color.g, RedKey.color.b, 255);
    //     GreenKey.color = new Color(GreenKey.color.r, GreenKey.color.g, GreenKey.color.b, 255);
    //     Jikan1.color = new Color(Jikan1.color.r, Jikan1.color.g, Jikan1.color.b, 255);
    // }

    public void setInactiveHaveKeyUI(string keyType){
        if (keyType.Equals("Red")){
            RedKey.color = new Color(RedKey.color.r, RedKey.color.g, RedKey.color.b, 50f);
        }else if (keyType.Equals("Green")){
            GreenKey.color = new Color(GreenKey.color.r, GreenKey.color.g, GreenKey.color.b, 50f);
        }else if (keyType.Equals("Jikan1")){
            Jikan1.color = new Color(Jikan1.color.r, Jikan1.color.g, Jikan1.color.b, 50f);
        }
    }

    public void setActiveUINotiGameSaved()
    {
        UINotiGameSaved.SetActive(true);
        StartCoroutine(waitToSetInactive(UINotiGameSaved));
    }

    public void setActiveUINotiCanNotSaveGame(){
        if (!UINotiCanNotSaveGame.activeInHierarchy)
        {
            UINotiCanNotSaveGame.SetActive(true);
        }
    }

    IEnumerator waitToSetInactive(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        obj.SetActive(false);
    }

    public void setActiveUISave(bool state)
    {
        UISave.SetActive(state);
    }

    public void setActiveAttack(bool state)
    {
        AttackButton.SetActive(state);
    }

    public void setActiveDash(bool state)
    {
        DashButton.SetActive(state);
    }

    public void NotiAttackFail()
    {
        if (!notiAttack.activeInHierarchy)
        {
            notiAttack.SetActive(true);
        }
    }

    public void setHealthUI(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }

    public void setManaUI(float manaPercent)
    {
        manaBar.fillAmount = manaPercent;
    }

    public void setItemCollectedUI(float amount)
    {
        itemText.SetText(amount.ToString());
    }
    public void setActiveUIBoss(bool val)
    {
        UIBoss.SetActive(val);
    }
    public void setHealthBoss(float healthPercent)
    {
        StartCoroutine(setHealthBlurBoss(bossHealthBlur.fillAmount, healthPercent));
        bossHealthFill.fillAmount = healthPercent;
    }
    IEnumerator setHealthBlurBoss(float healthCur, float healthNext)
    {
        while (healthCur > healthNext)
        {
            healthCur -= Time.deltaTime * 0.1f;
            bossHealthBlur.fillAmount = healthCur;
            yield return null;
        }
    }
    public void UICoolDownAttack(float timeCoolDown)
    {
        StartCoroutine(ImageCircleFillAmount(cooldownAttack, timeCoolDown));
        Debug.Log("Attack");
    }
    public void UICoolDownDash(float timeCoolDown)
    {
        StartCoroutine(ImageCircleFillAmount(cooldownDash, timeCoolDown));
        Debug.Log("Dash");
    }
    IEnumerator ImageCircleFillAmount(Image image, float time)
    {
        float baseTime = time;
        float progress = 1f;
        float timeLeft = Time.deltaTime;
        image.fillAmount = progress;
        while (progress > 0)
        {
            progress -= (1f / time) * timeLeft;
            image.fillAmount = Mathf.Lerp(0, 1, progress);
            yield return null;
        }
    }
    
}
