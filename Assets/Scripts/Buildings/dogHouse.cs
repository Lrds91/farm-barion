using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogHouse : MonoBehaviour
{
    [Header("Ammounts")]
    [SerializeField] private int woodReq;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;

    [Header("Components")]
    [SerializeField] private GameObject houseCollider;
    [SerializeField] private SpriteRenderer House;
    [SerializeField] private Transform point;

    
    private bool detectPlayer;
    private Player player;
    private PlayerAnim playerAnim;
    private PlayerItems playerItems;

    private float timeCount; //contador de tempo para preparar a casa de cachorro
    private bool isBeggining;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
        playerItems = player.GetComponent<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer && Input.GetKeyDown(KeyCode.E) && playerItems.totalWood >= woodReq)
        {
            //construção é inicializada
            isBeggining = true;
            playerAnim.OnHammeringStarted();
            House.color = startColor;
            player.transform.position = point.position; //move o player para o point a fim de ajustar animação para o lado certo
            player.isPaused = true;
            playerItems.totalWood -= woodReq;
        }
        if(isBeggining)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= timeAmount)
            {
                //casa é finalizada
                playerAnim.OnHammeringEnded();
                House.color = endColor;
                player.isPaused = false;
                houseCollider.SetActive(true);
            }
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
