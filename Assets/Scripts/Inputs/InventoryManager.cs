using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private int item;
    GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory");
        item = 1;

        for (int i = 1; i < 6; i++)
        {
            string text = "ItemDis" + i.ToString();
            GameManager._instance.inventoryNums[i - 1] = GameObject.Find(text).GetComponent<TMP_Text>();
        }
        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (item) {
                case 1:
                    GameManager._instance.playerHP = 0;
                    break;
                case 2:
                    break;
                case 3:
                    GameManager._instance.inventory[item-1] -= 1;
                    GameManager._instance.playerHP += 10;
                    if (GameManager._instance.playerHP > GameManager._instance.playerMaxHP) GameManager._instance.playerHP = GameManager._instance.playerMaxHP;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            inventory.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        { 
            inventory.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            item = 1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            item = 2;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            item = 3;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            item = 4;
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            item = 5;
        }
    }
}
