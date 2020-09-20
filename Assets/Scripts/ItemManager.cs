using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject[] listDiamond;
    [SerializeField] GameObject[] listHealthPotion;
    [SerializeField] GameObject[] listFullHealthPotion;
    [SerializeField] GameObject[] listManaPotion;
    [SerializeField] GameObject[] listFullManaPotion;
    [SerializeField] GameObject[] listAbilityEnable;

    private void setAllActive(GameObject[] listObject){
        foreach(GameObject obj in listObject){
            obj.SetActive(true);
        }
    }

    public int[] getListInactiveItem(string nameItem){
        if (nameItem.Equals("Diamond")){
            return getListInactiveItem(listDiamond);
        }else if (nameItem.Equals("HealthPotion")){
            return getListInactiveItem(listHealthPotion);
        }else if (nameItem.Equals("FullHealthPotion")){
            return getListInactiveItem(listFullHealthPotion);
        }else if (nameItem.Equals("ManaPotion")){
            return getListInactiveItem(listManaPotion);
        }else if (nameItem.Equals("FullManaPotion")){
            return getListInactiveItem(listFullManaPotion);
        }else if (nameItem.Equals("AbilityEnable")){
            return getListInactiveItem(listAbilityEnable);
        }else{
            Debug.Log("Wrong nameItem to getListInactiveItem" + nameItem);
            return null;
        }
    }

    public void setInactiveItem(int[] listInactiveItem, string nameItem){
        if (nameItem.Equals("Diamond")){
            setInactiveItem(listInactiveItem, listDiamond);
        }else if (nameItem.Equals("HealthPotion")){
            setInactiveItem(listInactiveItem, listHealthPotion);
        }else if (nameItem.Equals("FullHealthPotion")){
            setInactiveItem(listInactiveItem, listFullHealthPotion);
        }else if (nameItem.Equals("ManaPotion")){
            setInactiveItem(listInactiveItem, listManaPotion);
        }else if (nameItem.Equals("FullManaPotion")){
            setInactiveItem(listInactiveItem, listFullManaPotion);
        }else if (nameItem.Equals("AbilityEnable")){
            setInactiveItem(listInactiveItem, listAbilityEnable);
        }else{
            Debug.Log("Wrong nameItem to setActiveItem");
        }
    }

    private int[] getListInactiveItem(GameObject[] objects){
        List<int> indexPosition = new List<int>();
        for (int i = 0; i < objects.Length; i++){
            if (!objects[i].activeSelf){
                indexPosition.Add(i);
            }
        }
        return indexPosition.ToArray();
    }

    private void setInactiveItem(int[] listInactiveItem, GameObject[] objects){
        setAllActive(objects);
        foreach(int index in listInactiveItem){
            objects[index].SetActive(false);
        }
    }

    // HeathPotion
}
