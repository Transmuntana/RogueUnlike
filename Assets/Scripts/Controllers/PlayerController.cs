using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (GameManager._instance.inventory[GameManager._instance.item - 1] > 0) && (GameManager._instance.item == 3) && (GameManager._instance.playerHP < GameManager._instance.playerMaxHP))
        {
            GameManager._instance.inventory[GameManager._instance.item - 1] -= 1;
            GameManager._instance.playerHP += 10;
            if (GameManager._instance.playerHP > GameManager._instance.playerMaxHP) GameManager._instance.playerHP = GameManager._instance.playerMaxHP;
        }
        if (Input.GetKey(KeyCode.Keypad1))
        {
            GameManager._instance.item = 1;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            GameManager._instance.item = 2;
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            GameManager._instance.item = 3;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            GameManager._instance.item = 4;
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            GameManager._instance.item = 5;
        }
    }
}
