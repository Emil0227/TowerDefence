using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class showLevelInfo : MonoBehaviour
{
    public void SetTitle(string str)
    {
        transform.Find("Text").GetComponent<Text>().text = str;
    }

    public void SetButtonText(string str)
    {
        transform.Find("ButtonLevel").Find("Text").GetComponent<Text>().text = str;
    }
}
