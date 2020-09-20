using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    public void destroy(){
        this.gameObject.GetComponent<Animator>().SetBool("Destroy", true);
    }   
    
}
