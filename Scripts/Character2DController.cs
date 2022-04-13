using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character2DController : MonoBehaviour
{
    private bool disabled = false;
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public GameObject[] bullets;

    public Transform[] LaunchOffset;

    private Rigidbody2D _rigidbody;

    public WeaponScript weapons;

    public AudioClip[] gunSFX;

    public float health;

    public float dashSpeed = 30;
    private float dashTime;
    public float startDashTime = 0.1f;
    private int direction;

    private float shotCD = 0f;
    private bool hasFired = false;

    private float dashCD = 0f;
    private bool facingRight = true;

    public Slider healthBar;

    private float teleportDistance = 2;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        //update health bar
        healthBar.value = health;

        

        float horizontalInput = Input.GetAxis("Horizontal");

        if (!disabled)
        {
            UpdateMovement();
        }

        //handle death
        if (health <= 0)
        {
            Die();
        }

        //decrement shot countdown
        shotCD -= Time.deltaTime;
        //dont allow player to spam click to shoot
        if (hasFired)
        {
            if (shotCD <= 0)
            {
                hasFired = false;
            }
        }

        if (Input.GetButtonDown("Fire1") && !hasFired)
        {
            shotCD = 1.0f;
            hasFired = true;

            // Update the ammo count for the rifle and shotgun
            if (weapons.currentWeaponIndex != 0){
                weapons.bulletCount[weapons.currentWeaponIndex-1] -= 1;
            }
            
            Quaternion projectileEuler;
            if (transform.rotation.eulerAngles.y > 0)
            {
                projectileEuler = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                projectileEuler = Quaternion.Euler(0, 0, 90);
            }

            // Load and play the correct bullet sound
            AudioSource gunShot = GetComponent<AudioSource>();
            if (weapons.currentWeaponIndex == 0){
                gunShot.clip = gunSFX[0];
                gunShot.Play();
            }
            else if (weapons.currentWeaponIndex == 1){
                gunShot.clip = gunSFX[1];
                gunShot.Play();
            }
            else if (weapons.currentWeaponIndex == 2){
                gunShot.clip = gunSFX[2];
                gunShot.Play();
            }
            Instantiate(bullets[weapons.currentWeaponIndex], LaunchOffset[weapons.currentWeaponIndex].position, projectileEuler);
        }

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.Q) && horizontalInput < -0.01f)
            {
                direction = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && horizontalInput > 0.01f)
            {
                direction = 2;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                _rigidbody.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    _rigidbody.velocity = Vector2.left * dashSpeed;
                }
                else if (direction == 2)
                {
                    _rigidbody.velocity = Vector2.right * dashSpeed;
                }
            }
        }
    }


    private void UpdateMovement()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }

    private void Die(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void dealDamage(int damage){
        health -= damage;
        if (health <= 0){
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("GreenSlime"))
        {
            dealDamage(1);
        }
        if (collision.gameObject.name.Contains("GoldSlime"))
        {
            dealDamage(3);
        }
        if (collision.gameObject.name.Contains("Bat"))
        {
            dealDamage(1);
        }
    }




}
