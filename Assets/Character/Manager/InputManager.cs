using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Input = UnityEngine.Input;
public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] bool testByKeyBoard;
    [SerializeField] FloatingJoystick joystick;
    private Button button;
    public bool moveLeft;
    public bool moveRight;
    private bool holdAttack;
    private bool disableMove = false;
    private float timeAttackHolding;
    private float baseTimeAttackHolding;
    bool releaseSpace = true;
    public bool disableInput = false;

    void Start()
    {
        baseTimeAttackHolding = player.timeToChargeFlame;
    }

    void Update()
    {
        if (testByKeyBoard)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (!disableMove)
                    player.moveLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (!disableMove)
                    player.moveRight();
            }

            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                player.idle();
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
            {
                player.idle();
            }

            // Space
            if (Input.GetKey(KeyCode.Space))
            {
                if (releaseSpace && !disableMove)
                {
                    player.jump();
                    releaseSpace = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                releaseSpace = true;
            }

            // Shoot F
            if (Input.GetKeyDown(KeyCode.F))
            {
                player.shoot();
            }

            // Dash Control
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!disableMove)
                    player.dash();
            }

            // Charge Flame
            if (Input.GetKeyDown(KeyCode.F))
            {
                holdAttack = true;
            }
            if (holdAttack)
            {
                if (player.isGrounded && player.mana > 0)
                {
                    disableMove = true;
                    timeAttackHolding += Time.deltaTime;
                    if (timeAttackHolding > (baseTimeAttackHolding / 4))
                    {
                        player.instanceFlameCharging();
                        player.setBool("ChargeFlame", true);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                disableMove = false;
                if (timeAttackHolding > baseTimeAttackHolding)
                {
                    player.chargeFlame();
                }
                else
                {
                    player.destroyFlameCharging();
                }
                player.setBool("ChargeFlame", false);
                timeAttackHolding = 0;
                holdAttack = false;
            }
        }
        else
        {
            // Move Right
            // if (!moveLeft && !moveRight){
            //     player.idle();
            // }
            // if (moveLeft && moveRight){
            //     player.idle();
            // }
            // if (moveLeft){
            //     player.moveLeft();
            // }
            // if (moveRight){
            //     player.moveRight();
            // }

            if (joystick.Horizontal > 0)
            {
                if (!disableMove)
                {
                    moveLeft = false;
                    moveRight = true;
                    player.moveRight();
                }
            }
            else if (joystick.Horizontal < 0)
            {
                if (!disableMove)
                {
                    moveRight = false;
                    moveLeft = true;
                    player.moveLeft();
                }
            }
            else
            {
                moveRight = false;
                moveLeft = false;
                player.idle();
            }

            // Charge Flame
            if (player.chargeFlameEnable)
            {
                if (player.mana > 0)
                {
                    if (holdAttack && player.isGrounded)
                    {
                        disableMove = true;
                        timeAttackHolding += Time.deltaTime;
                        if (timeAttackHolding > (baseTimeAttackHolding / 4))
                        {
                            player.setBool("ChargeFlame", true);
                            player.runParticle.Stop();
                            player.instanceFlameCharging();
                        }
                    }
                    else if (!holdAttack)
                    {
                        disableMove = false;
                        player.setBool("ChargeFlame", false);
                        if (timeAttackHolding >= baseTimeAttackHolding)
                        {
                            player.chargeFlame();
                        }
                        else
                        {
                            player.destroyFlameCharging();
                        }
                        timeAttackHolding = 0;
                    }
                }
            }
        }

    }

    //Move
    public void LeftButtonDown()
    {

        if (!testByKeyBoard)
            moveLeft = true;

    }
    public void LeftButtonUp()
    {

        if (!testByKeyBoard)
            moveLeft = false;

    }
    public void RightButtonDown()
    {

        if (!testByKeyBoard)
            moveRight = true;

    }
    public void RightButtonUp()
    {

        if (!testByKeyBoard)
            moveRight = false;

    }
    // Jump
    public void spaceButtonEnter()
    {

        if (!testByKeyBoard)
            if (releaseSpace && !disableMove)
            {
                player.jump();
                releaseSpace = false;
            }
    }
    public void spaceButtonUp()
    {

        if (!testByKeyBoard)
            releaseSpace = true;

    }

    // Attack
    public void fireButtonDown()
    {

        holdAttack = true;
        if (!testByKeyBoard)
            player.shoot();

    }

    public void fireButtonUp()
    {

        holdAttack = false;

    }

    // Dash
    public void dashButton()
    {

        if (!testByKeyBoard && !disableMove)
            player.dash();
    }
}
