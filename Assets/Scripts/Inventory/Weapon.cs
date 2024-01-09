using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    public PlayerMovement pMove;
    public GameObject bullet;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = weaponData.cooldown;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counter > 0) counter--;
        if ((counter <= 0) && Input.GetKeyDown(KeyCode.Mouse0) && GameManager._instance.item==weaponData.weapon && GameManager._instance.inventory[weaponData.weapon-1]>0)
        {
            GameManager._instance.inventory[weaponData.weapon-1]--;
            counter = weaponData.cooldown;
            GameObject temp = Instantiate(bullet, transform.position + new Vector3(pMove.lastMove.x, pMove.lastMove.y,0)/3, Quaternion.identity) as GameObject;
            Rigidbody2D bVector = temp.GetComponent<Rigidbody2D>();
            Bullet bulletScr = temp.GetComponent<Bullet>();
            SpriteRenderer bulletSpr = temp.GetComponent<SpriteRenderer>();
            bVector.velocity = pMove.lastMove * weaponData.speed;
            bulletScr.weaponData = weaponData;
            bulletSpr.sprite = weaponData.bulletSprite;
        }

    }
}
