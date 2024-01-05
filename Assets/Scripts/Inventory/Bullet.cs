using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponData weaponData;
    private int pFuse;
    // Start is called before the first frame update
    void Start()
    {
        pFuse = weaponData.fuse;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        pFuse--;
        if (pFuse <= 0) Kill();
    }
    private void OnCollisionEnter2D(Collision2D trg)
    {

        if (trg.gameObject.tag == "Player") return;
        if (trg.gameObject.tag == "Enemy")
        {

            EnemyController enemyScr = trg.gameObject.GetComponent<EnemyController>();
            enemyScr.Kill();
        }
        Kill();
    }
    private void Kill()
    {
        if (weaponData.blast > 0) { Explode(); }
        Destroy(this.gameObject);
    }
    private void Explode()
    {
        Collider[] trgs = Physics.OverlapSphere(transform.position, weaponData.blast);
        foreach (var trg in trgs)
        {
            if (trg.GetComponent<Collider>().tag == "Enemy")
            {
                EnemyController enemyScr = trg.gameObject.GetComponent<EnemyController>();
                enemyScr.Kill();
            }
        }
    }
}
