using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;
    public Settings dialogue;

    bool playerHit;

    private List<string> sentences = new List<string>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && playerHit)
        {
            DialogManager.instance.Speech(sentences.ToArray());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Speaker();
    }


    void Speaker()
    {
        for(int i = 0; i < dialogue.dialogue.Count; i++)
        {
            switch(DialogManager.instance.language)
            {
                case DialogManager.idiom.pt:
                    sentences.Add(dialogue.dialogue[i].sentence.Portuguese);
                    break;
                case DialogManager.idiom.eng:
                    sentences.Add(dialogue.dialogue[i].sentence.English);
                    break;
            }
        }
    }

    // fixed update é usado pela física
    void FixedUpdate()
    {
        InitiateDialog();
    }

    void InitiateDialog()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        
        if(hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
        }
    }

    private void OnDrawGizmosSelected() //desenha um gizmo na tela para que a colisão do diálogo apareça na unity
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }

}
