using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameManagerUI GameUI;
    private PlayerController Player;
    public int numEnemiesTargetingPlayer; // The number of enemies targeting the player
    public int maxNumEnemiesTargetingPlayer = 3; //Maximum number of enemies that should target the player
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameUI = GameObject.Find("GameManagerUI").GetComponent<GameManagerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyWeapon(int weaponType)
    {
        //Type 0 is standard gun
        if (weaponType == 0 && !GameUI.gunPurchased && Player.numOfMetal >= 20)
        {
            Player.numOfMetal -= 20;
            //GameManagerUIScript.gunPrice.SetActive(false);
            GameUI.gunCheckmark.SetActive(true);
            GameUI.gunPurchased = true;
            Player.inventory[Player.availableSlotNum] = "Gun";
            GameUI.slotsUI[Player.availableSlotNum].enabled = true; //enables the empty slot image
            GameUI.slotsUI[Player.availableSlotNum].sprite = GameUI.gunSprite; //sets the image to the gun sprite
            Player.availableSlotNum++; //Moves the index of the available slot to the one after it
            //Potential bug: Function doesn't check if availableSlotNum reached its max (4)
        }

        //Type 1 is bow
        if (weaponType == 1 && !GameUI.bowPurchased && Player.numOfWood >= 10)
        {
            Player.numOfWood -= 10;
            //GameManagerUIScript.gunPrice.SetActive(false);
            GameUI.bowCheckmark.SetActive(true);
            GameUI.bowPurchased = true;
            Player.inventory[Player.availableSlotNum] = "Bow";
            GameUI.slotsUI[Player.availableSlotNum].enabled = true; //enables the empty slot image
            GameUI.slotsUI[Player.availableSlotNum].sprite = GameUI.bowSprite; //sets the image to the bow sprite
            Player.availableSlotNum++; //Moves the index of the available slot to the one after it
            //Potential bug: Function doesn't check if availableSlotNum reached its max (4)
        }
    }
    /*
     * 
    public void BuyAmmo(int weaponType)
    {
        //Type 0 is rocket ammo
        if(weaponType == 0)
        {
            //Makes sure player has enough gems
            if(Player.gems >= 50)
            {
                Player.gems -= 50;
                Player.rocketAmmo++;
            }
        }
    }
     * private void CheckPlayerHealth()
    {
        //Checks if player's still alive
        if (Player.health <= 0)
        {
            deathCam.enabled = true;
            mainCam.enabled = false;
            Player.gameObject.SetActive(false);
            isGameOver = true;
            RestartGameUI();
        }
    }
     * private void CheckEnemyHealth()
    {
        //Takes an array of enemies present in the world
        GameObject[] enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
        //Loops through the array to check if one of the enemies has health == 0 (basically dead)
        foreach (GameObject enemy in enemyArr)
        {
            //I can't use enemy.health since I need the enemy's script component first
            //This is a quick method of adding the script then checking for the enemy's health
            EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            if (enemyScript.health <= 0)
            {
                switch (enemyScript.enemyType) 
                {
                    case "Regular":
                        Player.gems += 20;
                        Destroy(enemy.gameObject);
                        break;
                    case "Bull":
                        Player.gems += 50;
                        Destroy(enemy.gameObject);
                        break;
                    case "Archer":
                        Player.gems += 40;
                        Destroy(enemy.gameObject);
                        break;
                    default:
                        Debug.Log("Unknown enemy type; please specify type");
                        break;
                }
            }
        }
    }
     */
}
