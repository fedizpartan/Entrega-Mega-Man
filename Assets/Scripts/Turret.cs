using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject Disparador;
    [SerializeField] GameObject Bala;
    [SerializeField] float speed;
    [SerializeField] GameManager gm;
    [SerializeField] int LifePoints;

    float FireRate = 0.5f;
    float nextFire;

    float NextFireIn;

    int HP;

    Animator TurretBody;
    BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {

        TurretBody = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();

        HP = LifePoints;

    }

    // Update is called once per frame
    void Update()
    {
        EnemyDetection();


        Disparar();
        
    }

    bool EnemyDetection()
    {
        //se hace la linea
        RaycastHit2D detector = Physics2D.Raycast(transform.position, Vector2.left, 7f, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, Vector2.left * 7f, Color.yellow);
        //!= diferente
        return detector.collider != null;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 7f);
     

    }

    void Disparar()
    {
     
        GameObject bullet = Instantiate(Bala, transform.position, transform.rotation);
       
        bool dir = transform.localScale.x == 1;

        (bullet.GetComponent<Bullet>()).Shoot(dir, speed * 2);

        NextFireIn = Time.time + FireRate;

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {

        TurretBody = GetComponent<Animator>();
        TurretBody.SetBool("Explosion" , true);

        if(TurretBody.GetBool("Explosion"))
        {
            TurretBody.SetBool("Destroyed", true);
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Bullet"))
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



    }

}
