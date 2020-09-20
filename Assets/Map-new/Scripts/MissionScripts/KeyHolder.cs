using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{

    public event EventHandler OnKeysChanged;

    private List<Key.KeyType> keyList;
    PlayerController player;

    private void Awake()
    {
        keyList = new List<Key.KeyType>();
        player = gameObject.GetComponentInParent<PlayerController>();
    }

    public List<Key.KeyType> GetKeyList()
    {
        return keyList;
    }

    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added Key: " + keyType);
        keyList.Add(keyType);
        OnKeysChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
        OnKeysChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Key"))
        {
            Key key = collider.gameObject.GetComponent<Key>();
            AddKey(key.GetKeyType());
            key.gameObject.SetActive(false);
            player.setActiveHaveKeyUI(key.GetKeyType().ToString());
        }
        else if (collider.CompareTag("Jikan"))
        {
            Jikan jikan = collider.GetComponent<Jikan>();
            if (ContainsKey(jikan.GetKeyType()))
            {
                jikan.ActiveJikan();
            }
            else
            {
                // Debug.Log("Don't have Jikan Key " + jikan.GetKeyType().ToString());
                player.setActiveNotHaveKeyUI(jikan.GetKeyType().ToString(), true);
            }
        }
        else if (collider.CompareTag("Door"))
        {
            KeyDoor keyDoor = collider.gameObject.GetComponent<KeyDoor>();
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                keyDoor.OpenDoor();
            }
            else
            {
                player.setActiveNotHaveKeyUI(keyDoor.GetKeyType().ToString(), true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Jikan"))
        {
            Jikan jikan = collider.GetComponent<Jikan>();
            if (!ContainsKey(jikan.GetKeyType()))
                player.setActiveNotHaveKeyUI(jikan.GetKeyType().ToString(), false);
        }
        else if (collider.CompareTag("Door"))
        {
            KeyDoor keyDoor = collider.gameObject.GetComponent<KeyDoor>();
            if (!ContainsKey(keyDoor.GetKeyType()))
                player.setActiveNotHaveKeyUI(keyDoor.GetKeyType().ToString(), false);
            else
                keyDoor.CloseDoor();
        }
    }
}
