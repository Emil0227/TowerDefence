using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class gameChoiceButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.Find("Text").GetComponent<Text>().text == "Try Again")
        {     
            Camera.main.GetComponent<gameState>().GameState = 0;//reset game state
            Camera.main.GetComponent<resLoad>().ShowGold.GetComponent<showGold>().Start();//reset golds
            Camera.main.GetComponent<createRole>().ResetLevel();//reset level
            //reset UI
            transform.parent.gameObject.SetActive(false); 
            transform.parent.parent.gameObject.GetComponent<Animator>().SetBool("showInfo", false);
        }
        else if (transform.Find("Text").GetComponent<Text>().text == "Next Level")
        {
            SceneManager.LoadScene("Loading");
        }
        else if (transform.Find("Text").GetComponent<Text>().text == "Quit Game")
        {
            Application.Quit();
        }
    }
}
