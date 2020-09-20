using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractive : MonoBehaviour
{
    PlayerController player;

    void Start()
    {
        player = this.GetComponentInParent<PlayerController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BulletCreep"))
        {
            player.takeDamage(other.gameObject.GetComponent<DamageEnemy>().damage);
        }
        else if (other.gameObject.CompareTag("HealthPotion"))
        {
            if (player.healthPotionIsTaken)
            {
                player.plusHealthOne();
            }
            else
            {
                player.plusHealthOne(other.gameObject);
            }
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.GetComponent<DestroyItem>().destroy();
        }
        else if (other.gameObject.CompareTag("FullHealthPotion"))
        {
            if (player.fullHealthPotionIsTaken)
            {
                player.fullHealth();
            }
            else
            {
                player.fullHealth(other.gameObject);
            }
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.GetComponent<DestroyItem>().destroy();
        }
        else if (other.gameObject.CompareTag("ManaPotion"))
        {
            if (player.manaPotionIsTaken){
                player.plusManaOne();
            }else{
                player.plusManaOne(other.gameObject);
            }
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.GetComponent<DestroyItem>().destroy();
        }
        else if (other.gameObject.CompareTag("FullManaPotion"))
        {
            if (player.fullManaPotionIsTaken){
                player.fullMana();
            }else{
                player.fullMana(other.gameObject);
            }
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.GetComponent<DestroyItem>().destroy();
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            if (player.itemIsTaken){
                player.collectItem();
            }else{
                player.collectItem(other.gameObject);
            }
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.GetComponent<DestroyItem>().destroy();
        }
        else if (other.gameObject.CompareTag("AttackEnable"))
        {
            player.enableAttack(other.gameObject);
            other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            other.gameObject.GetComponent<DestroyAbilityItem>().destroy();
        }
        else if (other.gameObject.CompareTag("WallJumpEnable"))
        {
            player.enableWallJump(other.gameObject);
            other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            other.gameObject.GetComponent<DestroyAbilityItem>().destroy();
        }
        else if (other.gameObject.CompareTag("DashEnable"))
        {
            player.enableDash(other.gameObject);
            other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            other.gameObject.GetComponent<DestroyAbilityItem>().destroy();
        }
        else if (other.gameObject.CompareTag("ChargeFlameEnable"))
        {
            player.enableChargeFlame(other.gameObject);
            other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            other.gameObject.GetComponent<DestroyAbilityItem>().destroy();
        }
        else if (other.gameObject.CompareTag("DeadGround"))
        {
            player.takeDamage(2f);
            player.knockBack(other.gameObject.transform.position);
        }
        else if (other.gameObject.CompareTag("CheckPoint"))
        {
            player.enterSavePoint(other.gameObject);
        }else if (other.gameObject.CompareTag("BulletBoss")){
            player.takeDamage(other.gameObject.GetComponent<DamageEnemy>().damage);
        }else if (other.gameObject.CompareTag("Boss")){
            player.takeDamage(other.gameObject.GetComponent<DamageEnemy>().damage);
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            player.Die();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("DeadGround")){
            if (player.takeDamageCoolDown <= 0){
                player.takeDamage(4f);
                player.knockBack(other.gameObject.transform.position);
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            player.exitSavePoint();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("Creep"))
        {
            player.takeDamage(1f);
            player.knockBack(other.gameObject.transform.position);
            //knockBack
        }
        else if (other.collider.gameObject.CompareTag("MovingFlat"))
        {
            player.transform.parent = other.gameObject.transform;
            Debug.Log("Moving Flat");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("MovingFlat"))
        {
            player.transform.parent = null;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.gameObject.CompareTag("Creep"))
        {
            player.knockBack(other.gameObject.transform.position);
        }
    }

}
