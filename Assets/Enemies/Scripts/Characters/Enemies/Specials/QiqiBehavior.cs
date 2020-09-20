using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiqiBehavior : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] LayerMask layerMask;
    public enum Orientation { Top, Bottom, Left, Right };
    [Header("Direction Attack")]
    [SerializeField] Orientation orientation;
    [Header("Time")]
    [SerializeField] float countDown;
    [SerializeField] float attackTime;
    [Header("Laser")]
    [SerializeField] GameObject laserObject;
    [SerializeField] Transform startPoint;
    [Header("Target Detection")]
    [SerializeField] TargetDetection targetDetection;
    [Header("Qiqi Enemy")]
    [SerializeField] GameObject qiqiEnemy;
    [Header("Particle")]
    [SerializeField] GameObject objectEffectLase;
    [SerializeField] ParticleSystem effectLaser;
    [Header("Sound")]
    [SerializeField] SFXMusic laserSound;

    private bool enableBehavior = false;
    LineRenderer laser;
    BoxCollider2D col2D;
    Vector3 dirAtk;
    Vector3 dirLaser;
    bool isHorizontal;
    bool enableAttack = true;
    Vector3 curStartPoint;
    void Awake()
    {
        laser = laserObject.GetComponent<LineRenderer>();
        col2D = laserObject.GetComponent<BoxCollider2D>();
        FixDirectionAttack();
    }
    void Update()
    {
        FixDirectionAttack();
        if (targetDetection.targetDetection && enableAttack)
        {
            qiqiEnemy.SetActive(true);
            StartCoroutine(CountDownAttack(attackTime, countDown));
        }
        if (!targetDetection.targetDetection)
        {
            qiqiEnemy.SetActive(false);
        }
    }
    private void StartAttack()
    {
        objectEffectLase.SetActive(true);
        effectLaser.Play();
        laserObject.SetActive(true);
        laser.SetPosition(0, startPoint.position);
        //RaycastHit2D hit2D = Physics2D.Raycast(laserObject.transform.position, dirLaser, layerMask);
        RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(curStartPoint, dirLaser, layerMask);
        foreach (RaycastHit2D hit2D in hit2Ds)
        {
            if (hit2D.collider && hit2D.collider.tag.Equals("Ground"))
            {
                dirAtk = Vector3.zero;
                if (isHorizontal)
                {
                    laser.SetPosition(1, new Vector3(hit2D.point.x, startPoint.position.y, 0f));
                    col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x));
                    col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x) / 2f);
                }
                else
                {
                    laser.SetPosition(1, new Vector3(startPoint.position.x, hit2D.point.y, 0f));
                    col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y));
                    col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y) / 2f);
                }
                break;
            }
            else
            {
                dirAtk += dirLaser * 100f;
                if (isHorizontal)
                {
                    laser.SetPosition(1, new Vector3(dirAtk.x + startPoint.position.x, startPoint.position.y, 0f));
                    col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x));
                    col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x) / 2f);
                }
                else
                {
                    laser.SetPosition(1, new Vector3(startPoint.position.x, dirAtk.y + startPoint.position.y, 0f));
                    col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y));
                    col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y) / 2f);
                }
                // col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(0).y - laser.GetPosition(1).y));
                // col2D.offset = new Vector2(0f, (laser.GetPosition(0).y - laser.GetPosition(1).y) / 2f);
            }
        }
        // if (hit2D.collider && !hit2D.collider.tag.Equals("Ground"))
        // {
        //     curStartPoint = hit2D.point;
        //     Debug.Log(curStartPoint);
        //     dirAtk += dirLaser * 10f;
        //     if (isHorizontal)
        //     {
        //         curStartPoint.x += dirLaser.x * 3f;
        //         laser.SetPosition(1, new Vector3(dirAtk.x + startPoint.position.x, startPoint.position.y, 0f));
        //         col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x));
        //         col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x) / 2f);
        //     }
        //     else
        //     {
        //         curStartPoint.y += dirLaser.y * 3f;
        //         laser.SetPosition(1, new Vector3(startPoint.position.x, dirAtk.y + startPoint.position.y, 0f));
        //         col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y));
        //         col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y) / 2f);
        //     }
        // }
        // else if (hit2D.collider && hit2D.collider.tag.Equals("Ground"))
        // {
        //     dirAtk = Vector3.zero;
        //     if (isHorizontal)
        //     {
        //         laser.SetPosition(1, new Vector3(hit2D.point.x, startPoint.position.y, 0f));
        //         col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x));
        //         col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x) / 2f);
        //     }
        //     else
        //     {
        //         laser.SetPosition(1, new Vector3(startPoint.position.x, hit2D.point.y, 0f));
        //         col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y));
        //         col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y) / 2f);
        //     }

        // }
        // else
        // {
        //     dirAtk += dirLaser * 100f;
        //     if (isHorizontal)
        //     {
        //         laser.SetPosition(1, new Vector3(dirAtk.x + startPoint.position.x, startPoint.position.y, 0f));
        //         col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x));
        //         col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(0).x - laser.GetPosition(1).x) / 2f);
        //     }
        //     else
        //     {
        //         laser.SetPosition(1, new Vector3(startPoint.position.x, dirAtk.y + startPoint.position.y, 0f));
        //         col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y));
        //         col2D.offset = new Vector2(0f, Mathf.Abs(laser.GetPosition(1).y - laser.GetPosition(0).y) / 2f);
        //     }
        //     // col2D.size = new Vector2(laser.startWidth, Mathf.Abs(laser.GetPosition(0).y - laser.GetPosition(1).y));
        //     // col2D.offset = new Vector2(0f, (laser.GetPosition(0).y - laser.GetPosition(1).y) / 2f);
        // }

    }
    private void EndAttack()
    {
        laserSound.StopMusic();
        objectEffectLase.SetActive(false);
        effectLaser.Stop();
        laserObject.SetActive(false);
        dirAtk = Vector3.zero;
    }
    IEnumerator CountDownAttack(float atkTime, float cd)
    {
        laserSound.PlayMusic();
        enableAttack = false;
        float timer = 0f;
        while (timer <= atkTime)
        {
            curStartPoint = laserObject.transform.position;
            StartAttack();
            yield return new WaitForSeconds(atkTime / 100f);
            timer += atkTime / 100f;
        }
        EndAttack();
        yield return new WaitForSeconds(cd);
        enableAttack = true;
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag.Equals("Player"))
    //     {
    //         enableBehavior = true;
    //     }
    // }
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag.Equals("Player"))
    //     {
    //         enableBehavior = false;
    //     }
    // }
    private void FixDirectionAttack()
    {
        switch (orientation)
        {
            case Orientation.Right:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                    dirLaser = Vector3.right;
                    isHorizontal = true;
                    break;
                }
            case Orientation.Bottom:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    dirLaser = Vector3.down;
                    isHorizontal = false;
                    break;
                }
            case Orientation.Left:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    dirLaser = Vector3.left;
                    isHorizontal = true;
                    break;
                }
            default:
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    dirLaser = Vector3.up;
                    isHorizontal = false;
                    break;
                }
        }
    }
}
