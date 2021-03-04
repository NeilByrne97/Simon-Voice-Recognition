using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotLogic : MonoBehaviour
{
    public ClickButton[] myButtons;

    public float showtime = 0.5f;
    public float pausetime = 0.5f;
    
    bool robot = false;
    bool player = false;

    private int myRandom;

    void Start()
    {
        for (int i = 0; i < myButtons.Length; i++)
        {
            myButtons[i].onClick += ButtonClicked;
            myButtons[i].myNumber = i;
        }    
    }

    void ButtonClicked(int _number)
    {

    }



    void Update()
    {
        
    }

    private IEnumerator Robot()
    {
        yield return new WaitForSeconds(1f);
        
        myRandom = Random.Range(0, myButtons.Length);
        myButtons[myRandom].ClickedColor();
        yield return new WaitForSeconds(showtime);  // Highlight Color
        myButtons[myRandom].UnClickedColor();
        yield return new WaitForSeconds(pausetime); // Normal Color

    }

    public void StartGame()
    {
        StartCoroutine(Robot());
    }

    private void Gameover()
    {

    }



}
