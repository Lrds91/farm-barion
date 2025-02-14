using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private PlayerAnim player;
    private Player playerDamage;
    private Animator anim;
    private Skeleton skeleton;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerDamage = FindObjectOfType<Player>();
        player = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>();
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void Attack()
    {
        if(!skeleton.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                //colis�o com player detectada, chama anima��o de dano
                player.OnHit();
                playerDamage.TakeDamage(2);
                playerDamage.UpdateHPBar();
            }
        }
        
    }

    public void OnHit()
    {
        if(skeleton.currentHealth <= 0)
        {
            skeleton.isDead = true;
            anim.SetTrigger("death");

            Destroy(skeleton.gameObject, 2f);
        }
        else
        {
            anim.SetTrigger("hit");
            skeleton.currentHealth--;

            skeleton.healthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
