using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class Stat
{
    public int totalHealth;
    public int currentHealth;


    public Stat(int cur, int max)
    {
        totalHealth = max;
        currentHealth = cur;
    }

    public void Substract(int amount)
    {
        currentHealth -= amount;
    }

    internal void Add(int amount)
    {
        currentHealth += amount;
        if(currentHealth > totalHealth)
        {
            currentHealth = totalHealth;
        }
    }
    internal void SetToMax()
    {
            currentHealth = totalHealth;
    }

}
public class Player : MonoBehaviour
{
    public bool isPaused;

    public Stat hp;

    [SerializeField] UIController hpBar;
    private PlayerItems playerItems;

    public void TakeDamage(int amount)
    {
        if(isDead == true) { return; }
        hp.Substract(amount);
        if (hp.currentHealth <= 0)
        {
            Dead();
        }
        UpdateHPBar();
    }

    public void Heal()
    {
        if (playerItems.carrots > 0 & Input.GetKeyDown(KeyCode.H))
        {
            hp.Add(3);
            UpdateHPBar();
            playerItems.carrots--;
        }
        if (playerItems.fishes > 0 & Input.GetKeyDown(KeyCode.G))
        {
            hp.Add(6);
            UpdateHPBar();
            playerItems.fishes--;
        }
    }

    public void FullHeal()
    {
        hp.SetToMax();
    }
    
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    private float initialSpeed;
    public Animator anim;

    PlayerRespawn playerRespawn;


    private Rigidbody2D rig;
  

    private bool _isRunning;
    private Vector2 _direction;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;
    private bool _isAttacking;
    private bool _isDead;

    public bool isDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }

    [HideInInspector] public int handlingObj; //esconde a informação desnecessária do inspector

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }

    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }

    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }

    }

    public bool isCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }

    }

    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }

    }

    public bool isWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }

    }
    public bool isAttacking
    {
        get { return _isAttacking; }
        set { _isAttacking = value; }

    }

    public bool IsCutting { get => _isCutting; set => _isCutting = value; }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
        playerItems = GetComponent<PlayerItems>();
        anim = GetComponent<Animator>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        if(!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) //Alpha são os números acima do teclado
            {
                handlingObj = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                handlingObj = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                handlingObj = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                handlingObj = 3;
            }

            //"salva" a posição que o jogador de acordo com o botão pressionado (dir-esq, cima-baixo)
            OnInput();
            OnRun();
            //OnRolling();
            OnCutting();
            OnDigging();
            OnWatering();
            OnAttacking();
            UpdateHPBar();
            Heal();
        }
        
    }

    public void UpdateHPBar()
    {

        hpBar.Set(hp.currentHealth, hp.totalHealth);
    }

    private void FixedUpdate()
    {
        if(!isPaused)
        {
            OnMove();
        }
    }

    #region Movement

    void OnCutting()
    {
        if(handlingObj == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }
        else
        {
            isCutting = false;
        }
    }

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(_direction.x != 0f || _direction.y != 0f)
        {
            anim.SetFloat("Horizontal", _direction.x);
            anim.SetFloat("Vertical", _direction.y);
        }

    }

    private void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
    }

    void OnRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }

    //void OnRolling()
    //{
    //    if(Input.GetMouseButtonDown(1)) //botão direito do mouse
    //    {
    //        _isRolling = true;
    //    }

    //    if(Input.GetMouseButtonUp(1))
    //    {
    //        _isRolling = false;
    //    }
    //}

    void OnDigging()
    {
        if(handlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }
        else
        {
            isDigging = false;
        }
    }
    void OnWatering()
    {
        if (handlingObj == 2)
        {
            if (Input.GetMouseButtonDown(0) && playerItems.currentWater > 0)
            {
                playerItems.currentWater--;
                isWatering = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isWatering = false;
                speed = initialSpeed;
            }
        }
        else
        {
            isWatering = false;
        }
    }

    void OnAttacking()
    {
        if (handlingObj == 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isAttacking = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isAttacking = false;
                speed = initialSpeed;
            }
        }
        else
        {
            isAttacking = false;
        }
    }

    void Dead()
    {
        isDead = true;
        anim.SetBool("death", true);
        isPaused = true;
        playerRespawn.StartRespawn();
        anim.SetBool("death", false);
    }

    #endregion


}
