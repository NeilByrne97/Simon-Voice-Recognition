﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotLogic : MonoBehaviour
{
    public ClickButton[] colorButtons;
    public List<int> colorList;
    public float showtime = 0.5f;   // Highlight Color
    public float pausetime = 0.5f;  // Normal Color
    
    // Game objects
    public int level = 2;
    private int playerLevel = 0;
    private bool robot = false;
    public bool player = false;

    // Color changes each turn
    private int randomColor;

    // Menu
    public Button StartButton;
    public Text gameOverText;
    public Text scoreText;
    public Text highScore;
    public int score;

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("High Score", 0).ToString();
        for (int i = 0; i < colorButtons.Length; i++)   // Difficulty TO BE DONE
        {
            colorButtons[i].onClick += ButtonClicked;    
            colorButtons[i].myNumber = i;
        }    
    }

    void ButtonClicked(int _number)
    {
        if(player)
        {
            if(_number == colorList[playerLevel])   // playerLevel is the position in the sequence EXPLAIN BETTER
            {
                playerLevel += 1;   // Next color in position set up for next button press
                score += 1;         // Increment after every correct click
                scoreText.text = score.ToString();  // Update string
            }
            else // Wrong answer
            {
                highScore.text = score.ToString();
                if(score > PlayerPrefs.GetInt("Highscore", 0))
                {
                    PlayerPrefs.SetInt("Highscore", score);
                    highScore.text = score.ToString();
                }
                GameOver();
            }
            if(playerLevel == level)
            {   // Next round reset
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
            if(colorList.Count < level) // Lits if shown colors
            {
                randomColor = Random.Range(0, colorButtons.Length); // Color is one of the available colors
                colorList.Add(randomColor); 
            }
            
            colorButtons[colorList[i]].ClickedColor();   // Change to highlight
            yield return new WaitForSeconds(showtime);   // Highlight Color time
            colorButtons[colorList[i]].UnClickedColor(); // Change back to normal
            yield return new WaitForSeconds(pausetime);  // Normal Color
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
