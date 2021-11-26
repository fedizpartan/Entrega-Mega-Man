using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] CircleCollider2D Detector;
    [SerializeField] GameObject Player;

    BoxCollider2D EnemyBody;
    Animator EnemyAni;

    void Start()
    {


        EnemyAni.SetBool("Fly A", true);
       


    }

    
    void Update()
    {

        EnemyAni.GetBool("Fly A");





       /* Collider2D chocando = Physics2D.OverlapCircle(transform.position, 5, LayerMask.GetMask("Player"));

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

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, Player.transform.position);
       // Gizmos.DrawWireSphere(transform.position, 5);
    }



    private void OnTriggerEnter2D(Collider2D bullet)
    {
        EnemyAni.SetBool("Death", true);

        Destroy(gameObject, 0.03f);
    }

}
