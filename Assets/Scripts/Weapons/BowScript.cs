using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowScript : MonoBehaviour
{
    public GameObject Arrow; //Arrow prefab
    public PlayerController Player;
    public Camera mainCamera;

    private int arrowForce = 15;
    private float timeStart; 

    private int fireRate = 1; //Number of arrows in a second
    private float timeToWait = 0f; //Time before player can shhot again. Starts at 0 but constantly changes. 

    private int arrowDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        if (Time.time > timeToWait)
        {
            if (Input.GetButtonDown("Fire1") && Player.inventorySlotSelected == "Bow")
            {
                timeStart = Time.time;
            }
            if (Input.GetButtonUp("Fire1") && Player.inventorySlotSelected == "Bow")
            {
                float timeDown = Time.time - timeStart; //Time spent holding down the fire1 button
                float forceMultiplier = timeDown * 2;
                if (forceMultiplier >= 4) forceMultiplier = 4; //Max limit for the force multiplier
                if (forceMultiplier <= 1) forceMultiplier = 1; //Min limit for the force multiplier
                arrowDamage = (int)forceMultiplier;
                Debug.Log(forceMultiplier);
                Vector3 spawnPos = transform.position + transform.forward + transform.up * 0.5f;
                GameObject arrow = Instantiate(Arrow, spawnPos, mainCamera.transform.rotation);
                arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * arrowForce * forceMultiplier, ForceMode.Impulse);
                arrow.GetComponent<ArrowScript>().damage = arrowDamage;
                timeToWait = Time.time + 1/fireRate;
            }
        }
    }
}
