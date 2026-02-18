using UnityEngine;
using System;
using UnityEditor.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
   public static readonly int Hash_MovementValue = Animator.StringToHash("MovementValue");
   public static readonly int Hash_ActionTrigger = Animator.StringToHash("ActionTrigger");
   public static readonly int Hash_ActionId = Animator.StringToHash("ActionId");
   
   public enum PlayerMovementState {Idle, Move}
   public enum PlayerDirectionState {Left, Right}
   public enum PlayerActionState {Default, Attack, Hops, Jump, DoubleJump, Fly}
    
    #region Inspector Variables
    
    
    [Header("Player States")]
    [SerializeField] private PlayerMovementState playerMovementState;
    [SerializeField] private PlayerDirectionState playerDirectionState;
    [SerializeField] private PlayerActionState playerActionState;

    [Header("Action Setup")] 
    [SerializeField] private float currentJumpForce;
    
    [SerializeField] private AnimationCurve curve;

    [SerializeField] private float flightDuration;
    [SerializeField] private float timeInAir;

    public float coyoteTime;
    private float _coyoteTimeCounter;

    public int jumpCounter = 0;


    public  bool canBigJump;
    public  bool canDoubleJump;
    public bool doubleJump = true;
    public bool _canAttack;
    public bool canFly;
    private bool _hasLanded = true;

    [Header("Ground Setup")] 
    [SerializeField] private Vector2 groundBoxPos;
    [SerializeField] private Vector2 groundBoxSize;
    [SerializeField] private LayerMask groundLayer;

    
    #endregion Inspector Variables
    
    #region private Variables
    
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private PlayerOneWay _playerOneWay;
    
    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _interactAction;
    
    private Vector2 _moveInput;

    private float _currentSpeed;
    private float _walkingSpeed = 3f;
    private float _sprintingSpeed = 4f;
    
    private float bigJumpForce = 7f;
    private float hopsForce = 5f;
    public float flyForce = 3f;


    private bool _isGrounded;
    public bool _canJump = true;
    private bool _isAttacking;
    public bool _isFlying;

    private bool _paused = false;
    
    private Vector2 _flyVelocity;
    
    #endregion
    
    #region Unity Event Funtions

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        _playerOneWay = GetComponent<PlayerOneWay>();
        
        SetInputActions();

        _currentSpeed = _walkingSpeed;
    }

    private void SetInputActions()
    {
       _inputActions = new InputSystem_Actions();
       _moveAction = _inputActions.Player.Move;
       _sprintAction = _inputActions.Player.Sprint;
       _jumpAction = _inputActions.Player.Jump;
       _attackAction = _inputActions.Player.Attack;
       _interactAction = _inputActions.Player.Interact;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _moveAction.performed += Move;
        _moveAction.canceled += Move;
        
        _sprintAction.performed += Sprint;
        _sprintAction.canceled += Sprint;
        
        _jumpAction.started += Jump;
        _jumpAction.performed += StartFly;
        _jumpAction.canceled += StopFly;
        
        _attackAction.performed += Attack;
        
        _interactAction.performed += Interact;
    }

    private void FixedUpdate()
    {
        CheckIsGrounded();
        
        _rb.linearVelocityX = _moveInput.x * _currentSpeed;
        
        UpdateAnimator();

        ReturnIsAttacking();
        
        ExecuteFlying();
    }
    
    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        
        _sprintAction.performed -= Sprint;
        _sprintAction.canceled -= Sprint;

        _jumpAction.started -= Jump;
        _jumpAction.canceled -= StopFly;

        
        _attackAction.performed -= Attack;
        
        _interactAction.performed -= Interact;
    }
    
    #endregion
    
    #region Physics

    private void CheckIsGrounded()
    {
        _isGrounded = Physics2D.OverlapBox((Vector2)transform.position + groundBoxPos, groundBoxSize, 0, groundLayer);

        if (_isGrounded)
        {
            timeInAir = 0;
            playerActionState = PlayerActionState.Default;

            _coyoteTimeCounter = coyoteTime;
            
            _hasLanded = true;

            doubleJump = true;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
            _canJump = false;
        }
    }
    
    #endregion
    
    #region Input

    private void Move(InputAction.CallbackContext ctx)
    {
       _moveInput = ctx.ReadValue<Vector2>();
       
       playerMovementState = (_moveInput.x == 0) ? PlayerMovementState.Idle : PlayerMovementState.Move;

       if (_moveInput.y < 0) // S gedrueckt
       {
           _playerOneWay.CheckForOneWayPlatform();
       }
       
       if (_moveInput.x < 0) // facing left
       {
           transform.rotation = Quaternion.Euler(0, 180, 0);
           playerDirectionState = PlayerDirectionState.Left;
       }
       
       else if (_moveInput.x > 0) // facing right
       {
           transform.rotation = Quaternion.Euler(0, 0, 0);
           playerDirectionState = PlayerDirectionState.Right;
       }
    }

    private void Sprint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _currentSpeed = _sprintingSpeed;
        }

        if (!ctx.performed)
        {
            _currentSpeed = _walkingSpeed;
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (canDoubleJump)
        {
            DoubleJump();
            return;
        }
        
        if (!(_coyoteTimeCounter > 0f)) return;
        if (!_canJump) return;
        
        currentJumpForce = canBigJump ? bigJumpForce : hopsForce;
        
        _canJump = false;
        _rb.AddForce(Vector2.up * currentJumpForce, ForceMode2D.Impulse);
        SetActionId(1);
        playerActionState = canBigJump ? PlayerActionState.Jump : PlayerActionState.Hops;  
        
        _coyoteTimeCounter = 0f;
    }

    private void DoubleJump()
    {
        if (!canDoubleJump) return;
        if (!doubleJump) return;
        
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        
        _rb.AddForce(Vector2.up * bigJumpForce, ForceMode2D.Impulse);
        SetActionId(1);
        playerActionState = PlayerActionState.DoubleJump; 
        
        _coyoteTimeCounter = 0f; 
        
        doubleJump = false;
    }

    
    
    private void StartFly(InputAction.CallbackContext ctx)
    {
        if (canFly)
        {
            if (_hasLanded)
            {
                //_rb.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
                _isFlying = true;    
            }
        }
    }
    
    private void ExecuteFlying()
    {   
        if (!_isFlying) return;
        
        timeInAir += Time.deltaTime;

        if (timeInAir <= flightDuration)
        {
            SetActionId(20);

            _canJump = false;
            _hasLanded = false;
            
            float t = Mathf.Clamp01(timeInAir / flightDuration);

            _flyVelocity = new Vector2(_rb.linearVelocity.x, t * flyForce);
        
            _rb.linearVelocity = _flyVelocity;

            playerActionState = PlayerActionState.Fly;
        }
        else
        {
            _isFlying = false;
            _canJump = true;
        }
    }

    private void StopFly(InputAction.CallbackContext ctx)
    {
        _isFlying  = false; 
        playerActionState = PlayerActionState.Default;
        _canJump = true;
    }
    
    private void Attack(InputAction.CallbackContext ctx)
    {
       if (!_canAttack) return;
        
        if (!_isAttacking)
        {   
            Debug.Log("attack");
            _isAttacking = true;
            SetActionId(10);
            playerActionState = PlayerActionState.Attack;
        }
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        if (_isAttacking) return;
        
        gameObject.GetComponent<PlayerInteraction>().TryInteract();
    }
    
    public bool ReturnIsAttacking()
    {
        return _isAttacking;
    }

    public void SetPaused(bool value)
    {
        _paused = value;
    }
    
    #endregion

    #region Upgrade Toggles
    
    public void SetCanBigJump(bool value)
    {
        canBigJump = value;
    }
    
    public void SetCanDoubleJump(bool value)
    {
        canDoubleJump = value;
    }
    
    public void SetCanAttack(bool value)
    {
        _canAttack = value;
    }
    
    public void SetCanFly(bool value)
    {
        canFly = value;
    }
    
    #endregion
    
    #region Animation
    
    private void UpdateAnimator()
    {
        _animator.SetFloat(Hash_MovementValue, Mathf.Abs(_rb.linearVelocity.x));
        _animator.SetBool("isGrounded", _isGrounded);
    }

    private void SetActionId(int id)
    {
        _animator.SetTrigger(Hash_ActionTrigger);
        _animator.SetInteger(Hash_ActionId, id);
    }

    public void AnimationEnd(PlayerActionType playerActionType)
    {
        if (playerActionType == PlayerActionType.ActionAttack)
        {
            EndAttack();
        }
        
        if (playerActionType == PlayerActionType.ActionJump)
        {
           EndJump();
        }
    }
    
    private void EndJump()
    {
        playerActionState = PlayerActionState.Default;
        _canJump = true; 
    }
    
    private void EndAttack()
    {
        playerActionState = PlayerActionState.Default;
        _isAttacking = false;
        _canJump = true;
    }
    
    #endregion
    
    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + groundBoxPos, groundBoxSize);
    }
    
    #endregion
}