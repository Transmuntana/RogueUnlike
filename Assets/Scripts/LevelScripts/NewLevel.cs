using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class coll : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager._instance.levelCounter++;
            Debug.Log("Loading level " + GameManager._instance.levelCounter+"!");
            SceneManager.LoadScene("RandomizedLevel");
        }
    }
}
