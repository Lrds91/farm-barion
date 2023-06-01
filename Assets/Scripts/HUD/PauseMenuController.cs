using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optPanel;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        player.isPaused = true;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        player.isPaused = false;
    }

    public void OpenOptions()
    {
        pausePanel.SetActive(false);
        optPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void CloseGame()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
