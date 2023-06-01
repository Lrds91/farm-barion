using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;
    public Settings dialogue;
    public GameObject dialogueIcon;

    public bool playerHit;

    public List<string> sentences = new List<string>();
    public List<string> actorName = new List<string>();
    public List<Sprite> spriteActor = new List<Sprite>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && playerHit)
        {
            DialogManager.instance.Speech(sentences.ToArray(), actorName.ToArray(),spriteActor.ToArray());
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

            actorName.Add(dialogue.dialogue[i].actorName);
            spriteActor.Add(dialogue.dialogue[i].portrait);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InitiateDialog();
        dialogueIcon.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueIcon.SetActive(false);
    }

}
