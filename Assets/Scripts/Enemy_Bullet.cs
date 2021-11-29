using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Bullet : MonoBehaviour
{

    Animator myAnimator;
    Rigidbody2D Enemy_Body;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {

        Enemy_Body = GetComponent<Rigidbody2D>();
        Enemy_Body.velocity = new Vector2(8, 0);


        Enemy_Body.velocity = transform.right * speed;

     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            SceneManager.LoadScene("GameOver");
        }
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
