using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret2 : MonoBehaviour
{
    public GameObject Aim = null;//target enemy
    public int CurrentRangeLevel = 1;//current turret level
    public int RotateSpeed = 0;//turret rotate speed
    public int Price = 0;//turret price
    public int BulletDamage = 0;//damage per bullet
    public float FireRate = 0;//turret fire rate
    public float AttackRange = 0;//turret attack range
    public float AttackRangeUpgrade = 0;//turret level upgrade multiples
    public float BulletSpeed = 0;
    int countBullet = 0;
    float distance = 0;
    bool isShooting = false;
    Transform transRotate = null;//transform information of turret
    Transform transBarrel = null;//transform information of the barrel
    GameObject resBullet2 = null;//bullet resource
    GameObject existedAim = null;
    ArrayList roleList;//pool for targeted enemies
    AudioSource shoot;

    void Start()
    {
        transRotate = transform.Find("base").Find("rotation");
        transBarrel = transform.Find("base").Find("rotation").Find("arm").Find("gunBody").Find("barrel");
        resBullet2 = Camera.main.GetComponent<resLoad>().Bullet2;
        roleList = Camera.main.GetComponent<createRole>().RoleList;
        shoot = gameObject.GetComponent<AudioSource>();
    }

    //set turret price
    public void SetPrice(int x)
    {
        Price = x;
    }
    public int GetPrice()
    {
        return Price;
    }
    //set turret fire rate
    public void SetFireRate(float f)
    {
        FireRate = f;
    }
    public float GetFireRate()
    {
        return FireRate;
    }
    //set turret attack range
    public void SetAttackRange(float r)
    {
       AttackRange = r;
    }
    public float GetAttackRange()
    {
        return AttackRange;
    }
    //set upgrade multiples
    public void SetAttackRangeUpgrade(float f)
    {
        AttackRangeUpgrade = f;
    }
    public float GetAttackRangeUpgrade()
    {
        return AttackRangeUpgrade;
    }
    //update turret level
    public void Upgrade()
    {
        if (CurrentRangeLevel == 4)
        {
            return;
        }
        AttackRange *= AttackRangeUpgrade;
        CurrentRangeLevel += 1;
    }
    public int GetCurrentRangeLevel()
    {
        return CurrentRangeLevel;
    }
    //設置子彈的速度
    public void SetBulletSpeed(float f)
    {
        BulletSpeed = f;
    }
    public float GetBulletSpeed()
    {
        return BulletSpeed;
    }
    //set bullet speed
    public void SetBulletDamage(int d)
    {
        BulletDamage = d;
    }
    public int GetBulletDamage()
    {
        return BulletDamage;
    }

    void StartShoot()
    {
        if (isShooting == false)
        {
            InvokeRepeating("CreateBullet", 1.0f, FireRate);
            isShooting = true;
        }
    }
    void StopShoot()
    {
        if (isShooting == true)
        {
            CancelInvoke("CreateBullet");
            isShooting = false;
        }
    }

    //Randomly fire 3 bullets within ±15 degrees in the horizontal direction, and the time interval between each bullet firing is 0.1 seconds
    void CreateBullet()
    {
        InvokeRepeating("CreateScattershot", 0.0f, 0.1f);
    }
    void CreateScattershot()
    {
        shoot.Play();
        GameObject bullet2 = GameObject.Instantiate(resBullet2);
        if (Aim != null)
        {
            bullet2.GetComponent<bullet>().Aim = this.Aim;
            Aim.GetComponent<role>().AddBullet(bullet2.GetComponent<bullet>());
        }
        bullet2.GetComponent<bullet>().Speed = BulletSpeed;
        bullet2.GetComponent<bullet>().Damage = BulletDamage;
        bullet2.transform.position = transBarrel.position;
        bullet2.transform.eulerAngles = new Vector3(transBarrel.transform.eulerAngles.x, transBarrel.transform.eulerAngles.y + Random.Range(-15, 15), transBarrel.transform.eulerAngles.z);
        countBullet++;
        if (countBullet == 3)
        {
            countBullet = 0;
            CancelInvoke("CreateScattershot");
        }
    }

    void Update()
    {
        //find the closest enemy target
        Aim = null;
        distance = 0.0f;
        foreach (GameObject r in roleList)
        {
            float d = Vector3.Distance(r.transform.position, transform.position);
            if (Aim == null)
            {
                Aim = r;
                distance = d;
            }
            else
            {
                if (distance > d)
                {
                    Aim = r;
                    distance = d;
                }
            }
        }
        //if there is a target enemy within the attack range, aim at the enemy and attack
        if (Aim != null && distance > 0 && distance < AttackRange)
        {
            if (Aim != existedAim)//every time a new target enemy is found, put the turret in the pool
            {
                existedAim = Aim;
                existedAim.GetComponent<role>().AddTurret2(this);
            }
            float currentAngleY = transRotate.eulerAngles.y;
            float currentAngleX = transRotate.eulerAngles.x;
            transRotate.LookAt(Aim.transform);
            float destAngleY = transRotate.eulerAngles.y;
            float destAngleX = transRotate.eulerAngles.x;
            float angleY = Mathf.MoveTowardsAngle(currentAngleY, destAngleY, Time.deltaTime * RotateSpeed);
            float angleX = Mathf.MoveTowardsAngle(currentAngleX, destAngleX, Time.deltaTime * RotateSpeed);
            transRotate.eulerAngles = new Vector3(angleX, angleY, 0);
            StartShoot();
        }
        else
        {
            StopShoot();
        }
    }
}