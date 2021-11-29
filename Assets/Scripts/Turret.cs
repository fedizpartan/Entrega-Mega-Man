using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Turret : MonoBehaviour
{
    [SerializeField] GameObject Disparador;
    [SerializeField] GameObject Bala;
    [SerializeField] float speed;
    [SerializeField] GameManager gm;
    [SerializeField] int LifePoints;


    float FireRate = 0.5f;
    float nextFire = 0f;

    int HP;

    Animator TurretBody;
    BoxCollider2D myCollider;
    Rigidbody2D mybody;

    // Start is called before the first frame update
    void Start()
    {

        TurretBody = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();

        LifePoints = HP;
        TurretBody.SetBool("Destroyed", false);
        TurretBody.SetBool("NormalState", true);



    }

    // Update is called once per frame
    void Update()
    {
        EnemyDetectionLeft();
        EnemyDetectionRight();
        Disparar(); 


    }

    public bool EnemyDetectionLeft()
    {
        //se hace la linea
        RaycastHit2D detector = Physics2D.Raycast(transform.position, Vector2.left, 10f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.left * 10f, Color.yellow);
        //!= diferente
        return detector.collider != null;

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 10f);

    }

    public bool EnemyDetectionRight()
    {

        RaycastHit2D detector2 = Physics2D.Raycast(transform.position, Vector2.right, 10f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.right * 10f, Color.yellow);
        return detector2.collider != null;
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 10f);

        if(TurretBody.GetBool("Destroyed"))
        {

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            SceneManager.LoadScene("GameOver");
        }
    }

    public void Disparar()
    {

        if(EnemyDetectionLeft() == true && Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;
            GameObject bullet = Instantiate(Bala, transform.position, transform.rotation);



            bool dir = transform.localScale.x == -1;

            (bullet.GetComponent<Bullet>()).Shoot(dir, speed * 2);



        }

        if (EnemyDetectionRight() == true && Time.time > nextFire)
        {

            nextFire = Time.time + FireRate;

            GameObject bullet = Instantiate(Bala, transform.position, transform.rotation);

            bool dir = transform.localScale.x == 1;

            (bullet.GetComponent<Bullet>()).Shoot(dir, speed * 2);

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GetComponent<EnemyHP>().TakeHit;

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Bullet")))
        {

            TurretBody = GetComponent<Animator>();
            TurretBody.SetBool("Explosion", true);

            if (TurretBody.GetBool("Explosion"))
            {
                TurretBody.SetBool("Destroyed", true);

                if (TurretBody.GetBool("Destroyed"))
                {
                    //Se deshabilita collider para que el jugador cruce este gameobject sin que el juego lo mande al gameOver scene
                    GetComponent<BoxCollider2D>().enabled = false;
                    


                }
            }

        }
        
    }





    /*private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Disparo"))
        {
            int puntos = collision.gameObject.GetComponent<Bullet>().Damage();
            LifePoints = LifePoints - puntos;

            if (LifePoints <= 0)
            {
                gm.ReduceEnemies();

                TurretBody = GetComponent<Animator>();
                TurretBody.SetBool("Explosion", true);

                if (TurretBody.GetBool("Explosion"))
                {
                    TurretBody.SetBool("Destroyed", true);
                }
            }
        }
    }*/


}
