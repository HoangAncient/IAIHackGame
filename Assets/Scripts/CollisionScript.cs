using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public float speed = 8f;
    public static Animator animator;
    //Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = true;
    private float jumpingPower = 12f;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
            Debug.Log("Item");
        }
        else if (collision.gameObject.tag == "Barrier")
        {
            Debug.Log("Barrier");
            Time.timeScale = 0.0f;
            QuizUI.render = true;
            // Quiz appear pause the animation
            QuizManager.Instance.StartGame();
            Destroy(collision.gameObject);
        }
    }

    void Start() {
        animator = gameObject.GetComponent<Animator>();
        QuizManager.Instance.GetData();
        rb = GetComponent<Rigidbody2D>();
    }
   
    void Update() {    
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
        if( Input.GetButtonDown("Jump") && isGrounded())
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2 (rb.velocity.x, jumpingPower );
        }
        else
        {
            animator.SetTrigger("Run");
        }
    }
   
  
    /*void MovePlayer() {
        /*if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + (Vector3.left * speed) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + (Vector3.right * speed) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + (Vector3.up * speed) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + (Vector3.down * speed) * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
        if (Input.GetKeyDown(KeyCode.Space))        // continue the game
        {
            animator.SetTrigger("Attack");
        }
    }*/

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed,rb.velocity.y);    
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
