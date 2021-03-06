﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private PlayerController Player;
    //private ParticleSystem Particles;
    public Camera mainCamera;
    private GameManagerUI GameUI;

    private int damage = 10;

    private float range = 40f;

    private float cooldown = 10f; //Time to wait in seconds to shoot another ray
    private float timeToWait = 0f; //Time before player can shoot again. Starts at 0 but constantly changes. 
    private float cooldownTimerUI = 0f;

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
            int numOfHits = 0; //Can go through a max of 3 targets
            RaycastHit[] hits;
            hits = Physics.RaycastAll(mainCamera.transform.position, mainCamera.transform.forward, range);
            for (int i = 0; i < hits.Length && numOfHits < 3; i++)
            {
                RaycastHit hit = hits[i];
                EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
                if (enemy) //Checks if enemy is true or NULL (false)
                {
                    enemy.TakeDamage(damage); 
                    numOfHits++;
                }
            }
            timeToWait = Time.time + cooldown;
            cooldownTimerUI = 10f;
        }
    }
    void CooldownDisplay()
    {
        if (Player.inventorySlotSelected == "Gun") GameUI.GunCooldown.enabled = true;
        else GameUI.GunCooldown.enabled = false;

        if (cooldownTimerUI > 0)
        {
            cooldownTimerUI -= Time.deltaTime;
            GameUI.GunCooldown.text = "Cooldown: " + Mathf.Ceil(cooldownTimerUI);
        }
        else GameUI.GunCooldown.text = "Cooldown: Ready";
    }
}

