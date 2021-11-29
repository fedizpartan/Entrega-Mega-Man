using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    Animator myAnimator;
    Rigidbody2D myBody;
    [SerializeField] AudioClip sfx_Bullet;
    [SerializeField] int DamagePoints;
    public float speed = 20f;

    //CapsuleCollider2D myCollider;


    void Start()
    {
        /*myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myBody = GetComponent<Rigidbody2D>();*/

        myBody = GetComponent<Rigidbody2D>();
        myBody.velocity = new Vector2(10, 0);
        

     
    }

    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;

        //transform.Translate(new Vector3(10 * Time.deltaTime, 0, 0));
        //bool dir = transform.localScale.x == 1;

        //transform.Translate(Vector2.right * speed * Time.deltaTime);


    }



    public void Shoot(bool direction, float speed)
    {

        myBody = GetComponent<Rigidbody2D>();
        if(direction )
            myBody.velocity = new Vector2(speed, 0);
        else
            myBody.velocity = new Vector2(-speed, 0);

        AudioSource.PlayClipAtPoint(sfx_Bullet, Camera.main.transform.position);

    }


    public void OnTriggerExit2D(Collider2D collision)
    {

        myAnimator = GetComponent<Animator>();
        myBody.velocity = Vector2.zero;
        myAnimator.SetTrigger("Explosion");
        Destroy(gameObject, 0.03f);
    }

    public int Damage()
    {

        return DamagePoints;

    }

  

    private void OnCollisionEnter2D(Collision2D collision)
    {


       myAnimator = GetComponent<Animator>();
        myAnimator.SetTrigger("Explosion");
        GameObject objeto = collision.gameObject;
        string etiqueta = objeto.tag;

        Destroy(this.gameObject, 0.03f);
    }



    void Die()
    {
        //Destroy(gameObject);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        var enemy =collision.collider.GetComponent<EnemyHP>();
        if(enemy)
        {
            enemy.TakeHit(1);
        }
    }




    public void OnTriggerEnter2D(Collider2D col)
    {
        
         if (col.gameObject.tag=="Plataformas")
         {
             myAnimator.SetBool("Explosion",true);
             
             Destroy(gameObject,0.03f);
         }
    }
    
}
