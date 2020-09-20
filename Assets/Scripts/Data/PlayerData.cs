using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    // Player Parameter
    public float health;
    public float mana;
    // Player's Skill
    public bool attackEnable;
    public bool wallJumpEnable;
    public bool dashEnable;
    public bool chargeFlameEnable;
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

    public PlayerData(PlayerController player, GodTreeBehavior godTreeBehavior){
         // Player Parameter
        health = player.health;
        mana = player.mana;
        // Skill
        attackEnable = player.attackEnable;
        wallJumpEnable = player.wallJumpEnable;
        dashEnable = player.dashEnable;
        chargeFlameEnable = player.chargeFlameEnable;
        // Position
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        // State
        healthPotionIsTaken = player.healthPotionIsTaken;
        fullHealthPotionIsTaken = player.fullHealthPotionIsTaken;
        manaPotionIsTaken = player.manaPotionIsTaken;
        fullManaPotionIsTaken = player.fullManaPotionIsTaken;
        itemIsTaken = player.itemIsTaken;
        godTreeData = godTreeBehavior.getMeetGodTreeData();
        tutorialMapEnable = player.tutorialMapEnable;
    }
}
