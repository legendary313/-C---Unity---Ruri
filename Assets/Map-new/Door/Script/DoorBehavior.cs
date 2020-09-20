using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [Header("Trigger")]
    // [SerializeField] DoorLockTrigger doorLockTrigger;
    [SerializeField] Collider2D gateMap;
    [Header("UI")]
    [SerializeField] UIEventSystem uIEventSystem;
    [Header("Joystick")]
    [SerializeField] FloatingJoystick floatingJoystick;
    [Header("Sound")]
    [SerializeField] SFXMusic soundDoor;
    [Header("Light")]
    [SerializeField] GameObject doorLight;
    [SerializeField] GameObject lockDoorLight;
    bool isStandingLockTrigger = false;
    bool finishOpen = false;
    bool finishClose = true;
    Animator animator;
    private void Awake()
    {
        // doorLockTrigger.setStatePlayerOnLockTrigger = setStateLockDoor;
        // doorMoveTrigger.setPlayerOnMoveTrigger = teleportPlayer;
        finishOpen = false;
        finishClose = true;
        animator = GetComponent<Animator>();
        doorLight.SetActive(false);
        lockDoorLight.SetActive(true);
        gateMap.enabled = false;
    }

    private void Update()
    {
        //Debug.Log(finishOpen.ToString() + " - " + finishClose.ToString());
    }
    public void setStateLockDoor(bool val)
    {
        if (val && finishClose)
        {
            Debug.Log("mo cua ra");
            uIEventSystem.InactiveAll();
            floatingJoystick.StopMovement();
            finishOpen = false;
            isStandingLockTrigger = val;
            StartCoroutine(OpenTheDoor());
        }
        else if (val && finishOpen)
        {
            Debug.Log("cua da mo roi, khong can mo nua");
            finishOpen = true;
            isStandingLockTrigger = val;
            gateMap.enabled = true;
        }
        else if (!val && finishOpen)
        {
            Debug.Log("chuan bi dong cua");
            finishClose = false;
            isStandingLockTrigger = val;
            gateMap.enabled = false;
            StartCoroutine(CloseTheDoor());
        }
        else if (!val && finishClose)
        {
            Debug.Log("cua da dong roi, khong can dong nua");
            finishClose = true;
            isStandingLockTrigger = val;
            gateMap.enabled = false;
        }
    }
    IEnumerator OpenTheDoor()
    {
        while (!finishClose) yield return null;
        animator.SetTrigger("Open");
        lockDoorLight.SetActive(false);
        soundDoor.PlayMusic();
    }
    IEnumerator CloseTheDoor()
    {
        Debug.Log("doi 5 giay");
        yield return new WaitForSeconds(5f);
        if (isStandingLockTrigger)
        {
            Debug.Log("khong duoc dong, player dang dung o cua");
            yield break;
        }
        if (!finishClose)
        {
            Debug.Log("dong cua");
            finishOpen = false;
            doorLight.SetActive(false);
            animator.SetTrigger("Close");
            soundDoor.PlayMusic();
        }
    }

    public void FinishOpenDoor()
    {
        uIEventSystem.ActiveUIPlayGame();
        finishOpen = true;
        finishClose = false;
        doorLight.SetActive(true);
        gateMap.enabled = true;
    }
    public void FinishCloseDoor()
    {
        finishClose = true;
        lockDoorLight.SetActive(true);
    }
}
