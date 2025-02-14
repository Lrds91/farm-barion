using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalWood;

    [SerializeField] private ParticleSystem leafes;

    private bool isCut;

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
        anim.SetTrigger("isHit"); //ativa a anima��o de hit
        leafes.Play();

        if (treeHealth <= 0) //se HP da �rvore for <= 0 ent�o ir� instanciar os drops
        {
            for(int i = 0; i < Random.Range(1,totalWood); i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), transform.rotation); //define posi��o aleat�ria de spawn do drop
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
