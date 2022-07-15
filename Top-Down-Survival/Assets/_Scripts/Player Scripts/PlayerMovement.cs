using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerSO _playerSO;

    private Rigidbody2D _rb;

    public static bool CanMove;

    private Vector2 _moveDir;

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
    }

    void HandleInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDir = new Vector2(moveX, moveY).normalized;
    }

    void HandleMovement()
    {
        if (CanMove)
        {
            _rb.velocity = _moveDir * _playerSO.speed;
        }
    }
}
