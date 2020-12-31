using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldObjects : MonoBehaviour
{
    private PlayerController Player;
    private GameManagerUI GameUI;
    public string objectName;
    public string type; //Metal, wood, rocks
    public int health;
    private int rareResourceChanceWood = 2; // 2% chance of dropping a rare resource after destorying wood type material
    private int rareResourceChanceRocks = 4; // 4% chance of dropping a rare resource after destorying rocks type material
    private int rareResourceChanceMetal = 10; // 10% chance of dropping a rare resource after destorying metal type material
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameUI = GameObject.Find("GameManagerUI").GetComponent<GameManagerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHealth();
    }
    public void TakeDamage()
    {
        health--;
    }
    private void CheckForHealth()
    {
        if (health <= 0)
        {
            UraniumDropRates();
            Destroy(gameObject);
        }
    }
    private void UraniumDropRates()
    {
        // Chance of getting a 0 would be 1 in 100/rareResourceChanceX which is the same as saying rareResourceChanceX %
        if (type == "Wood" && Random.Range(0, 100 / rareResourceChanceWood) == 0)
        {
            Player.numOfUranium++;
            GameUI.numOfUranium.text = "Uranium: " + Player.numOfUranium;
        }
        if (type == "Rocks" && Random.Range(0, 100 / rareResourceChanceRocks) == 0)
        {
            Player.numOfUranium++;
            GameUI.numOfUranium.text = "Uranium: " + Player.numOfUranium;
        }
        if (type == "Metal" && Random.Range(0, 100 / rareResourceChanceMetal) == 0)
        {
            Player.numOfUranium++;
            GameUI.numOfUranium.text = "Uranium: " + Player.numOfUranium;
        }
    }
}
