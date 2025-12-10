using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
   public static readonly int Hash_MovementValue = Animator.StringToHash("MovementValue");
    
    #region Inspector Variables
    #endregion Inspector Variables
    
    #region private Variables
    
    private Rigidbody2D _rb;
    private Animator _animator;
    
    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    
    private Vector2 _moveInput;

    private float _currentSpeed;
    
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
    
    #region Input

    private void Move(InputAction.CallbackContext ctx)
    {
       _moveInput = ctx.ReadValue<Vector2>();

       if (_moveInput.x < 0) //facing left
       {
           transform.rotation = Quaternion.Euler(0, 180, 0);
       }
       
       if (_moveInput.x > 0) //facing right
       {
           transform.rotation = Quaternion.Euler(0, 0, 0);
       }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        
    }
    
    #endregion
    
    #region Animation
    
    private void UpdateAnimator()
    {
        _animator.SetFloat(Hash_MovementValue, Mathf.Abs(_rb.linearVelocity.x));
    }
    
    #endregion
}
