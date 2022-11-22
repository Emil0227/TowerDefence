using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class resLoad : MonoBehaviour
{
    public GameObject ParticleHit = null;//particle effect resource (enemy hitten)
    public GameObject ParticleEliminate = null;//particle effect resource (enemy eliminated)
    public GameObject Bullet1 = null;//normal bullet resource
    public GameObject Bullet2 = null;//scattershot bullet resource
    public GameObject[] Turret = null;//turret resources
    public GameObject[] Button = null;//button resources for creating turret
    public GameObject ButtonUpgrade = null;//button resource for upgrading turret
    public GameObject ButtonDestory = null;//button resource for destroying turret
    public int TurretCount=0;//number of turret types
    public GameObject ShowGold;//Information for gold (assign values in the Unity editor)
    public GameObject ShowLevelInfo;//information for level (assign values in the Unity editor)
    public GameObject Canvas;//(assign values in the Unity editor)

    void Awake()
    {
        FileInfo fi = new FileInfo(Application.dataPath + "/myLevel" + singleClass.currentLevel + "/turretConfig.txt");
        StreamReader sr = fi.OpenText();
        TurretCount = int.Parse(sr.ReadLine());
        Turret = new GameObject[TurretCount];
        Button = new GameObject[TurretCount];
        //read the configuration of <turretConfig.txt>
        for (int j = 0; j < TurretCount; j++)
        {
            string turretName = sr.ReadLine();
            Turret[j] = Resources.Load<GameObject>(turretName);
            Button[j] = Resources.Load<GameObject>("UI/"+ turretName+"Button");
            int price = int.Parse(sr.ReadLine());
            int rotateSpeed = int.Parse(sr.ReadLine());
            float fireRate = float.Parse(sr.ReadLine());
            float attackRange = float.Parse(sr.ReadLine());
            float attackRangeUpgrade = float.Parse(sr.ReadLine());
            int bulletSpeed = int.Parse(sr.ReadLine());
            int bulletDamage = int.Parse(sr.ReadLine());
            if (Turret[j].gameObject.name == "turret1")
            {
                turret1 t = Turret[j].GetComponent<turret1>();
                t.SetPrice(price);
                t.RotateSpeed = rotateSpeed;
                t.SetFireRate(fireRate);
                t.SetAttackRange(attackRange);
                t.SetAttackRangeUpgrade(attackRangeUpgrade);
                t.SetBulletSpeed(bulletSpeed);
                t.SetBulletDamage(bulletDamage);
            }
            else if (Turret[j].gameObject.name == "turret2")
            {
                turret2 t = Turret[j].GetComponent<turret2>();
                t.SetPrice(price);
                t.RotateSpeed = rotateSpeed;
                t.SetFireRate(fireRate);
                t.SetAttackRange(attackRange);
                t.SetAttackRangeUpgrade(attackRangeUpgrade);
                t.SetBulletSpeed(bulletSpeed);
                t.SetBulletDamage(bulletDamage);
            }
        }
        sr.Close();

        ParticleHit = Resources.Load<GameObject>("hit");
        ParticleEliminate = Resources.Load<GameObject>("eliminate");
        Bullet1 = Resources.Load<GameObject>("bullet1");
        Bullet2 = Resources.Load<GameObject>("bullet2");
        ButtonUpgrade = Resources.Load<GameObject>("UI/ButtonUpgrade");
        ButtonDestory = Resources.Load<GameObject>("UI/ButtonDestory");
    }
}
