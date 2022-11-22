using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class role : MonoBehaviour
{
    ArrayList turretList1; //pool for turret1 targeting at an enemy
    ArrayList turretList2; //pool for turret2 targeting at an enemy
    ArrayList bulletList; //pool for bullets targeting at an enemy
    GameObject path = null;//path node
    Transform nextTrans = null;// transform information of the next path node
    Vector3 vFullLife;//initial scale of the life bar
    float speed = 0;//enemy moving speed
    int initialLife = 0;//initial life value
    int currentLife = 0;//current life value

    void Start()
    {
        turretList1 = new ArrayList();
        turretList2 = new ArrayList();
        bulletList = new ArrayList();
        vFullLife = gameObject.transform.Find("lifeBar").localScale;
    }

    public void AddTurret1(turret1 t)//add a turret1 object that targeting at the enemy (to the pool)
    {
        turretList1.Add(t);
    }
    public void AddTurret2(turret2 t)//add a turret2 object that targeting at the enemy (to the pool)
    {
        turretList2.Add(t);
    }
    public void AddBullet(bullet b)//add a bullet object that targeting at the enemy (to the pool)
    {
        bulletList.Add(b);
    }
    public void DestoryTurret1(turret1 t)//delete a turret1 object that targeting at the enemy (from the pool)
    {
        turretList1.Remove(t);
    }
    public void DestoryTurret2(turret2 t)//delete a turret2 object that targeting at the enemy (from the pool)
    {
        turretList2.Remove(t);
    }
    public void DestoryBullet(bullet b)//delete a bullet object that targeting at the enemy (from the pool)
    {
        bulletList.Remove(b);
    }
    public void DisconnectTurret1()//disconnect all the turret1 that targeting at the enemy
    {
        foreach (turret1 t in turretList1)
        {
            t.Aim = null;
        }
    }
    public void DisconnectTurret2()//disconnect all the turret2 that targeting at the enemy
    {
        foreach (turret2 t in turretList2)
        {
            t.Aim = null;
        }
    }
    public void DisconnectBullet()//disconnect all the bullet that targeting at the enemy
    {
        foreach (bullet b in bulletList)
        {
            b.Aim = null;
        }
    }

    //set life bar length
    public void SetLife(int life)
    {
        currentLife = life;
        float percentage = currentLife * 1.0f / initialLife;
        gameObject.transform.Find("lifeBar").localScale = new Vector3(vFullLife.x * percentage, vFullLife.y, vFullLife.z);
    }
    public int GetLife()
    {
        return currentLife;
    }

    //set the enemy's path, speed, life value
    public void InitRole(string path, float speed, int blood)
    {
        initialLife = blood;
        currentLife = blood;
        this.path = GameObject.Find(path);
        this.speed = speed;
        transform.position = this.path.transform.position;
        nextTrans = this.path.transform.Find("path");
        transform.LookAt(nextTrans);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void Update()
    {
        //enemy follow path
        if (nextTrans != null)
        {
            float distance = Vector3.Distance(transform.position, nextTrans.position);
            if (distance > Time.deltaTime * this.speed)
            {
                transform.position += transform.forward * Time.deltaTime * speed;
                float currentAngle = transform.eulerAngles.y;
                transform.LookAt(nextTrans);
                float destAngle = transform.eulerAngles.y;
                float angle = Mathf.MoveTowardsAngle(currentAngle, destAngle, Time.deltaTime * 400);
                transform.eulerAngles = new Vector3(0, angle, 0);
            }
            else
            {
                transform.position = nextTrans.position;
                nextTrans = nextTrans.Find("path"); 
                if (nextTrans == null)//enemy reaches the last node
                {
                    //set the UI for game over
                    if (Camera.main.GetComponent<gameState>().GameState != 1)
                    {
                        Camera.main.GetComponent<gameState>().GameState = 1;
                        GameObject showLevelInfo = Camera.main.GetComponent<resLoad>().ShowLevelInfo;
                        GameObject canvas = Camera.main.GetComponent<resLoad>().Canvas;
                        showLevelInfo.SetActive(true);
                        canvas.GetComponent<Animator>().SetBool("showInfo", true); 
                        showLevelInfo.GetComponent<showLevelInfo>().SetTitle("Game Over");
                        showLevelInfo.GetComponent<showLevelInfo>().SetButtonText("Try Again");
                    }     
                }
            }
        }
    }
}


