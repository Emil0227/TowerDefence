using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class turretBase : MonoBehaviour
{
    public GameObject IsHasTurret = null;
    GameObject canvas;
    GameObject attackRangeBall = null;
    GameObject[] buttonArray;
    int buttonCountForCreate = 0;
    bool isPopButton = false;
    bool isMouseEnter = false;
    float attackRange;
    resLoad rl;
    AudioSource sfx;

    void Start()
    {
        rl = Camera.main.GetComponent<resLoad>();
        canvas = rl.Canvas;
        attackRangeBall = GameObject.Find("attackRange");
        buttonArray = new GameObject[2];
        buttonCountForCreate = Camera.main.GetComponent<resLoad>().TurretCount;
        Camera.main.GetComponent<createRole>().TurretBaseList.Add(this.gameObject);
        sfx = gameObject.GetComponent<AudioSource>();
    }

    public void DestoryAllButton()
    {
        for (int j = 0; j < 2; j++)
        {
            GameObject.Destroy(buttonArray[j]);
            isPopButton = false;
        }
        attackRangeBall.transform.position = new Vector3(0, -1000, 0);
    }

    //show attack range
    public void SetRange()
    {
        attackRangeBall.transform.position = IsHasTurret.transform.position;
        if (IsHasTurret.gameObject.name == "turret1(Clone)")
        {
            attackRange = IsHasTurret.GetComponent<turret1>().GetAttackRange();
        }
        else if (IsHasTurret.gameObject.name == "turret2(Clone)")
        {
            attackRange = IsHasTurret.GetComponent<turret2>().GetAttackRange();
        }
        attackRangeBall.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, attackRange * 2);
    }

    void OnMouseDown()
    {
        if (isPopButton)
        {
            return;
        }
        isPopButton = true;
        if (IsHasTurret == null) //when there is no turret on the base,click the base to pop-up create button
        {
            sfx.Play();
            buttonArray = new GameObject[buttonCountForCreate];
            for (int j = 0; j < buttonCountForCreate; j++)
            {
                buttonArray[j] = GameObject.Instantiate(rl.Button[j]);
            }
            //check if there are enough golds
            if (rl.ShowGold.GetComponent<showGold>().GetGold() < rl.Turret[0].GetComponent<turret1>().GetPrice())
            {
                buttonArray[0].GetComponent<button>().Disable();
            }
            if (rl.ShowGold.GetComponent<showGold>().GetGold() < rl.Turret[1].GetComponent<turret2>().GetPrice())
            {
                buttonArray[1].GetComponent<button>().Disable();
            }
            SetButtonPosition(buttonCountForCreate);
        }
        else//when there is a turret on the base,click the base to pop-up upgrade/destory button, and show attack range
        {
            sfx.Play();
            buttonArray = new GameObject[2];
            buttonArray[0] = GameObject.Instantiate(rl.ButtonUpgrade);
            buttonArray[1] = GameObject.Instantiate(rl.ButtonDestory);
            SetButtonPosition(2);
            SetRange();
            //When the upgrade limit is reached or golds < 20, disable the upgrade button
            if (IsHasTurret.gameObject.name == "turret1(Clone)")
            {
                turret1 t = IsHasTurret.GetComponent<turret1>();
                if (t.CurrentRangeLevel == 4)
                {
                    buttonArray[0].GetComponent<button>().Disable();
                }
                else if (rl.ShowGold.GetComponent<showGold>().GetGold() < 20)
                {
                    buttonArray[0].GetComponent<button>().Disable();
                }
            }
            if (IsHasTurret.gameObject.name == "turret2(Clone)")
            {
                turret2 t = IsHasTurret.GetComponent<turret2>();
                if (t.CurrentRangeLevel == 4)
                {
                    buttonArray[0].GetComponent<button>().Disable();
                }
                else if (rl.ShowGold.GetComponent<showGold>().GetGold() < 20)
                {
                    buttonArray[0].GetComponent<button>().Disable();
                }
            }
        }
    }

    //set button positions
    void SetButtonPosition(int count)
    {
        float width = buttonArray[0].GetComponent<RectTransform>().rect.width;
        float height = buttonArray[0].GetComponent<RectTransform>().rect.height;
        float startX = Input.mousePosition.x - (width * 2 + 10) / 2 + width / 2.0f;
        for (int j = 0; j < count; j++)
        {
            buttonArray[j].GetComponent<button>().Tb = this;
            buttonArray[j].transform.SetParent(canvas.transform);
            buttonArray[j].transform.position = new Vector3(startX + (width+10)* j, Input.mousePosition.y+30, 0);
        }
    }
    void OnMouseEnter()
    {
        isMouseEnter = true;
    }
    void OnMouseExit()
    {
        isMouseEnter = false;
    }

    void Update()
    {
        //click on other areas to destory the button
        if (!EventSystem.current.IsPointerOverGameObject() && isPopButton && isMouseEnter == false && Input.GetMouseButton(0) == true)
        {
            DestoryAllButton();
        }
    }
}

