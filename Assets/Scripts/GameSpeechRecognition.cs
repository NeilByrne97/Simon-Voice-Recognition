using System;
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

public class GameSpeechRecognition : MonoBehaviour
{
    private string[] colors = new string[] { "yellow", "green", "blue", "red", "orange", "purple" };    
    private GrammarRecognizer gr;

    protected string color = "";

    public RobotLogic robotLogic = new RobotLogic();
    public int _number;
    private void Start()
    {      
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
            ColorChecker(valueString);
        }
        // use a string builder to create the string and out put to the user
        Debug.Log(message);
    }

    public void ColorChecker(String color)
    {
        switch(color)
        {
            case "yellow":
                robotLogic.ButtonClicked(0);
                _number = 0;
                Debug.Log("Yellow");
                break;
            case "green":
                robotLogic.ButtonClicked(1);
                Debug.Log("Green");
                break;
            case "blue":
                robotLogic.ButtonClicked(2);
                Debug.Log("Blue");
                break;
            case "red":
                robotLogic.ButtonClicked(3);
                Debug.Log("Red");
                break;
            case "orange":
                robotLogic.ButtonClicked(4);
                Debug.Log("Orange");
                break;
            case "purple":
                robotLogic.ButtonClicked(5);
                Debug.Log("Purple");
                break;                                                                                

        }
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
