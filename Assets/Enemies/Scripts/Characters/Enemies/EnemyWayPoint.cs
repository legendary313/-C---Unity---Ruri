using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPoint : MonoBehaviour
{
    [Header("Layer Ground")]
    [SerializeField] LayerMask layerMask;
    [Header("Distance")]
    float distance = 0.2f;
    bool isGrounded = false;
    void Start()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distance, layerMask);
    }

    void Update()
    {
        if (!isGrounded)
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distance, layerMask);
            transform.Translate(new Vector3(0f, -0.1f, 0f));
        }
    }
}
