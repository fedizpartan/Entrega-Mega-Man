using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret2 : MonoBehaviour
{
    [SerializeField] GameObject Bala;
    [SerializeField] GameObject Disparador1;
    [SerializeField] float speed;
    Rigidbody2D mybody;
    BoxCollider2D myCollider;
    Animator myAni;



    float FireRate = 0.5f;
    float nextFire = 0f;

    // Start is called before the first frame update
    void Start()
    {

        mybody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        myAni = GetComponent<Animator>();
        //Vector3 bulletforce = Turret.rotation * speed;
 

    }

    // Update is called once per frame
    void Update()
    {

        Shoot();
        DetectionL();
        DetectionR();
        DetectionUp();
        Death();


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            SceneManager.LoadScene("GameOver");
        }
    }

    public bool DetectionL()
    {
        RaycastHit2D detectortorre = Physics2D.Raycast(transform.position, Vector2.left, 3f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.left * 10f, Color.yellow);
        return detectortorre.collider != null;
    }

    bool DetectionR()
    {
        RaycastHit2D detectortorre = Physics2D.Raycast(transform.position, Vector2.right, 3f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.right * 10f, Color.yellow);
        return detectortorre.collider != null;
    }

    bool DetectionUp()
    {
        RaycastHit2D detectortorre = Physics2D.Raycast(transform.position, Vector2.up, 3f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.up * 10f, Color.yellow);
        return detectortorre.collider != null;
    }


    void Shoot()
    {

        if( DetectionL() && Time.time > nextFire)
        {

            //myAni = GetComponent<Animator>();
            //myAni.SetBool("Shoot", true);

            nextFire = Time.time + FireRate;


            GameObject bullet = Instantiate(Bala, transform.position, transform.rotation);
                bullet.GetComponent<ControladorBala>().xSpeed = 0.1f;
                bullet.GetComponent<ControladorBala>().ySpeed = 0.1f;

                bool dir = transform.localScale.x == 1;

                (bullet.GetComponent<BulletTurret2>()).Shoot(dir, speed);

        }

        if(DetectionUp() && Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;

            GameObject bullet = Instantiate(Bala, transform.position, transform.rotation);
            bullet.GetComponent<ControladorBala>().xSpeed = 0.1f;
            bullet.GetComponent<ControladorBala>().ySpeed = 0.1f;

          

            bool dir = transform.localScale.x == 1;

            (bullet.GetComponent<BulletTurret2>()).Shoot(dir, speed);
        }

        if(DetectionR() && Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;

            GameObject bullet = Instantiate(Bala, transform.position, transform.rotation);
            bullet.GetComponent<ControladorBala>().xSpeed = 0.1f;
            bullet.GetComponent<ControladorBala>().ySpeed = 0.1f;

            bool dir = transform.localScale.x == 1;

            (bullet.GetComponent<BulletTurret2>()).Shoot(dir, speed);

            
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Bullet")))
        {
            myAni = GetComponent<Animator>();
            myAni.SetBool("Death", true);
 
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

}
