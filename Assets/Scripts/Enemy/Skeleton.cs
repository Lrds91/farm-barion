using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("Status")]
    public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;


    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;



    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType < Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            //faz ir em dire��o ao player
            agent.SetDestination(player.transform.position);

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                //chegou no limite de dist�ncia /para o inimigo
                animControl.PlayAnim(2);
            }
            else
            {
                //inimigo segue o player
                animControl.PlayAnim(1);
            }

            float posX = player.transform.position.x - transform.position.x;

            if (posX > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }

    }
}
