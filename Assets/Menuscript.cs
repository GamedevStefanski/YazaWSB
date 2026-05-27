using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class  MainMenu : MonoBehaviour
{
    public void OnCanvasGroupChanged()
    {
        gameObject.SetActive(true);
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}