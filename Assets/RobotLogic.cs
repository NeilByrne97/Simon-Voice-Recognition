using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotLogic : MonoBehaviour
{
    public ClickButton[] myButtons;
    public List<int> colorList;
    public float showtime = 0.5f;
    public float pausetime = 0.5f;
    
    public int level = 2;
    private int playerLevel = 0;
    private bool robot = false;
    public bool player = false;

    private int myRandom;

    public Button StartButton;
    public Text gameOverText;
    public Text scoreText;
    private int score;

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
        if(player)
        {
            if(_number == colorList[playerLevel])
            {
                playerLevel += 1;
                score += 1;
                scoreText.text = score.ToString();

            }
            else
            {
                GameOver();
            }
            if(playerLevel == level)
            {   // Next round
                level += 1;
                playerLevel = 0;
                player = false;
                robot = true;
            }
        }
    }



    void Update()
    {
        if(robot)
        {
            robot = false;
            StartCoroutine(Robot());
        }
                
    }

    private IEnumerator Robot()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < level; i++)
        {
            if(colorList.Count < level)
            {
                myRandom = Random.Range(0, myButtons.Length);
                colorList.Add(myRandom);
            }
            
            myButtons[colorList[i]].ClickedColor();
            yield return new WaitForSeconds(showtime);  // Highlight Color
            myButtons[colorList[i]].UnClickedColor();
            yield return new WaitForSeconds(pausetime); // Normal Color
        }
        player = true;
    }

    public void StartGame()
    {
        robot = true;
        score = 0;
        playerLevel = 0;
        level = 2;
        gameOverText.text = "";
        scoreText.text = score.ToString();
        StartButton.interactable = false;


    }

    private void GameOver()
    {
        gameOverText.text = "Game Over";
        StartButton.interactable = true;
        player = false;
    }



}
