using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Player Parameter
    public float health;
    public float mana;
    // Player's Skill
    public bool attackEnable;
    public bool wallJumpEnable;
    public bool dashEnable;
    public bool chargeFlameEnable;
    // Map where player is playing
    public int playingMap;
    // Position
    public float[] position;
    // State
    public bool healthPotionIsTaken;
    public bool fullHealthPotionIsTaken;
    public bool manaPotionIsTaken;
    public bool fullManaPotionIsTaken;
    public bool itemIsTaken;
    public string godTreeData;
    public bool tutorialMapEnable;
    // Achievement
    // public float mapCompletePercent;
    public float abilityCompletePercent;
    public float itemCollectedAmount;
    public float killedCreepAmount;
    public float completeMap1Progress;
    public float completeMap2Progress;

    // // Item
    // public int[] listInactiveDiamond;
    // public int[] listInactiveHealthPotion;
    // public int[] listInactiveFullHealthPotion;
    // public int[] listInactiveManaPotion;
    // public int[] listInactiveFullManaPotion;
    // public int[] listInactiveAbilityEnable;

    public GameData (PlayerData player, AchievementData achievement){
        // Player Parameter
        health = player.health;
        mana = player.mana;
        // Skill
        attackEnable = player.attackEnable;
        wallJumpEnable = player.wallJumpEnable;
        dashEnable = player.dashEnable;
        chargeFlameEnable = player.chargeFlameEnable;
        //Playing Map
        playingMap = PlayerPrefs.GetInt("PlayingMap");
        // Position
        position = new float[3];
        position[0] = player.position[0];
        position[1] = player.position[1];
        position[2] = player.position[2];
        // States
        healthPotionIsTaken = player.healthPotionIsTaken;
        fullHealthPotionIsTaken = player.fullHealthPotionIsTaken;
        manaPotionIsTaken = player.manaPotionIsTaken;
        fullManaPotionIsTaken = player.fullManaPotionIsTaken;
        itemIsTaken = player.itemIsTaken;
        godTreeData = player.godTreeData;
        tutorialMapEnable = player.tutorialMapEnable;


        // Achievement
        // mapCompletePercent = achievement.mapCompletePercent;
        abilityCompletePercent = achievement.abilityCompletePercent;
        itemCollectedAmount = achievement.itemCollectedAmount;
        killedCreepAmount = achievement.killedCreepAmount;
        completeMap1Progress = achievement.completeMap1Progress;
        completeMap2Progress = achievement.completeMap2Progress;
        // Item
        // Location of inactive Item;
        // listInactiveDiamond = itemData.listInactiveDiamond;
        // listInactiveHealthPotion = itemData.listInactiveHealthPotion;
        // listInactiveFullHealthPotion = itemData.listInactiveFullHealthPotion;
        // listInactiveManaPotion = itemData.listInactiveManaPotion;
        // listInactiveFullManaPotion = itemData.listInactiveFullManaPotion;
        // listInactiveAbilityEnable = itemData.listInactiveAbilityEnable;
    }
}
