using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour
{
    public delegate string GetHealthAndBaseHealth();
    public GetHealthAndBaseHealth getHealthAndBaseHealthDel;
    public delegate string GetManaAndBaseMana();
    public GetManaAndBaseMana getManaAndBaseManaDel;
    public delegate float GetCompleteMap1Percent();
    public GetCompleteMap1Percent getCompleteMap1PercentDel;
    public delegate float GetCompleteMap2Percent();
    public GetCompleteMap2Percent getCompleteMap2PercentDel;
    public delegate string GetItemCollectablePercent();
    public GetItemCollectablePercent getItemCollectedPercentDel;
    public delegate float GetKilledCreepAmount();
    public GetKilledCreepAmount getKilledCreepAmountDel;
    public delegate float GetAbilityCompletePercent();
    public GetAbilityCompletePercent getAbilityCompletePercentDel;

    [SerializeField] UIProfileManager profileUI;
    [SerializeField] UIAchievementManager uIAchievement;

    void Start() {
        profileUI.getHeathAndBaseHealth = getHealthAndBaseHealth;
        profileUI.getManaAndBaseMana = getManaAndBaseMana;
        uIAchievement.getCompleteMap1PercentDel = getCompleteMap1Percent;
        uIAchievement.getCompleteMap2PercentDel = getCompleteMap2Percent;
        uIAchievement.getItemCollectedPercentDel = getItemCollectedPercent;
        uIAchievement.getKilledCreepAmountDel = getKilledCreepAmount;
        uIAchievement.getCompleteAbilityPercentDel = getAbilityCompletePercent;
    }
    
    public void setHealthUI(float health, float baseHealth){
        profileUI.setHealthUI(health, baseHealth);
    }

    public void setManaUI(float mana, float baseMana){
        profileUI.setManaUI(mana, baseMana);
    }

    public void setCompleteMap1Progress(float progress){
        uIAchievement.setMap1CompleteProgress(progress);
    }

    public void setCompleteMap2Progress(float progress){
        uIAchievement.setMap2CompleteProgress(progress);
    }

    public void setItemCollectedUI(float currentAmount, float total){
        uIAchievement.setItemCollected(currentAmount, total);
    }

    public void setKilledCreepUI(float amount){
        uIAchievement.setKilledCreepUI(amount);
    }

    public void setAbilityProgressUI(){
        uIAchievement.setAbilitiesProgress();
    }

    string getHealthAndBaseHealth(){
        return getHealthAndBaseHealthDel();
    }

    string getManaAndBaseMana(){
        return getManaAndBaseManaDel();
    }

    float getCompleteMap1Percent(){
        return getCompleteMap1PercentDel();
    }

    float getCompleteMap2Percent(){
        return getCompleteMap2PercentDel();
    }

    string getItemCollectedPercent(){
        return getItemCollectedPercentDel();
    }

    float getKilledCreepAmount(){
        return getKilledCreepAmountDel();
    }

    float getAbilityCompletePercent(){
        return getAbilityCompletePercentDel();
    }

}
