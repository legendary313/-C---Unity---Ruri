using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyDoorManager : MonoBehaviour
{
    [SerializeField] GameObject[] listKey;

    public Action<string> setActiveHaveKeyUI;

    private void setAllActive(GameObject[] listObject){
        foreach(GameObject obj in listObject){
            obj.SetActive(true);
        }
    }

    public int[] getListInactiveKeyOrDoor(string nameItem){
        if (nameItem.Equals("Key")){
            return getListInactiveItem(listKey);
        }else{
            Debug.Log("Wrong name: Key or Door");
            return null;
        }
    }

    public void setInactiveItem(int[] listInactiveItem, string nameItem){
        if (nameItem.Equals("Key")){
            setInactiveItem(listInactiveItem, listKey);
        }
    }

    public void setKeyUI(){
        for (int i = 0; i < listKey.Length; i++){
            if (!listKey[i].activeInHierarchy){
                string keyType = listKey[i].GetComponent<Key>().GetKeyType().ToString();
                setActiveHaveKeyUI(keyType);
            }
        }
    }

    public void loadDataForKeyHolder(KeyHolder keyHolder){
        for(int i = 0; i < listKey.Length; i++){
            if (!listKey[i].activeInHierarchy){
                Key.KeyType keyType = listKey[i].GetComponent<Key>().GetKeyType();
                keyHolder.AddKey(keyType);
            }
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
}
