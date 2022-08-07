using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    public string nameOfTheMainHolder;
    public string nameOfTheOptionsHolder;
    private GameObject mainHolder;
    private GameObject optionsHolder;
    void Start()
    {
        mainHolder = GameObject.Find(nameOfTheMainHolder);
        optionsHolder = GameObject.Find(nameOfTheOptionsHolder);
        optionsHolder.SetActive(false);
    }
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Continue()
    {
        //Load the .xml file
        Debug.Log("Loading the game");
    }
    public void Options()
    {
        mainHolder.SetActive(false);
        optionsHolder.SetActive(true);
    }
    public void Exit()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }    
}
