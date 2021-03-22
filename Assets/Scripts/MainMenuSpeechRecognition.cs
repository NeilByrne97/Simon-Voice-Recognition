﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;  // for stringbuilder
using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser
using UnityEngine.SceneManagement;


/*
 *  Uses English US in the settings - Keyboard (on the taskbar), Region, Preferred Language and Speech in Settings
 */

public class MainMenuSpeechRecognition : MonoBehaviour
{
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private GrammarRecognizer gr;

    public GameObject DifficultyMenu;
    public GameObject MainMenu;
    public GameObject LeaderboardMenu;

    private void Start()
    {
        actions.Add("play", Play);
        actions.Add("leaderboard", Leaderboard);
        actions.Add("quit", Quit);
        actions.Add("back", Back);

        actions.Add("easy", Easy);
        actions.Add("medium", Medium);
        actions.Add("hard", Hard);

        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, 
                                                "GameGrammar.xml"), 
                                    ConfidenceLevel.High);
        Debug.Log("Grammar loaded!");
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        if (gr.IsRunning) Debug.Log("Recogniser running");
    }

    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder message = new StringBuilder();
        Debug.Log("Recognised a phrase");
        // read the semantic meanings from the args passed in.
        SemanticMeaning[] meanings = args.semanticMeanings;

        // use foreach to get all the meanings.
        foreach(SemanticMeaning meaning in meanings)
        {
            string keyString = meaning.key.Trim();
            string valueString = meaning.values[0].Trim();
            message.Append("Key: " + keyString + ", Value: " + valueString + " ");
            actions[valueString].Invoke();
        }
        // use a string builder to create the string and out put to the user
        Debug.Log(message);
    }

    private void Play()
    {
        MainMenu.SetActive(false);
        DifficultyMenu.SetActive(true);
    }
    private void Leaderboard()
    {
        MainMenu.SetActive(false);
        LeaderboardMenu.SetActive(true);
    }
    private void Back()
    {
        MainMenu.SetActive(true);
        DifficultyMenu.SetActive(false);
        LeaderboardMenu.SetActive(false);
    }
    private void Quit()
    {
        Debug.Log("Quiting...");
        Application.Quit();  
    }

    private void Easy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 1);
    }
  
    private void Medium()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 2);
    }

    private void Hard()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 3);
    }


    private void OnApplicationQuit()
    {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhraseRecognized;
            gr.Stop();
        }
    }
}
