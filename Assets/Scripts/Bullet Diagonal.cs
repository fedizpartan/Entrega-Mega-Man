using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDiagonal : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
       
    }


    void Shoot(GameObject obj)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject)
    }
}
