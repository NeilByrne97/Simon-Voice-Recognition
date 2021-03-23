using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class CrossHair : MonoBehaviour
{
    [SerializeField] private string[] keywords;

    [SerializeField] private float speed = 5.0f;    // move the crosshair at this speed

    private KeywordRecognizer kr;   // up down left right stop shoot/fire
    private Rigidbody2D rb;

    // Action is in System, using System; or System.Action
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Confidence Level
    private ConfidenceLevel confidenceLevel = ConfidenceLevel.Low;

    private string spokenWord = "";

    void Start()
    {
        actions.Add("Up", Up);
        actions.Add("Down", Down);
        actions.Add("Left", Left);
        actions.Add("Right", Right);

        rb = GetComponent<Rigidbody2D>();
        if(keywords != null)
        {
            //kr = new KeywordRecognizer(keywords, confidenceLevel);
            kr = new KeywordRecognizer(actions.Keys.ToArray(), confidenceLevel);
            kr.OnPhraseRecognized += KR_OnPhraseRecognized;
            kr.Start();
            Debug.Log("KR Started");
        }
    }

    private void KR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        spokenWord = args.text;
        Debug.Log("You said " + spokenWord);
        actions[spokenWord].Invoke();
    }

    // == Actions Methods ==
    // void return type and no parameters
    private void Up()
    {
        transform.Translate(0, 1, 0);
    }

    private void Down()
    {
        transform.Translate(0, -1, 0);
    }

    private void Left()
    {
        transform.Translate(-1, 0, 0);
    }

    private void Right()
    {
        transform.Translate(1, 0, 0);
    }

    //void Update()
    //{
    //    // how to apply that command from the spoken word
    //    float hMovement = 0, vMovement = 0;

    //    switch (spokenWord)
    //    {
    //        case "Up":
    //            vMovement = 1;
    //            break;
    //        case "Down":
    //            vMovement = -1;
    //            break;
    //        case "Left":
    //            hMovement = -1;
    //            break;
    //        case "Right":
    //            hMovement = 1;
    //            break;
    //        case "Stop":
    //            hMovement = vMovement = 0;
    //            break;
    //        default:
    //            break;
    //    }

    //float xValue, yValue;
    //Mathf.Clamp()
    //    rb.velocity = new Vector2(hMovement * speed, vMovement * speed);
    //}
    private void OnApplicationQuit()
    {
        if (kr != null && kr.IsRunning)
        {
            kr.OnPhraseRecognized -= KR_OnPhraseRecognized;
            kr.Stop();
        }
    }
    // failsafe stop mechanism
    //private void OnApplicationQuit()
    //{
    //    if(kr!= null && kr.IsRunning)
    //    {
    //        kr.OnPhraseRecognized -= KR_OnPhraseRecognized;
    //        kr.Stop();
    //    }
    //}

    /*
     * Say the colour, not the word (Click the colour, not the word)
     * 
     * Create a simple UI with text on it.
     * pick a colour word (Red, Blue, Green, Yellow)
     * colour the text different to word
     * User has to say what colour the text is.
     * 
     */

}
