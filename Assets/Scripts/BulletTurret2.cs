using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret2 : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(8, 0);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1 * Time.deltaTime, -2 * Time.deltaTime, 0));
    }


    public void Shoot(bool direction, float speed)
    {

        rb = GetComponent<Rigidbody2D>();
        if (direction)
            rb.velocity = new Vector2(speed, 0);
        else
            rb.velocity = new Vector2(-speed, 0);
    }

 
}
