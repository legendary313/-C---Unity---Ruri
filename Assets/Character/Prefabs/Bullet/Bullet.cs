using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    private GameObject targetObject;
    bool appearGameObject = false;

    public void directToObjective(GameObject target){
        targetObject = target;
        appearGameObject = true;
    }

    void Start() {
        Destroy(gameObject, 2);    
    }

    void Update(){
        if (targetObject != null){
            Vector2 direction = new Vector2(targetObject.transform.position.x - transform.position.x, targetObject.transform.position.y - transform.position.y);
            if (targetObject.CompareTag("Boss")){
                gameObject.GetComponent<Rigidbody2D>().velocity = (direction + new Vector2(0f, -2f)).normalized * speed;
            }else{
                gameObject.GetComponent<Rigidbody2D>().velocity = (direction + new Vector2(0f, 0.7f)).normalized * speed;
            }
        }else{
            if (appearGameObject){
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ground")){
            Destroy(gameObject);
        }
    }

}
