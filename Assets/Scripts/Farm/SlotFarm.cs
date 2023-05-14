using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class SlotFarm : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;
    [Header("Components")]

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount; //tempo que tem que cavar para criar um buraco
    [SerializeField] private int waterAmount; //total de água para nascer uma cenoura

    [SerializeField] private bool detecting;
    private bool isPlayer; //indica se o player está encostando

    private int initialDigAmount;
    private int currentWater;

    private bool dugHole;
    private bool carrotPlant;

    PlayerItems playerItems;

    private void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        initialDigAmount = digAmount;
    }

    private void Update()
    {
        if (dugHole)
        {
            if (detecting)
            {
                currentWater++;
            }

            if (currentWater >= waterAmount / 2 && !carrotPlant)
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRenderer.sprite = carrot;

                carrotPlant = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && carrotPlant && isPlayer)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRenderer.sprite = hole;
                playerItems.carrots++;
                currentWater = 0;
            }
        }
    }

    public void onHit()
    {
        digAmount--;

        if(digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
        }



        //if (digAmount <= 0) 
        //{
        //    //plantar cenoura
        //    spriteRenderer.sprite = carrot;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shovel"))
        {
            onHit();
        }

        if(collision.CompareTag("Water"))
        {
            detecting = true;
        }
        if(collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detecting = false;
        }
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
