using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Vector2 respawnPointPosition;
    [SerializeField] string respawnPointScene;
    
    internal void StartRespawn()
    {
        GameSceneManager.instance.Respawn(respawnPointPosition, respawnPointScene);
    }
}
