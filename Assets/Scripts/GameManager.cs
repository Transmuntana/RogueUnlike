using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public int levelCounter;
    public int playerHP;
    public int playerMaxHP;
    public int[] inventory = new int[5];//silver spine, gravelerrock,oran berry, revive seed, unused
    public TMP_Text[] inventoryNums = new TMP_Text[5];
    #region Sigleton
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }
            return _instance;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);
        else
            _instance = this;
        DontDestroyOnLoad(this);
        levelCounter = 1;
        playerHP = 25;
        playerMaxHP = 25;
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            inventoryNums[i].text = inventory[i].ToString();
        }
    }
    #endregion
    public int CurrentHealth { get; set; }
}
