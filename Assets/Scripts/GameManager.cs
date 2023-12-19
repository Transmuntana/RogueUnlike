using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public int levelCounter;
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
    }
    #endregion
    public int CurrentLevel { get; set; }
    public int CurrentHealth { get; set; }
}
