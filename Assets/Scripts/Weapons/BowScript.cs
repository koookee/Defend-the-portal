using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowScript : MonoBehaviour
{
    public GameObject Arrow; //Arrow prefab
    private PlayerController Player;
    public Camera mainCamera;
    private GameManagerUI GameUI; 

    public int ammo = 3; //Starts with 3 arrows

    private int arrowForce = 15;
    private float timeStart; 

    private int fireRate = 1; //Number of arrows in a second
    private float timeToWait = 0f; //Time before player can shhot again. Starts at 0 but constantly changes. 

    private int arrowDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameUI = GameObject.Find("GameManagerUI").GetComponent<GameManagerUI>();
        GameUI.ArrowsAmmo.text = "Arrows: " + ammo;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        AmmoDisplay();
    }
    void Shoot()
    {
        if (ammo > 0 && !GameUI.isCraftingToggled)
        {
            if (Input.GetButtonDown("Fire1") && Player.inventorySlotSelected == "Bow")
            {
                timeStart = Time.time;
            }
            //If player changes hotbar slots while bow is charged, it doesn't spawn nor use up an arrow
            //If I placed Player.inventorySlotSelected == "Bow" outside the nested statement, the player
            //would still shoot the arrow after moving to a different hotbar slot
            if (Input.GetButtonUp("Fire1") && Player.inventorySlotSelected == "Bow" && Time.time > timeToWait) 
            {
                float timeDown = Time.time - timeStart; //Time spent holding down the fire1 button
                float forceMultiplier = timeDown * 2;
                if (forceMultiplier >= 4) forceMultiplier = 4; //Max limit for the force multiplier
                if (forceMultiplier <= 1) forceMultiplier = 1; //Min limit for the force multiplier
                arrowDamage = (int)forceMultiplier;
                Vector3 spawnPos = transform.position + transform.forward + transform.up * 0.5f + transform.right * 0.25f;
                GameObject arrow = Instantiate(Arrow, spawnPos, mainCamera.transform.rotation);
                arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * arrowForce * forceMultiplier, ForceMode.Impulse);
                arrow.GetComponent<ArrowScript>().damage = arrowDamage;
                timeToWait = Time.time + 1/fireRate;
                ammo--;
                GameUI.ArrowsAmmo.text = "Arrows: " + ammo; //Updates the ammo UI
            }
        }
    }
    void AmmoDisplay()
    {
        if (Player.inventorySlotSelected == "Bow") GameUI.ArrowsAmmo.enabled = true;
        else GameUI.ArrowsAmmo.enabled = false;
    }
}
