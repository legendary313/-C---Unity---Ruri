using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jikan : MonoBehaviour
{
    // Start is called before the first frame update
    // public Text misionLable;    
    // public GameObject activePar;
    [SerializeField] private Key.KeyType keyType;
    [SerializeField] private bool isActived = false;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] GameObject particleObject;
    [SerializeField] ParticleSystem[] listFire;
    [SerializeField] UIEventSystem uIEventSystem;
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] CameraController cameraController;
    [SerializeField] Camera cam;
    [SerializeField] SFXMusic soundJikan;
    // private DoorAnims doorAnims;

    private void Awake()
    {
        // doorAnims = GetComponent<DoorAnims>();
    }

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void ActiveJikan()
    {
        isActived = true;
        if (boxCollider2D.enabled)
            StartCoroutine(StartActiveJikan());
    }
    IEnumerator StartActiveJikan()
    {
        uIEventSystem.InactiveAll();
        joystick.StopMovement();
        particleObject.SetActive(true);
        cameraController.offset = new Vector3(0f, 4f, -10f);
        cam.orthographicSize = 8f;
        for (int i = 0; i < listFire.Length; i++)
        {
            listFire[i].Play();
            soundJikan.PlayMusic();
            yield return new WaitForSeconds(1.5f);
        }
        smoke.Stop();
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(1f);
        cameraController.offset = new Vector3(0f, 2f, -10f);
        cam.orthographicSize = 5f;
        uIEventSystem.ActiveUIPlayGame();
    }
}
