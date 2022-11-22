using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showGold : MonoBehaviour
{
    public int Gold = 0;
    AudioSource bonus;

    public void Start()
    {
        bonus = gameObject.GetComponent<AudioSource>();
        if (singleClass.currentLevel == 1)
        {
            SetGold(20);
        }
        if (singleClass.currentLevel == 2)
        {
            SetGold(30);
        }
    }

    public void SetGold(int x)
    {
        Gold = x;
        transform.Find("Text").gameObject.GetComponent<Text>().text = "Gold: " + Gold;
    }

    public void BonusSFX()
    {
        bonus.Play();
    }

    public int GetGold()
    {
        return Gold;
    }
}
