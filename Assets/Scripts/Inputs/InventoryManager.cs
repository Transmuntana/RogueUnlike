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
        GameManager._instance.item = 1;

        for (int i = 1; i < 6; i++)
        {
            string text = "ItemDis" + i.ToString();
            GameManager._instance.inventoryNums[i - 1] = GameObject.Find(text).GetComponent<TMP_Text>();
        }
        inventory.SetActive(false);
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    
}
