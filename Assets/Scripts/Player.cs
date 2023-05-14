using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPaused;
    
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    private float initialSpeed;

    private Rigidbody2D rig;
    private PlayerItems playerItems;

    private bool _isRunning;
    private Vector2 _direction;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;
    private bool _isAttacking;


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
            OnRolling();
            OnCutting();
            OnDigging();
            OnWatering();
            OnAttacking();
        }
        
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

    void OnRolling()
    {
        if(Input.GetMouseButtonDown(1)) //botão direito do mouse
        {
            _isRolling = true;
        }

        if(Input.GetMouseButtonUp(1))
        {
            _isRolling = false;
        }
    }

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

    #endregion
}
