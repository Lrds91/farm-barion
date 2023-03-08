using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng
    }

    public idiom language;
    
    [Header("Components")]
    public GameObject dialogObj; //janela do diálogo
    public Image profileSprite; //sprite do perfil
    public Text speechText; //texto de fala
    public Text actorNameText; //nome do NPC

    [Header("Settings")]
    public float conversationSpeed; //velocidade da fala

    //variáveis de controle
    private bool isShowing; //se a janela está visível
    private int index; //qtd de texto que tem dentro de uma fala
    private string[] sentences;

    public static DialogManager instance;

    private void Awake() //chamado antes de todos os Start() na hierarquia de execução de script
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TypeSentence() //método que controla a velocidade de aparição da fala
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(conversationSpeed);
        }
    }

    public void NextSentence() //pular para a próxima frase/fala
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else //quando terminam os textos
            {
                speechText.text = "";
                index = 0;
                dialogObj.SetActive(false);
                sentences = null;
                isShowing = false; //faz o NPC falar novamente ao encerrar o diálogo anterior
            }
        }
    }

    public void Speech(string[] txt) //chamar a fala do NPC
    {
        if(!isShowing) //se não estiver falando
        {
            dialogObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
