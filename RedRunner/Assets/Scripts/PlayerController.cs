using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields

    private Animator animator;
    private Rigidbody2D rigidbody;
    private bool facingRight;
    private Transform groundCheck;
    private bool jump;
    private bool grounded;

    #endregion

    #region Public Fields

    public float moveForce;
    public float maxSpeed;
    public float jumpForce;

    #endregion

    #region Monobehaviour Callbacks

    private void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        facingRight = true;
        jump = false;
        grounded = false;
    }

    void FixedUpdate()
    {
        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        animator.SetFloat("Speed", Mathf.Abs(h));

        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * rigidbody.velocity.x < maxSpeed)
            // ... add a force to the player.
            rigidbody.AddForce(Vector2.right * h * moveForce);

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
            // ... set the player's velocity to the maxSpeed in the x axis.
            rigidbody.velocity = new Vector2(Mathf.Sign(rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y);

        //// Restrict player movement
        //KeepWithinMinMaxRectangle();

        // If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();
        
        //If the player should jump...
        if (jump)
        {
            // Set the Jump animator trigger parameter.
            animator.SetTrigger("Jump");

            // Add a vertical force to the player.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }

    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        Debug.Log("Grounded ? " + grounded);

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
    }

    #endregion

    #region Private Methods

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    #endregion
}
