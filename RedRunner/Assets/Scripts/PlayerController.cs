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

    private Vector2 SavePlayerPos;

    private AudioSource audioSource;


    #endregion

    #region Public Fields

    public float checkRadius = 0.5f;
    public float speed = 5.0f;
    public float jumpForce = 5.0f;

    public LayerMask whatIsGrounded;
    public Transform groundCheck;

    public Transform cornerMin = null;
    public Transform cornerMax = null;


    public AudioClip drowning;
    public AudioClip slain;
    public AudioClip coin;
    public AudioClip chest;

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _facingRight = true;
        _jump = false;
        _isGrounded = false;
        SavePlayerPos = transform.position;
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

        // Restrict player movement
        KeepWithinMinMaxRectangle();

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            audioSource.clip = drowning;
            audioSource.Play();

            StartCoroutine("Death", drowning.length);
        }
        if (collision.CompareTag("Enemy"))
        {
            audioSource.clip = slain;
            audioSource.Play();

            StartCoroutine("Death", slain.length);
        }
        if (collision.CompareTag("Coin"))
        {
            audioSource.clip = coin;
            audioSource.Play();
            Destroy(collision.gameObject);
            LevelManager.Instance.AddOneCoin();
        }
        if (collision.CompareTag("Chest"))
        {
            audioSource.clip = chest;
            audioSource.Play();

            collision.gameObject.GetComponent<Animator>().SetBool("Open", true);
            StartCoroutine("Win", 3f);
        }
    }

    IEnumerator Death(float time)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(time);

        transform.position = SavePlayerPos;
        GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;

        LevelsManager.Instance.RemoveOneLife();
        LevelManager.Instance.PlayerIsDead();

    }

    IEnumerator Win(float time)
    {
        yield return new WaitForSeconds(time);
        LevelManager.Instance.ChestFound();
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

    void OnDrawGizmos()
    {
        Vector3 top_right = Vector3.zero; Vector3 bottom_right = Vector3.zero;
        Vector3 bottom_left = Vector3.zero;
        Vector3 top_left = Vector3.zero;
        if (cornerMax && cornerMin)
        {
            top_right = cornerMax.position;
            bottom_left = cornerMin.position;
            bottom_right = top_right;
            bottom_right.y = bottom_left.y;
            top_left = top_right;
            top_left.x = bottom_left.x;
        }
        //Set the following gizmo colors to YELLOW
        Gizmos.color = Color.yellow;
        //Draw 4 lines making a rectangle
        Gizmos.DrawLine(top_right, bottom_right);
        Gizmos.DrawLine(bottom_right, bottom_left);
        Gizmos.DrawLine(bottom_left, top_left);
        Gizmos.DrawLine(top_left, top_right);
    }

    void KeepWithinMinMaxRectangle()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        float clampedX = Mathf.Clamp(x, cornerMin.position.x, cornerMax.position.x);
        float clampedY = Mathf.Clamp(y, cornerMin.position.y, cornerMax.position.y);
        transform.position = new Vector3(clampedX, clampedY, z);
    }

    #endregion
}