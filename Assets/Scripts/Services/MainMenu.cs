using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoBackToAR()
    {
        SceneManager.LoadScene(1);
    }
    public void goBackToRoutes()
    {
        SceneManager.LoadScene(2);
    }
    public void ChooseRoute()
    {
        SceneManager.LoadScene(3);
    }
}
