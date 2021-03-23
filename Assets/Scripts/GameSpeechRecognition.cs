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
    [SerializeField] GameObject Yellow;
    [SerializeField] GameObject Green;
    [SerializeField] GameObject Blue;
    [SerializeField] GameObject Red;
    [SerializeField] GameObject Orange;
    [SerializeField] GameObject Purple;

    [SerializeField] Material MatYellow;
    [SerializeField] Material MatGreen;
    [SerializeField] Material MatBlue;
    [SerializeField] Material MatRed;
    [SerializeField] Material MatOrange;
    [SerializeField] Material MatPurple;

    [SerializeField] Material MatYellowLight;
    [SerializeField] Material MatGreenLight;
    [SerializeField] Material MatBlueLight;
    [SerializeField] Material MatRedLight;
    [SerializeField] Material MatOrangeLight;
    [SerializeField] Material MatPurpleLight;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private GrammarRecognizer gr;

    protected string color = "";

    public RobotLogic robotLogic = new RobotLogic();
    [SerializeField] GameObject PauseMenu;

     public Vector3 savedPosition;

    private void Start()
    {    
        actions.Add("yellow", YellowColor);
        actions.Add("green", GreenColor);
        actions.Add("blue", BlueColor);
        actions.Add("red", RedColor);
        actions.Add("orange", OrangeColor);
        actions.Add("purple", PurpleColor);

        actions.Add("start", StartGame);
        actions.Add("pause", Pause);

        actions.Add("resume", Resume);
        actions.Add("menu", Menu);
        actions.Add("quit", Quit);


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

    // USER INTERFACE AND PAUSE MENU // 
    private void StartGame()
    {
        robotLogic.StartGame();
    }
    private void Pause()
    {
        PauseMenu.SetActive(true);
    }
    private void Resume()
    {
        PauseMenu.SetActive(false);
    }

    private void Menu()
    {
        SceneManager.LoadScene(0);
    }
    private void Quit()
    {
        Debug.Log("Quiting...");
        Application.Quit();  
    }

    // COLORS SPEECH RECOGNITION // 
    private void YellowColor()
    {
        Yellow.GetComponent<MeshRenderer> ().material = MatYellow;
        StartCoroutine(Click());
        //Yellow.GetComponent<MeshRenderer> ().material = MatYellowLight;
        robotLogic.ButtonClicked(0);
        Debug.Log("Yellow");    
    }
    private void GreenColor()
    {
        Green.GetComponent<MeshRenderer> ().material = MatGreen;
        StartCoroutine(Click());
        //Green.GetComponent<MeshRenderer> ().material = MatGreenLight;                
        robotLogic.ButtonClicked(1);    
    }
    private void BlueColor()
    {
        Blue.GetComponent<MeshRenderer> ().material = MatBlue;
        StartCoroutine(Click());
        //Blue.GetComponent<MeshRenderer> ().material = MatBlueLight;                
        robotLogic.ButtonClicked(2);
        Debug.Log("Blue");  
    }

    private void RedColor()
    {
        Red.GetComponent<MeshRenderer> ().material = MatRed;    
        StartCoroutine(Click());
        // Red.GetComponent<MeshRenderer> ().material = MatRedLight;                
        robotLogic.ButtonClicked(3);
        Debug.Log("Red");    }
    private void OrangeColor()
    {
         Orange.GetComponent<MeshRenderer> ().material = MatOrange;
        StartCoroutine(Click());
        // Orange.GetComponent<MeshRenderer> ().material = MatOrangeLight;                
        robotLogic.ButtonClicked(4);
        Debug.Log("Orange");
    }

    private void PurpleColor()
    {
        Purple.GetComponent<MeshRenderer> ().material = MatPurple;
        StartCoroutine(Click());
        // Purple.GetComponent<MeshRenderer> ().material = MatPurpleLight;                
        robotLogic.ButtonClicked(5);
        Debug.Log("Purple");
    }


    private IEnumerator Click()
    {
        yield return new WaitForSeconds(1f);
    }

    private void OnApplicationQuit()
    {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhraseRecognized;
            gr.Stop();
        }
    }


    public void ChangePositions()
    {
        savedPosition = Orange.transform.position;

        Orange.transform.position = Purple.transform.position;  
        Purple.transform.position = Green.transform.position;  
        Green.transform.position = Yellow.transform.position;  
        Yellow.transform.position = Red.transform.position;  
        Red.transform.position = Blue.transform.position;  
        Blue.transform.position = savedPosition;  


    }

}
