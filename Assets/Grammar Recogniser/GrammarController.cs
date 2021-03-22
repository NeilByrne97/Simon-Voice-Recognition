using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;  // for stringbuilder
using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser

/*
 *  Uses English US in the settings - Keyboard (on the taskbar), Region, Preferred Language and Speech in Settings
 */

public class GrammarController : MonoBehaviour
{
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private GrammarRecognizer gr;

    private void Start()
    {
        actions.Add("play", Play);

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
        // Move pawn from C2 to C4 - Piece, Start, Finish
        // semantic meanings are returned as key/value pairs
        // Piece/"pawn", Start/"C2", Finish/"C4"
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
        Debug.Log("Play method running!!");
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
