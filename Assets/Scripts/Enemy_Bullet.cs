using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{

    Animator myAnimator;
    Rigidbody2D Enemy_Body;
    [SerializeField] AudioClip sfx_Bullet;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {

        Enemy_Body = GetComponent<Rigidbody2D>();
        Enemy_Body.velocity = new Vector2(8, 0);
        myAnimator.SetTrigger("Rotation");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(10 * Time.deltaTime, 0, 0));
    }

    public void Shoot (bool direccion, float speed)
    {
        Enemy_Body = GetComponent<Rigidbody2D>();
        if (direccion)
            Enemy_Body.velocity = new Vector2(speed, 0);
        else
            Enemy_Body.velocity = new Vector2(-speed, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
