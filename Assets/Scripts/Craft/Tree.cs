using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //método que detecta colisão com machado
    public void onHit()
    {
        treeHealth--;
        anim.SetTrigger("isHit"); //trigga animação de hit

        if (treeHealth <= 0) //se HP da árvore for <= 0 então irá instanciar os drops
        {
            anim.SetTrigger("cut");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe"))
        {
            onHit();
        }
    }


}
