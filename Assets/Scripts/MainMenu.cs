using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
  /*  public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 1);
    }
*/
    public void EasyLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 1);
    }

    public void MediumLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 2);
    }

    public void HardLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 3);
    }

    public void QuitGame()
    {
        Application.Quit();  
    }
}
