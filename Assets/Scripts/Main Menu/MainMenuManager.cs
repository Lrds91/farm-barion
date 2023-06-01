using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string levelNameID;
    [SerializeField] private string essentialsID;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject htpPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject aboutPanel;
    [SerializeField] private Settings language;
    [SerializeField] private AudioSource audioSource;

    public void Play()
    {
        SceneManager.LoadScene(levelNameID, LoadSceneMode.Single);
        SceneManager.LoadScene(essentialsID, LoadSceneMode.Additive);   
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenHTP()
    {
        mainMenuPanel.SetActive(false);
        htpPanel.SetActive(true);
    }

    public void CloseHTP()
    {
        htpPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenAbout()
    {
        mainMenuPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        aboutPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        aboutPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void CloseGame()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }

    public void ENLanguage()
    {
        
    }
}
