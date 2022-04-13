using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class Bat : MonoBehaviour
{
    private Animator animator;

    private Transform player;

    public AIPath aiPath;
    public AIDestinationSetter destinationSetter;

    private bool facingRight = true;

    void Start()
    {
        //set some variables
        player = GameObject.Find("Player").transform;
        animator = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
        //flip sprite to face direction it is moving in
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            //moving to the right
            if (!facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            //moving to the left
            if (facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //spot player and chase
        if (other.transform == player)
        {
            animator.SetBool("PlayerSpotted", true);
            destinationSetter.target = player;
        }
    }
}
