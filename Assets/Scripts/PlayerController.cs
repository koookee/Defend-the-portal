using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManagerUI GameUI;
    public Camera mainCamera;
    private Rigidbody playerRB;
    public int numOfStones = 0;
    public int numOfWood = 0;
    public int numOfMetal = 0;
    private float playerSpeed = 3.0f;
    private float jumpForce = 4.0f;
    public int numOfJumps = 2;
    private int jumpsLeft;
    public int health = 10;
    private float weaponRange = 50f;
    public float axeRange = 70f;
    //Inventory arr
    public string[] inventory = new string[] { "Harvesting tool", "empty", "empty", "empty", "empty" };
    public string inventorySlotSelected = "";
    public int inventorySlotNum = 0; //inventorySlotNum is basically just the inventory array index
    private int previousInventorySlotNum = 0; //Index of the previous slot num
    public int availableSlotNum = 1; //The index of the available slot to be occupied by purchased weapons
    private KeyCode[] numKeypadArr = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5}; //Key codes of the first 5 numbers

    // Start is called before the first frame update
    void Start()
    {
        //GameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameUI = GameObject.Find("GameManagerUI").GetComponent<GameManagerUI>();
        inventorySlotSelected = inventory[inventorySlotNum];
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveFunc();
        JumpFunc();
        HotbarSelector();
        CheckPlayerHealth();
        ShootingFunc();
        HarvestingFunc();
        ToggleCraftingUI();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            health--;
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Checks to see if player is on the ground to reset doubleJump to 2
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = numOfJumps;
        }
    }
    private void ShootingFunc()
    {
        //Allows the player to damage enemies
        if (Input.GetButtonDown("Fire1") && inventorySlotSelected == "Gun")
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, weaponRange))
            {
                EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
                //Checks to see if the gameobject is an enemy/has EnemyScript as a component
                if (enemy != null) enemy.TakeDamage(1);
            }
        }
    }
    private void HarvestingFunc()
    {
        //Allows the player to damage enemies
        if (Input.GetButtonDown("Fire1") && inventorySlotSelected == "Harvesting tool")
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, axeRange))
            {
                WorldObjects worldObject = hit.transform.GetComponent<WorldObjects>();
                //Checks to see if the gameobject is an enemy/has EnemyScript as a component
                if (worldObject != null)
                {
                    switch (worldObject.type)
                    {
                        case ("Stone"):
                            worldObject.TakeDamage();
                            numOfStones++;
                            Debug.Log("Stones: " + numOfStones);
                            break;
                        case ("Wood"):
                            worldObject.TakeDamage();
                            numOfWood++;
                            Debug.Log("Wood: " + numOfWood);
                            break;
                        case ("Metal"):
                            worldObject.TakeDamage();
                            numOfMetal++;
                            Debug.Log("Metal: " + numOfMetal);
                            break;
                        default:
                            Debug.Log("Can't find object type");
                            break;
                    }

                }

            }
        }
    }
    private void JumpFunc()
    {
        //Takes the input to make the player jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsLeft -= 1;
        }
    }


    private void MoveFunc()
    {
        if (!GameUI.isCraftingToggled) //Player can move only if the crafting UI isn't visible 
        {
            //Makes the player move left and right
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 playerMovement = new Vector3(horizontal, 0, vertical).normalized * playerSpeed * Time.deltaTime;
            ////normalized prevents player at moving at twice the speed diagonally 
            transform.Translate(playerMovement, Space.Self);
            //playerRB.AddForce(playerMovement * 5,ForceMode.VelocityChange);
            //Vector3 velocityVector = playerMovement.normalized * 3;
            //playerRB.velocity = (velocityVector);
            //Debug.Log(playerRB.velocity);
        }
    }
    private void CheckPlayerHealth()
    {
        if (health <= 0)
        {
            Debug.Log("Player has died!");
        }
    }

    private void HotbarSelector() //Calls both methods that navigate the hotbar
    {
        HotbarScrollWheel();
        HotbarNumPad();
    }
    private void HotbarScrollWheel() //Moves between slots using scroll wheel
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && inventorySlotNum < inventory.Length - 1)
        {
            previousInventorySlotNum = inventorySlotNum;
            inventorySlotNum++;
            inventorySlotSelected = inventory[inventorySlotNum];

            //GameUI part
            GameUI.selectedUI[inventorySlotNum].SetActive(true); //Moves the slot border to the new selected slot
            GameUI.selectedUI[previousInventorySlotNum].SetActive(false); //Turns off the border of the previously selected slot
            previousInventorySlotNum = inventorySlotNum;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && inventorySlotNum > 0)
        {
            previousInventorySlotNum = inventorySlotNum;
            inventorySlotNum--;
            inventorySlotSelected = inventory[inventorySlotNum];

            //GameUI part
            GameUI.selectedUI[inventorySlotNum].SetActive(true); //Moves the slot border to the new selected slot
            GameUI.selectedUI[previousInventorySlotNum].SetActive(false); //Turns off the border of the previously selected slot
            previousInventorySlotNum = inventorySlotNum;
        }
    }
    private void HotbarNumPad() //Moves between slots using the number pad
    {
        for (int i = 0; i < numKeypadArr.Length; i++)
        {
            if (Input.GetKeyDown(numKeypadArr[i]))
            {
                inventorySlotNum = i;
                inventorySlotSelected = inventory[inventorySlotNum];

                //GameUI part
                GameUI.selectedUI[inventorySlotNum].SetActive(true); //Moves the slot border to the new selected slot
                if(inventorySlotNum != previousInventorySlotNum)GameUI.selectedUI[previousInventorySlotNum].SetActive(false); //Turns off the border of the previously selected slot
                previousInventorySlotNum = i;
            }
        }

    }
    private void ToggleCraftingUI()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameUI.ToggleCraftingUI();
        }
    }

}
