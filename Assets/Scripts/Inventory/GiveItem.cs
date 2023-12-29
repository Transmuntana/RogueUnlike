using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GiveItem : MonoBehaviour
{
    private string[] items = {"Silver spine", "Gravelerrock", "Oran Berry", "Revive Seed", "Gold"};
    private int reward;
    private int treasure;
    // Start is called before the first frame update
    void Start()
    {
        var rand = new System.Random();
        treasure = rand.Next(5);
        switch (treasure)
        {
            case 0:
                reward = rand.Next(6) + 5;
                break;
            case 1:
                reward = rand.Next(3) + 1;
                break;
            case 2:
                reward = 1;
                break;
            case 3:
                reward = 1;
                break;
            case 4:
                reward = (rand.Next(20)+1) * 50;
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager._instance.inventory[treasure] += reward;
            Debug.Log("You found " + reward + " " + items[treasure] + "!");
            Destroy(this.gameObject);
        }
    }
}
