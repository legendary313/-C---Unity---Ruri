using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementData
{
    // public float mapCompletePercent;
    public float abilityCompletePercent;
    public float itemCollectedAmount;
    public float killedCreepAmount;
    public float completeMap1Progress;
    public float completeMap2Progress;

    public AchievementData(GameManager gameManager){
        // mapCompletePercent = gameManager.mapCompletePercent;
        abilityCompletePercent = gameManager.abilityCompletePercent;
        itemCollectedAmount = gameManager.itemCollectedAmount;
        killedCreepAmount = gameManager.killedCreepAmount;
        completeMap1Progress = gameManager.completeMap1Progress;
        completeMap2Progress = gameManager.completeMap2Progress;
    }
    
}
