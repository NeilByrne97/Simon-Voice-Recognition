using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{

    public Material lightMat;
    public Material normalMat;

    private Renderer myR;
    private Vector3 myTP;

    public int myNumber = 99; 
    public RobotLogic myLogic;
    public delegate void ClickEV(int number);

    public event ClickEV onClick;

    private void Awake()
    {
        myR = GetComponent<Renderer>();
        myR.enabled = true;
        myTP = transform.position;
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(myLogic.player)
        {
            ClickedColor();
            transform.position = new Vector3 (myTP.x, -0.2f, myTP.z);
            onClick.Invoke(myNumber);
        }
    }

    private void OnMouseUp()
    {
        UnClickedColor();
        transform.position = new Vector3 (myTP.x, myTP.y, myTP.z);
    }

    public void ClickedColor()
    {
        myR.sharedMaterial = normalMat;
    }

    public void UnClickedColor()
    {
        myR.sharedMaterial = lightMat;
    }

}
