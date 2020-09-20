using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDoor : MonoBehaviour {
    [SerializeField] private Key.KeyType keyType;

    public Key.KeyType GetKeyType() {
        return keyType;
    }

    public void OpenDoor() {
        gameObject.GetComponentInParent<DoorBehavior>().setStateLockDoor(true);
    }

    public void CloseDoor(){
        DoorBehavior door = gameObject.GetComponentInParent<DoorBehavior>();
        if (door != null) 
            door.setStateLockDoor(false);
    }

}
