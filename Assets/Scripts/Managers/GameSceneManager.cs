using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    private Player player;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] ScreenTint screenTint;
    [SerializeField] CameraConfiner cameraConfiner;

    string currentScene;
    AsyncOperation unload;
    AsyncOperation load;

    bool respawnTransition;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        player = GetComponent<Player>();
    }

    public void InitSwitchScene(string to, Vector3 targetPosition)
    {
        StartCoroutine(Transition(to, targetPosition));
    }

    public void Respawn(Vector3 respawnPointPosition, string respawnPointScene)
    {
        respawnTransition = true;


        if (currentScene != respawnPointScene)
        {
            InitSwitchScene(respawnPointScene, respawnPointPosition);
        }
        else
        {
            MovePlayer(respawnPointPosition);
        }
    }

    IEnumerator Transition(string to, Vector3 targetPosition)
    {
        screenTint.Tint();

        yield return new WaitForSeconds(1f / screenTint.speed + 0.1f);

        SwitchScene(to, targetPosition);

        while(load != null & unload != null) 
        {
            if(load.isDone)
            {
                load = null;
            }
            if (unload.isDone)
            {
                unload = null;
            }
            yield return new WaitForSeconds(0.1f);
        }
        screenTint.UnTint();
        cameraConfiner.UpdateBounds();


    }

    // Update is called once per frame
    public void SwitchScene(string to, Vector3 targetPosition)
    {
        load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        unload = SceneManager.UnloadSceneAsync(currentScene);
        currentScene = to;
        MovePlayer(targetPosition);
    }

    private void MovePlayer(Vector3 targetPosition)
    {
        Transform playerTransform = GameManager.instance.player.transform;

        CinemachineBrain currentCamera = Camera.main.GetComponent<CinemachineBrain>();
        currentCamera.ActiveVirtualCamera.OnTargetObjectWarped(playerTransform, targetPosition - playerTransform.position);

        playerTransform.position = new Vector3(targetPosition.x, targetPosition.y, playerTransform.position.z);

        if (respawnTransition)
        {
            playerTransform.GetComponent<Player>().FullHeal();
            playerTransform.GetComponent<Player>().isPaused = false;
            respawnTransition = false;
        }
    }
}
