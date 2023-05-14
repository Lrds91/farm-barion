using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private int percentage; //% de chance de pescar um peixe por tentativa
    [SerializeField] private GameObject fishPrefab;

    private PlayerItems player;
    private PlayerAnim playerAnim;

    private bool detectPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerItems>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer && Input.GetKeyDown(KeyCode.F))
        {
            playerAnim.OnCasting();
        }
    }

    public void onFishing()
    {
        int randomValue = Random.Range(1, 100);

        if(randomValue <= percentage)
        {
            //conseguir pescar o peixe
            Instantiate(fishPrefab, player.transform.position + new Vector3(-1f,0f,0f), Quaternion.identity);
        }
        else
        {
            //não pescou

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectPlayer = false;
        }
    }
}
