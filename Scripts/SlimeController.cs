using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class SlimeController : MonoBehaviour
{
    private Animator animator;

    public Transform[] waypoints;
    private int currentWaypoint = 0;

    private Transform player;
    public bool playerSpotted = false;

    private float reload = 0f;

    public AIPath aiPath;
    public AIDestinationSetter destinationSetter;

    private bool facingRight = true; 

    private float waitTime = 1.0f;

    public Transform firePoint;
    public GameObject slimeBall;

    void Start()
    {
        //set some variables
        player = GameObject.Find("Player").transform;
        animator = gameObject.GetComponent<Animator>();

        //set first waypoint
        destinationSetter.target = waypoints[currentWaypoint];
    }
    
    void Update()
    {
        //Debug.Log(reload);
        if (playerSpotted)
        {
            if (reload <= 0)
            {
                //play attack animation
                animator.SetTrigger("attack");
                Shoot();
                reload = 2.0f;
            }
        }
        else
        {
            Patrol();
        }

        //reload next shot
        if (reload >= 0)
        {
            reload -= Time.deltaTime;
        }

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

    private void Patrol()
    {
        //print(aiPath.reachedEndOfPath);
        //walk until reached goal, then choose next point to move to
        if (aiPath.reachedEndOfPath)
        {
            //pause for a second
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            //set next destination
            else if (waitTime <= 0)
            {
                waitTime = 1.0f;
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                destinationSetter.target = waypoints[currentWaypoint];
            }
        }
    }

    private void Shoot()
    {
        //shoot a slime ball
        Instantiate(slimeBall, firePoint.position, firePoint.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //spotting player
        if (other.transform == player)
        {
            playerSpotted = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //player leaves sight
        if (other.transform == player)
        {
            playerSpotted = false;
        }
    }
}
