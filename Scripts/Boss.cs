using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Transform player;

    public float speed = 3f;

    private bool facingRight = true;

    private bool casting = false;
    private bool summoning = false;
    
    private float countdown = 7.0f;
    private float random;

    private float maxFireballCD = 2.0f;
    private int maxFireballs = 3;
    private float fireballCD = 2.0f;
    private float fireballs = 0;
    public Transform firePoint;
    public GameObject fireball;

    private float summonTime = 5.0f;
    private float maxSummonCD = 1.0f;
    private float summonCD = 1.0f;
    public GameObject icicle;
    private Vector2 position;
    public Transform icePoint;

    private bool playerInRange = false;

    public Slider healthbar;
    private HealthScript healthScript;

    void Start()
    {
        //set some variables
        player = GameObject.Find("Player").transform;
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        healthScript = gameObject.GetComponent<HealthScript>();
    }

    
    void Update()
    {
        //update healthbar
        healthbar.value = healthScript.hp;

        //increase stats when at/below half health
        if (healthScript.hp <= healthScript.startHealth/2)
        {
            maxFireballCD = 1.0f;
            maxFireballs = 5;
            maxSummonCD = 0.5f;
            speed = 4f;
            animator.SetFloat("castSpeed", 2);
        }

        //check death
        if (healthScript.hp > 0)
        {
            //chase player by default
            if (!casting && !summoning)
            {
                Chase();

                //choose next attack
                if (countdown <= 0)
                {
                    random = Random.Range(-1f, 1f);
                    //print("*********** INSIDE ***********" + random);
                    if (random <= 0)
                    {
                        casting = true;
                    }
                    else if (random > 0)
                    {
                        summoning = true;
                    }
                }
            }
            if (casting)
            {
                Fireball();
                fireballCD -= Time.deltaTime;
            }
            if (summoning)
            {
                Summon();
                summonTime -= Time.deltaTime;
                summonCD -= Time.deltaTime;
            }

            //count down to mix up attacks
            countdown -= Time.deltaTime;
        }
        if (healthScript.hp <= 0)
        {
            //stop moving
            Vector2 newPos = (rb.position);
            rb.MovePosition(newPos);
            animator.SetBool("Dead", true);
        }

        //flip sprite to face direction it is moving in
        //face left
        if (transform.position.x > player.position.x)
        {
            if (facingRight)
            {
                //transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
        }
        //face right
        else if (transform.position.x < player.position.x)
        {
            if(!facingRight)
            {
                //transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }
    }

    void Chase()
    {
        //run towards player, keep y value the same
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);

        //if close enough to player, do melee attack
        if (Vector2.Distance(player.position, rb.position) <= 1.75)
        {
            //stop moving and hit
            newPos = (rb.position);
            rb.MovePosition(newPos);
            animator.SetTrigger("Melee");
        }
    }

    public void DamageCheck()
    {
        if (playerInRange)
        {
            player.GetComponent<Character2DController>().health -= 1;
        }
    }

    void Fireball()
    {
        animator.SetBool("Casting", true);
        
        //shoots 3 fireballs, following cooldown
        if (fireballs < maxFireballs && fireballCD <= 0)
        {
            //shoot a fireball
            Instantiate(fireball, firePoint.position, firePoint.rotation);
            fireballs++;
            //reset CD
            fireballCD = maxFireballCD;
        }

        //end casting
        if (fireballs >= maxFireballs)
        {
            fireballs = 0;
            casting = false;
            summoning = false;
            animator.SetBool("Casting", false);
            countdown = 7.0f;
        }
    }

    void Summon()
    {
        animator.SetBool("Summoning", true);

        //summon for set time
        if (summonTime >= 0)
        {
            if (summonCD <= 0)
            {
                //summon icicle
                Instantiate(icicle, icePoint.position, icePoint.rotation);
                //reset CD
                summonCD = maxSummonCD;
            }
        }
        //end summoning
        else
        {
            summonTime = 5.0f;
            summoning = false;
            casting = false;
            animator.SetBool("Summoning", false);
            countdown = 7.0f;
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.name == "Player")
        {
            playerInRange = false;
        }
    }
}
