using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerSO _playerSO;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    public static bool CanMove, IsDashing;

    private Animator _animator;

    private Rigidbody2D _rb;

    private Vector2 _moveDir;

    private float _rollCoolCounter;

    private const string WALK = "Walk";
    private const string IDLE = "Idle";
    private const string ROLL = "Roll";

    private string currentState;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        CanMove = true;
        currentState = IDLE;
    }

    
    void Update()
    {
        HandleInput();

        if (IsDashing)
        {
            ChangeAnimationState(ROLL);
        }
        else if(_moveDir.x > 0)
        {
            _spriteRenderer.flipX = false;
            ChangeAnimationState(WALK);
        }
        else if(_moveDir.x < 0)
        {
            _spriteRenderer.flipX = true;
            ChangeAnimationState(WALK);
        }
        else
        {
            ChangeAnimationState(IDLE);
        }
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
            if(_rollCoolCounter <= 0)
            {
                _rb.velocity = _moveDir * _playerSO.RollSpeed;
                IsDashing = true;
            }
        }

        if(_rollCoolCounter > 0)
        {
            _rollCoolCounter -= Time.deltaTime;
        }
    }

    public void EndRoll()
    {
        IsDashing = false;
        _rollCoolCounter = _playerSO.RollCoolDown;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        _animator.Play(newState);

        currentState = newState;
    }
}
