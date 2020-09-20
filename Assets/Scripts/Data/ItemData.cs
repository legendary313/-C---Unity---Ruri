using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int[] listInactiveDiamond;
    public int[] listInactiveHealthPotion;
    public int[] listInactiveFullHealthPotion;
    public int[] listInactiveManaPotion;
    public int[] listInactiveFullManaPotion;
    public int[] listInactiveAbilityEnable;
    public int[] listInactiveKey;
    public string listOfPointInMiniMap1;
    public string listOfPointInMiniMap2;

    public ItemData(ItemManager itemManager, UIMapManager uIMapManager, KeyDoorManager keyDoorManager){
        listInactiveDiamond = itemManager.getListInactiveItem("Diamond");
        listInactiveHealthPotion = itemManager.getListInactiveItem("HealthPotion");
        listInactiveFullHealthPotion = itemManager.getListInactiveItem("FullHealthPotion");
        listInactiveManaPotion = itemManager.getListInactiveItem("ManaPotion");
        listInactiveFullManaPotion = itemManager.getListInactiveItem("FullManaPotion");
        listInactiveAbilityEnable = itemManager.getListInactiveItem("AbilityEnable");
        listInactiveKey = keyDoorManager.getListInactiveKeyOrDoor("Key");
        listOfPointInMiniMap1 = uIMapManager.getDataMinimap(1);
        listOfPointInMiniMap2 = uIMapManager.getDataMinimap(2);
    }
}
