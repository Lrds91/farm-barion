using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    private float initialSpeed;

    private int index;
    private Animator anim;

    public List<Transform> paths = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = speed;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogManager.instance.isShowing) //se o jogador chegar no range de conversação e apertar F o NPC irá parar, caso o contrário irá retomar a velocidade normal
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }
        
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, paths[index].position) < 0.001f) //verifica se o NPC chegou próximo ao path desejado
        {
            if(index < paths.Count - 1)
            {
                index++;
                //index = Random.Range(0, paths.Count - 1); caminha aleatóriamente entre os paths
            }
            else
            {
                index = 0;
            }
        }

        Vector2 direction = paths[index].position - transform.position;

        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if(direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
