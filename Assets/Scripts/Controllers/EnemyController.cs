using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject chest;
    private GameObject player;
    private Vector2 distance;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    private float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (CheckPlayer(10)) Kill();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckPlayer(2)) moveVector = distance / distance.magnitude;
        else moveVector = Vector2.zero;
        rb.velocity = moveVector * moveSpeed;
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
