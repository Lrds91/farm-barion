using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator anim;
    private Fishing cast;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        cast = FindObjectOfType<Fishing>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();
    }

    #region Movement

    private void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if(player.isRolling) //se isRolling for true, então altera a transition para 3 e executa a animação
            {
                anim.SetTrigger("isRoll");
            }
            else //se não executa a animação de caminhada
            {
                anim.SetBool("isRoll", false);
                anim.SetInteger("Transition", 1);
            }
        }
        else
        {
            anim.SetInteger("Transition", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if(player.isCutting)
        {
            anim.SetInteger("Transition", 3);
        }

        if (player.isDigging)
        {
            anim.SetInteger("Transition", 4);
        }

        if (player.isWatering)
        {
            anim.SetInteger("Transition", 5);
        }
    }

    void OnRun()
    {
        if(player.isRunning)
        {
            anim.SetInteger("Transition", 2);
        }
    }

    #endregion

    //chamado quando o jogador pressiona F na lagoa
    public void OnCasting() //cuida da animação de pescaria
    {
        anim.SetTrigger("isFishing");
        player.isPaused = true;
    }

    //chamado quando termina de executar a animação de pescaria
    public void OnFishEnd() //puxa o onFishing do script Fishing e faz as verificações de sucesso/falha na pesca
    {
        cast.onFishing();
        player.isPaused = false;
    }

    public void OnHammeringStarted()
    {
        anim.SetBool("constructing", true);
    }

    public void OnHammeringEnded()
    {
        anim.SetBool("constructing", false);
    }
}
