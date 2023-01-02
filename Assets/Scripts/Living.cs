using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Living : MonoBehaviour // WIP
{
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private int lives = 3;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // in comparison to collectables, traps are physical, not just triggers

        if(collision.gameObject.CompareTag("Trap"))
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        anim.SetTrigger("Hit");
        lives--;

        if(lives <= 0)
        {
            rb.bodyType = RigidbodyType2D.Static;                       // disable further movement
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // reload current level
    }
}
