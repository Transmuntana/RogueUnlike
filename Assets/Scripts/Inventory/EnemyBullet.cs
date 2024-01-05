using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int pFuse;
    // Start is called before the first frame update
    void Start()
    {
        pFuse = 60;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        pFuse--;
        if (pFuse <= 0) Kill();
    }
    private void OnCollisionEnter2D(Collision2D trg)
    {

        if (trg.gameObject.tag == "Enemy") return;
        if (trg.gameObject.tag == "Player")
        {
            GameManager._instance.playerHP -= 5;
        }
        Kill();
    }
    private void Kill()
    {
        Destroy(this.gameObject);
    }
}
