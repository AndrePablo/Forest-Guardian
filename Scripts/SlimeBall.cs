using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    void Start()
    {
        //move bullet
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Deal damage to the player
       if (collider.gameObject.name == "Player")
        {
            Debug.Log(collider.gameObject.name);
            collider.gameObject.GetComponent<Character2DController>().dealDamage(1);
        }
        print(collider.name);
       if (!collider.gameObject.name.Contains("Green") && !collider.gameObject.name.Contains("bullet") && !collider.gameObject.name.Contains("Bat"))
        {
            Destroy(gameObject);
        }
        
    }
}
