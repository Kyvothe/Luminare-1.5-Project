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
   public enum PlayerActionState {Default, Attack, Hops, Jump, Peck, Flutter}
    
    #region Inspector Variables
    
    [Header("Player States")]
    [SerializeField] private PlayerMovementState playerMovementState;
    [SerializeField] private PlayerDirectionState playerDirectionState;
    [SerializeField] private PlayerActionState playerActionState;

    [Header("Action Setup")] 
    [SerializeField] private float currentJumpForce;

    [SerializeField] private bool canBigJump;

    [Header("Ground Setup")] 
    [SerializeField] private Vector2 groundBoxPos;
    [SerializeField] private Vector2 groundBoxSize;
    [SerializeField] private LayerMask groundLayer;

    
    #endregion Inspector Variables
    
    #region private Variables
    
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    
    private Vector2 _moveInput;

    private float _currentSpeed;
    
    private float bigJumpForce = 5f;
    private float hopsForce = 3f;

    private bool _isGrounded;
    private bool _canJump = true;
    
    #endregion
    
    #region Unity Event Funtions

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _currentSpeed = 5f;
        
        SetInputActions();
    }

    private void SetInputActions()
    {
       _inputActions = new InputSystem_Actions();
       _moveAction = _inputActions.Player.Move;
       _jumpAction = _inputActions.Player.Jump;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _moveAction.performed += Move;
        _moveAction.canceled += Move;

        _jumpAction.performed += Jump;
    }

    private void FixedUpdate()
    {
        CheckIsGrounded();
        
        _rb.linearVelocityX = _moveInput.x * _currentSpeed;
        
        UpdateAnimator();
    }
    
    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;

        _jumpAction.performed -= Jump;
    }
    
    #endregion
    
    #region Physics

    private void CheckIsGrounded()
    {
        _isGrounded = Physics2D.OverlapBox((Vector2)transform.position + groundBoxPos, groundBoxSize, 0, groundLayer);
    }
    
    #endregion
    
    #region Input

    private void Move(InputAction.CallbackContext ctx)
    {
       _moveInput = ctx.ReadValue<Vector2>();
       
       playerMovementState = (_moveInput.x == 0) ? PlayerMovementState.Idle : PlayerMovementState.Move;

       if (_moveInput.x < 0) //facing left
       {
           transform.rotation = Quaternion.Euler(0, 180, 0);
           playerDirectionState = PlayerDirectionState.Left;
       }
       
       else if (_moveInput.x > 0) //facing right
       {
           transform.rotation = Quaternion.Euler(0, 0, 0);
           playerDirectionState = PlayerDirectionState.Right;
       }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (!_isGrounded) return;

        if (!_canJump) return;
        
        currentJumpForce = canBigJump ? bigJumpForce : hopsForce;
        
        _canJump = false;
        _rb.AddForce(Vector2.up * currentJumpForce, ForceMode2D.Impulse);
        SetActionId(1);
        playerActionState = canBigJump ? PlayerActionState.Jump : PlayerActionState.Hops;
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
    
    public void EndJump()
    {
        playerActionState = PlayerActionState.Default;
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
