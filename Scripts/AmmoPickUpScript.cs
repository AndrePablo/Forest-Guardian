using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUpScript : MonoBehaviour
{
    public WeaponScript weapons;
    private string ammoName;
    public int incAmmoCount;
    // Start is called before the first frame update
    void Start()
    {
        ammoName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Give the player ammo if they collide with the pickup
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player"){
            // Increment rifle ammo
            if (ammoName.Contains("rifleAmmo")){
                weapons.bulletCount[0] += incAmmoCount;
                Destroy(gameObject);
            }
            else if (ammoName.Contains("shotgunAmmo")){
                weapons.bulletCount[1] += incAmmoCount;
                Destroy(gameObject);
            }
        }
    }
}
