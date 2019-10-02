using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private Animator _animator;

    private new Rigidbody2D rigidbody;

    private bool _jump;
    private bool _isGrounded;
    private bool _facingRight;

    private float _horizontalMove;

    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Speed = Animator.StringToHash("Speed");

    #endregion

    #region Public Fields

    public float checkRadius = 0.5f;
    public float speed = 5.0f;
    public float jumpForce = 5.0f;

    public LayerMask whatIsGrounded;
    public Transform groundCheck;

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _facingRight = true;
        _jump = false;
        _isGrounded = false;
    }

    void Update()
    {
        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _jump = true;
            rigidbody.velocity = new Vector2(_horizontalMove * speed / 2, jumpForce);
        }
    }

    void FixedUpdate()
    {
        float jumpVelocity = _horizontalMove * speed / 2;
        
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        _animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGrounded);

        _horizontalMove = Input.GetAxis("Horizontal");
        // Catch the horizontal input.
        if (_isGrounded)
            jumpVelocity = _horizontalMove * speed;

        rigidbody.velocity = new Vector2(jumpVelocity, rigidbody.velocity.y);

        if (_horizontalMove > 0 && !_facingRight)
            Flip();
        else if (_horizontalMove < 0 && _facingRight)
            Flip();

        if (_jump)
        {
            _animator.SetTrigger(Jump);
            _jump = false;
        }
    }

    #endregion

    #region Private Methods

    void Flip()
    {
        _facingRight = !_facingRight;

        Vector3 transformLocalScale = transform.localScale;
        transformLocalScale.x *= -1;
        transform.localScale = transformLocalScale;
    }

    #endregion
}