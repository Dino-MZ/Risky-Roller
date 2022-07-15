using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerSO _playerSO;

    private Rigidbody2D _rb;

    public static bool CanMove, IsDashing;

    private Vector2 _moveDir;

    private float _rollCounter, _rollCoolCounter;



    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        CanMove = true;
    }

    
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRoll();
    }

    void HandleInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDir = new Vector2(moveX, moveY).normalized;
    }

    void HandleMovement()
    {
        if (CanMove && !IsDashing)
        {
            _rb.velocity = _moveDir * _playerSO.Speed;
        }
    }

    void HandleRoll()
    {
        if (Input.GetKey(KeyCode.Space) && CanMove)
        {
            if(_rollCoolCounter <= 0 && _rollCounter <= 0)
            {
                _rb.velocity = _moveDir * _playerSO.RollSpeed;
                IsDashing = true;
                _rollCounter = _playerSO.RollLength;
            }
        }

        if(_rollCounter > 0)
        {
            _rollCounter -= Time.deltaTime;

            if(_rollCounter <= 0)
            {
                _rollCoolCounter = _playerSO.RollCoolDown;
                IsDashing = false;
            }
        }

        if(_rollCoolCounter > 0)
        {
            _rollCoolCounter -= Time.deltaTime;
        }
    }
}
