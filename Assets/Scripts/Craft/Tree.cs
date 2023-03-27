using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalWood;

    [SerializeField] private ParticleSystem leaves;

    private bool isCut;

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
        leaves.Play();

        if (treeHealth <= 0) //se HP da árvore for <= 0 então irá instanciar os drops
        {
            for(int i = 0; i < totalWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f), transform.rotation); //define posição aleatória de spawn do drop
            }
            
            anim.SetTrigger("cut");

            isCut = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe") && !isCut)
        {
            onHit();
        }
    }


}
