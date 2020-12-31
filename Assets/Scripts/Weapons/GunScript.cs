using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private PlayerController Player;
    //private ParticleSystem Particles;
    public Camera mainCamera;
    private GameManagerUI GameUI;

    //public int ammo = 20;

    private float range = 40f;

    private float cooldown = 10f; //Time to wait in seconds to shoot another ray
    private float timeToWait = 0f; //Time before player can shoot again. Starts at 0 but constantly changes. 

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameUI = GameObject.Find("GameManagerUI").GetComponent<GameManagerUI>();
       // Particles = GetComponent<ParticleSystem>();
       // GameUI.BulletsAmmo.text = "Bullets: " + ammo;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        CooldownDisplay();
    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && Player.inventorySlotSelected == "Gun" && Time.time > timeToWait && !GameUI.isCraftingToggled)
        {
            //Particles.Play();
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
            {
                EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
                //Checks to see if the gameobject is an enemy has EnemyScript as a component
                if (enemy != null) enemy.TakeDamage(50);
            }
            timeToWait = Time.time + cooldown;
            //ammo--;
            //GameUI.BulletsAmmo.text = "Bullets: " + ammo;
        }
    }
    void CooldownDisplay()
    {
        //if (Player.inventorySlotSelected == "Gun") GameUI.BulletsAmmo.enabled = true;
        //else GameUI.BulletsAmmo.enabled = false;
    }
}

