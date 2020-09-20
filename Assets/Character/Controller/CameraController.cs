using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [HideInInspector] public Vector3 offset;
    [HideInInspector] public bool followPlayer = true;
    [SerializeField] Transform bossZonePosition = null;

    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Transform>().gameObject;
        //offset = transform.position - player.transform.position;
        offset = new Vector3(0f, 2f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        // if (SceneManager.GetActiveScene().name.Equals("Map2"))
        // {
        //     if (followPlayer && bossZonePosition != null)
        //     {
        //         transform.position = player.transform.position + offset;
        //     }
        //     else
        //     {
        //         transform.position = bossZonePosition.position;
        //     }
        // }
        // else
        // {
            transform.position = player.transform.position + offset;
        // }
    }

    public void ShakeCamera()
    {
        gameObject.GetComponentInChildren<ShakeCamera>().shakeCamera(.15f, .4f);
    }
}
