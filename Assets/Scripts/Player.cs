using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    [HideInInspector] public int handlingObj; //esconde a informa��o desnecess�ria do inspector

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

    public bool IsCutting { get => _isCutting; set => _isCutting = value; }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
        playerItems = GetComponent<PlayerItems>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) //Alpha s�o os n�meros acima do teclado
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

        //"salva" a posi��o que o jogador de acordo com o bot�o pressionado (dir-esq, cima-baixo)
        OnInput();
        OnRun();
        OnRolling();
        OnCutting();
        OnDigging();
        OnWatering();

    }

    private void FixedUpdate()
    {
        OnMove();
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
        if(Input.GetMouseButtonDown(1)) //bot�o direito do mouse
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
    }

    #endregion
}
