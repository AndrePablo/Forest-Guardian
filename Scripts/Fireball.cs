using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    private Animator animator;

    private bool collision = false;
    private float explosionTime = 0.5f;

    void Start()
    {
        //get variables
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        //move fireball
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        if (collision)
        {
            if (explosionTime <= 0)
            {
                Destroy(gameObject);
            }
            explosionTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.name != "Boss" && !(other.name.Contains("bullet")))
        {
            rb.velocity = new Vector2(0,0);
            collision = true;
            animator.SetTrigger("Collision");
        }
        if (other.name == "Player")
        {
            other.GetComponent<Character2DController>().health -= 2;
        }
    }
}
