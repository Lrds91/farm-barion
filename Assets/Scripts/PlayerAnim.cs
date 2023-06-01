using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPointDown;
    [SerializeField] private Transform attackPointUp;
    [SerializeField] private Transform attackPointLeft;
    [SerializeField] private Transform attackPointRight;
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
        //OnRun();

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
                if(anim.GetCurrentAnimatorStateInfo(0).IsName("roll") == false) //0 é o numero da layer. se a animação roll não estiver sendo executada
                {
                    anim.SetTrigger("isRoll");
                }

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

    //void OnRun()
    //{
    //    if(player.isRunning && player.direction.sqrMagnitude > 0)
    //    {
    //        anim.SetInteger("Transition", 2);
    //    }
    //}

    #endregion

    #region Attack

    public void onAttack()
    {
        Collider2D hitDown = Physics2D.OverlapCircle(attackPointDown.position, radius, enemyLayer);
        Collider2D hitUp = Physics2D.OverlapCircle(attackPointUp.position, radius, enemyLayer);
        Collider2D hitLeft = Physics2D.OverlapCircle(attackPointLeft.position, radius, enemyLayer);
        Collider2D hitRight = Physics2D.OverlapCircle(attackPointRight.position, radius, enemyLayer);

        if (hitDown != null)
        {
            //atacou o inimigo
            hitDown.GetComponentInChildren<AnimationControl>().OnHit();
        }
        else if (hitUp != null)
        {
            hitUp.GetComponentInChildren<AnimationControl>().OnHit();
        }
        else if (hitLeft != null)
        {
            hitLeft.GetComponentInChildren<AnimationControl>().OnHit();
        }
        else if (hitRight != null)
        {
            hitRight.GetComponentInChildren<AnimationControl>().OnHit();
        }
        else
        {

        }
    }

    public void onAttackPaladin()
    {
        Collider2D hitDown = Physics2D.OverlapCircle(attackPointDown.position, radius, enemyLayer);
        Collider2D hitUp = Physics2D.OverlapCircle(attackPointUp.position, radius, enemyLayer);
        Collider2D hitLeft = Physics2D.OverlapCircle(attackPointLeft.position, radius, enemyLayer);
        Collider2D hitRight = Physics2D.OverlapCircle(attackPointRight.position, radius, enemyLayer);

        if (hitDown != null)
        {
            //atacou o inimigo
            hitDown.GetComponentInChildren<PaladinAnim>().OnHit();
        }
        else if (hitUp != null)
        {
            hitUp.GetComponentInChildren<PaladinAnim>().OnHit();
        }
        else if (hitLeft != null)
        {
            hitLeft.GetComponentInChildren<PaladinAnim>().OnHit();
        }
        else if (hitRight != null)
        {
            hitRight.GetComponentInChildren<PaladinAnim>().OnHit();
        }
        else
        {

        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointDown.position, radius);
        Gizmos.DrawWireSphere(attackPointUp.position, radius);
        Gizmos.DrawWireSphere(attackPointLeft.position, radius);
        Gizmos.DrawWireSphere(attackPointRight.position, radius);
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
        if(!isHitting && player.isDead == false)
        {
            anim.SetTrigger("hit");

            isHitting = true;
        }
    }
}
