using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public float speed = 5;
    public Animator animator;
    void Start() {
        animator = gameObject.GetComponent<Animator>();
        QuizManager.Instance.GetData();
    }
    void Update() {
        
        MovePlayer();
    }

    private void  OnCollisionEnter2D (Collision2D collision)
    {
        Debug.Log("Hit");

        Time.timeScale = 0.0f;

        QuizUI.render = true;
         // Quiz appear pause the animation
        QuizManager.Instance.StartGame();

    }
  
    void MovePlayer() {
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
        }*/
        if (!QuizUI.render)        // continue the game
        {
            
            Time.timeScale = 1.0f;
            animator.SetTrigger("Attack");
        }

    }
}
