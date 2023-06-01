using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinAnim : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private PlayerAnim player;
    private Player playerDamage;
    private Animator anim;
    private Paladin paladin;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerAnim>();
        playerDamage = FindObjectOfType<Player>();
        paladin = GetComponentInParent<Paladin>();
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void Attack()
    {
        if (!paladin.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                //colisão com player detectada, chama animação de dano
                player.OnHit();
                playerDamage.TakeDamage(5);
                playerDamage.UpdateHPBar();
            }
        }

    }

    public void OnHit() //chama animação de hit
    {
        if (paladin.currentHealth <= 0)
        {
            paladin.isDead = true;
            anim.SetTrigger("Death");

            Destroy(paladin.gameObject, 2f);
        }
        else
        {
            anim.SetTrigger("Hurt");
            paladin.currentHealth--;

            paladin.healthBar.fillAmount = paladin.currentHealth / paladin.totalHealth;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
