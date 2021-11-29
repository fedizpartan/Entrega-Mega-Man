using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] CircleCollider2D Detector;
    [SerializeField] GameObject Player;
   
    BoxCollider2D EnemyBody;
    Animator EnemyAni;
    Rigidbody2D mybody;

    void Start()
    {
        EnemyAni.SetBool("Fly A", true);
        EnemyBody = GetComponent<BoxCollider2D>();
        Detector = GetComponent<CircleCollider2D>();

        EnemyAni.SetBool("Death", false);
      
    }

    
    void Update()
    {



        Move();


       /*Collider2D chocando = Physics2D.OverlapCircle(transform.position, 5, LayerMask.GetMask("Player"));

        if(chocando != null)
        {
            Debug.Log("Siguiendo player");
        }
        else
            Debug.Log("Dejar player");
        

        if (Vector2.Distance(transform.position, Player.transform.position) < 10)
        {
            Debug.Log("Siguiendo al personaje");
        }
        else
            Debug.Log("Dejar player");

        if(Detector.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("Siguiendo player");
        }
        else
        {
            Debug.Log("Dejar player");
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Disparo"))
        {

            EnemyAni = GetComponent<Animator>();
            EnemyAni.SetBool("Death",true );
            
        }
    }

   



    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            SceneManager.LoadScene("GameOver");
        }
    }
    void Move()
    {

        float direccion = Input.GetAxis("Horizontal");

        if (direccion < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
            transform.localScale = new Vector2(1, 1);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, Player.transform.position);
       // Gizmos.DrawWireSphere(transform.position, 5);
    }

    public void bigoof()
    {
        Destroy(gameObject);
    }
   
 



    

    

}


