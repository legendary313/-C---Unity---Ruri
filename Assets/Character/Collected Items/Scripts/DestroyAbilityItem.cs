using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAbilityItem : MonoBehaviour
{
    GameObject arrowGuide;
    void Start() {
        arrowGuide = transform.GetChild(0).gameObject;
        if (arrowGuide != null){
            arrowGuide.SetActive(true);
        }   
    }
    public void destroy(){
        this.gameObject.GetComponent<Animator>().SetBool("Destroy", true);
        if (arrowGuide != null){
            arrowGuide.SetActive(false);
        }
    }
}
