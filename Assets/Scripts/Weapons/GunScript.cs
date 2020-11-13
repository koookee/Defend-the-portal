using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private PlayerController Player;
    private ParticleSystem Particles;
    public Camera mainCamera;
    private GameManagerUI GameUI;

    public int ammo = 20;

    private float range = 40f;

    private float fireRate = 2f; //Number of bulets in a second
    private float timeToWait = 0f; //Time before player can shoot again. Starts at 0 but constantly changes. 

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameUI = GameObject.Find("GameManagerUI").GetComponent<GameManagerUI>();
        Particles = GetComponent<ParticleSystem>();
        GameUI.BulletsAmmo.text = "Bullets: " + ammo;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        AmmoDisplay();
    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && Player.inventorySlotSelected == "Gun" && Time.time > timeToWait && !GameUI.isCraftingToggled && ammo > 0)
        {
            Particles.Play();
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
            {
                EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
                //Checks to see if the gameobject is an enemy has EnemyScript as a component
                if (enemy != null) enemy.TakeDamage(1);
            }
            timeToWait = Time.time + 1 / fireRate;
            ammo--;
            GameUI.BulletsAmmo.text = "Bullets: " + ammo;
        }
    }
    void AmmoDisplay()
    {
        if (Player.inventorySlotSelected == "Gun") GameUI.BulletsAmmo.enabled = true;
        else GameUI.BulletsAmmo.enabled = false;
    }
}

