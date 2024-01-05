using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public int shoot;
    public int cooldown;
    public int blast;
    public int fuse;
    public int damage;
    public float speed;
    public Sprite bulletSprite;
    public int weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
