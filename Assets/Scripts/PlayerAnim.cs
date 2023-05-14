using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;
    
    private Player player;
    private Animator anim;
    private Fishing cast;
    private bool isHitting;
    private float recoveryTime = 1.5f;
    private float timeCount;

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

        if(isHitting)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }

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
        if (player.isAttacking)
        {
            anim.SetInteger("Transition", 6);
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

    #region Attack

    public void onAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if(hit != null)
        {
            //atacou o inimigo
            hit.GetComponentInChildren<AnimationControl>().OnHit();
        }
        else
        {

        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
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

    public void OnHit()
    {
        if(!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
        }
    }
}
