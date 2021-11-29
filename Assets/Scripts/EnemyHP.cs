using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{

    public float Hitpoints;
    public float MaxHP = 5;
    // Start is called before the first frame update
    void Start()
    {
        Hitpoints = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit (float dmg)
    {
        Hitpoints -= dmg;

        if(Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
