using UnityEngine;
using System.Collections;
using System.Collections.Generic;   
using UnityEngine.SceneManagement;

public class menupoziomu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OpenLevel(int levelId)
    {
        
        string levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }


}
