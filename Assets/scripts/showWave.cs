using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showWave : MonoBehaviour
{
    Text text = null;

    void Awake()
    {
        text = transform.Find("Text").gameObject.GetComponent<Text>();
    }

    public void SetText(string sText)
    {
        text.text = sText;
    }
}
