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

    private bool _isRunning;
    private Vector2 _direction;
    private bool _isRolling;

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

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
    }

    private void Update()
    {
        //"salva" a posição que o jogador de acordo com o botão pressionado (dir-esq, cima-baixo)
        OnInput();
        OnRun();
        OnRolling();

    }

    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement

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

    #endregion
}
