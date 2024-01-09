using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{

    public GameObject chest;
    public GameObject bullet;
    private GameObject player;
    private Vector2 distance;
    private int cooldown;
    private Rigidbody2D rb = null;
    private float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (CheckPlayer(10)) Kill();
        cooldown = 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cooldown--;
        if (CheckPlayer(6) && cooldown<=0) {
            cooldown = 15;
            GameObject temp = Instantiate(bullet, transform.position + new Vector3(distance.x/distance.magnitude, distance.y / distance.magnitude, 0), Quaternion.identity) as GameObject;
            Rigidbody2D bVector = temp.GetComponent<Rigidbody2D>();
            bVector.velocity = new Vector2(distance.x / distance.magnitude * 3, distance.y / distance.magnitude * 3);
        }
    }
    private bool CheckPlayer(float dis) {
        distance = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        if (distance.magnitude < dis) return true;
        else return false;
    }
    private void OnCollisionEnter2D(Collision2D trg)
    {

        if (trg.gameObject.tag == "Player")
        {
            GameManager._instance.playerHP-=5;
            Kill();
        }
    }
    public void Kill()
    {
        GameObject temp = Instantiate(chest, transform.position, Quaternion.identity) as GameObject;
        Destroy(this.gameObject);
    }
}
