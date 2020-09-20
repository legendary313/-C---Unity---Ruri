using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jikan02 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isActived = false;
    public GameObject activePar;
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Player"))
        {
            isActived = true;
        }    
        
    }

    void Update()
    {
        if(isActived == true)
        {
            activePar.SetActive(true);
        }
    }
    
}
