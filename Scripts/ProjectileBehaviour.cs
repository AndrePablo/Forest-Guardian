using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float Speed = 4.5f;
    public float damage;

    // Update is called once per frame
    void Update()
    {
     transform.position += transform.up * Time.deltaTime * Speed;   
    }

    private void OnCollisionEnter2D(Collision2D collision){
        GameObject collisionGameObject = collision.gameObject;
        string selfName = gameObject.name;

        if(collisionGameObject.GetComponent<HealthScript>() != null){
            collisionGameObject.GetComponent<HealthScript>().TakeDamage(damage);
        }

        // Can't just destroy itself if the player shoots too fast
        if(collisionGameObject.name != selfName ){
            Destroy(gameObject);
        }
    }
}
