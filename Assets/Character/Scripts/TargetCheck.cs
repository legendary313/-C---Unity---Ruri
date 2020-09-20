using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCheck : MonoBehaviour
{
    private GameObject targetObject;
    private List<GameObject> listTargets;
    private GameObject closestTarget;
    void Start()
    {
        listTargets = new List<GameObject>();
        targetObject = gameObject.transform.GetChild(0).gameObject;
        targetObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (listTargets.Count > 0)
        {
            float mindistance = Vector2.Distance(listTargets[0].transform.position, transform.position);
            GameObject target = listTargets[0];
            foreach (GameObject enemy in listTargets)
            {
                float distance = Vector2.Distance(enemy.transform.position, transform.position);
                if (mindistance > distance)
                {
                    mindistance = distance;
                    target = enemy;
                }
            }
            closestTarget = target;
            targetObject.transform.position = closestTarget.transform.position + new Vector3(0f, 0.7f, 0f);
            if (!closestTarget.CompareTag("Boss"))
                targetObject.SetActive(true);
        }else{
            targetObject.SetActive(false);
            closestTarget = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Creep"))
        {
            listTargets.Add(other.gameObject);
        }else if (other.CompareTag("Boss")){
            listTargets.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Creep") || other.CompareTag("Boss")){
            listTargets.Remove(other.gameObject);
        }
    }

    public GameObject getClosestTarget()
    {
        return closestTarget;
    }
}
