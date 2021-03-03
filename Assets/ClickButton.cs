using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{

    public Material lightMat;
    public Material normalMat;

    private Renderer myR;

    private void Awake()
    {
        myR = GetComponent<Renderer>();
        myR.enabled = true;
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        ClickedColor();
    }

    private void OnMouseUp()
    {
        UnClickedColor();
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
