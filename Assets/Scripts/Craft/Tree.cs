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
    //m�todo que detecta colis�o com machado
    public void onHit()
    {
        treeHealth--;
        anim.SetTrigger("isHit"); //trigga anima��o de hit

        if (treeHealth <= 0) //se HP da �rvore for <= 0 ent�o ir� instanciar os drops
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
