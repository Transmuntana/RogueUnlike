using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    [SerializeField] private int shoot;
    [SerializeField] private int cooldown;
    [SerializeField] private int blast;
    [SerializeField] private int fuse;
    [SerializeField] private int damage;
    [SerializeField] private int speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
