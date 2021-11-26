using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject bala;
    [SerializeField] GameObject disparador;
    [SerializeField] AudioClip sfx_Salto;
    [SerializeField] AudioClip sfx_Aterrizaje;
    [SerializeField] AudioClip sfx_Dash;
    [SerializeField] AudioClip sfx_Die;
    [SerializeField] Text Reloj;


    float FireRate = 0.3f;
    float NextFireIn;

    float nextFire;
    float AniCoolDown = 0.4f;

    public bool shooting;
    private float shootT;
    public float time;
    public bool Dash;
    public float DashT;
    public float SDash;


    float DashTime = 0.5f;
    float DashST;
    bool DJ = false;
    bool isDashing;

    float dir = 1;
  

    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;

    private int SaltosAdicionales;
    public int ValorSaltos;

    float tiempo;
    float contador = 0;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        myAnimator.SetBool("Is Running", true);

        myBody = GetComponent <Rigidbody2D>();

        myCollider = GetComponent <BoxCollider2D>();

        SaltosAdicionales=ValorSaltos;

        StartCoroutine(TimerCorutina());

    }

    void Update()
    {
        Correr();
        Saltar();
        Caer();
        Disparar();
        DashSkill();
        //TimerNormal();
        
    }


    void TimerNormal()
    {
        //se suma el tiempo en segundos
        // tiene que haber un yield return
        tiempo += Time.deltaTime;
        if(tiempo >= 1f)
        {
            contador++;
            Reloj.text = contador.ToString();
            time = 0;
        }
    }


    IEnumerator TimerCorutina()
    {
        while(true)
        {
            //salir y esperar 1 segundo
            yield return new WaitForSeconds(1);
            contador++;
            Reloj.text = contador.ToString();
        }
    }


    bool OnFloor()
    {
        return myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //se necesitan 4 parametros para usar el raycast (donde, direccion, distancia y cual capa desea detectar
        RaycastHit2D colision_suelo = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, 
                                      myCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));


        //ayuda visual para el raycast
        //donde estamos y direccion
        Debug.DrawRay(myCollider.bounds.center, Vector2.down * (myCollider.bounds.extents.y + 0.2f), Color.red);

        return (colision_suelo.collider != null);
    }

    bool TouchingWalls()
    {
        
        RaycastHit2D colision_pared = Physics2D.Raycast(myCollider.bounds.center, new Vector2 (dir, 0),
                                      myCollider.bounds.extents.x + 0.5f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(myCollider.bounds.center, new Vector2(dir, 0) * (myCollider.bounds.extents.x + 0.2f), Color.yellow);

        return colision_pared != null;


        /*float movH;
        return myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if(movH > 0)
        {
            Physics2D.Raycast(myCollider.bounds.center,
            Vector2.left, myCollider.bounds.extents.x + 0.2f, LayerMask.GetMask("Ground"));

            Debug.DrawRay(myCollider.bounds.center, Vector2.left * (myCollider.bounds.extents.x + 0.2f), Color.yellow);
        }
        else
            Physics2D.Raycast(myCollider.bounds.center,
            Vector2.right, myCollider.bounds.extents.x + 0.2f, LayerMask.GetMask("Ground"));

            Debug.DrawRay(myCollider.bounds.center, Vector2.right * (myCollider.bounds.extents.x + 0.2f), Color.yellow);
        */



    }
    void Disparar()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {

            //se crea la bala y se guarda en el gameobject bullet
            GameObject bullet = Instantiate(bala, transform.position, transform.rotation);

            //la bala dispara hacia la izq y hacia la der
            bool dir = transform.localScale.x == 1;

            //llamo el script de Bullet, invocando la funcion "shoot" especificando sus 2 variables
            (bullet.GetComponent<Bullet>()).Shoot(dir, speed * 2);


            //al oprimir la tecla se cambia la capa de animacion
            myAnimator.SetLayerWeight(1, 1);


            nextFire = Time.time + AniCoolDown;
            NextFireIn = Time.time + FireRate;

        }
        else
        {
            if (Time.time > nextFire)
                myAnimator.SetLayerWeight(1, 0);
        }



        /*if (Input.GetKey(KeyCode.Z))
        {
            Instantiate(bala, disparador.transform.position, transform.rotation);
            nextFire = Time.time + FireRate;

            if (!shooting)
            {
                shooting = true;
            }
        }
        if (shooting)
        {
            
            shootT += 3 * Time.deltaTime;
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
         myAnimator.SetLayerWeight(1, 0);
        }
            
        if (shootT >=time)
        {
            shooting = false;
            shootT = 0; 
        }*/
    }
    void Caer()
    {
        if(myBody.velocity.y < -0.05f & myAnimator.GetBool("TakeOff"))
        {
            myAnimator.SetBool("Is Falling", true);
        }

    }
    void FinishJump()
    {
        myAnimator.SetBool("Is Falling", true);
        myAnimator.SetBool("TakeOff", false); 
    }
    void Saltar()
    {
      
        {
            if(OnFloor())
            {
                DJ = false;
                myAnimator.SetBool("Is Falling", false);
                myAnimator.SetBool("TakeOff", false);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    myAnimator.SetTrigger("Jump");
                    myAnimator.SetBool("TakeOff", true);
                    DJ = true;

                    AudioSource.PlayClipAtPoint(sfx_Salto, Camera.main.transform.position);
                }
            }

           
        }

        //aqui se puede hacer doble salto
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && DJ)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                {
                myBody.AddForce(new Vector2(0, jumpForce/2), ForceMode2D.Impulse);
                DJ = false;
            }
        }
        //////////////////////////////////////////////////////////////////////
    


        /*if (Input.GetKeyDown(KeyCode.Space) && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            myAnimator.SetTrigger("Jump");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                myAnimator.SetTrigger("Jump");
            }
        }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            SaltosAdicionales = ValorSaltos;
        }

        if (Input.GetKeyDown(KeyCode.Space) && SaltosAdicionales > 0)
        {
            myBody.velocity = Vector2.up * jumpForce;
            SaltosAdicionales--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && SaltosAdicionales == 0 && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {

            myBody.velocity = Vector2.up * jumpForce;
        }

        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("Is Falling", true);
        }*/

    }
    void DashSkill()
    {
        
            if(OnFloor())
        {
            //oprimir tecla
            if(Input.GetKeyDown(KeyCode.G))
            {
                DashST = Time.time;
                myAnimator.SetBool("Dash", true);
                isDashing = true;
                AudioSource.PlayClipAtPoint(sfx_Dash, Camera.main.transform.position);
            }

            //MANTENER OPRIMIDO
            if(Input.GetKey(KeyCode.G))
            {
                if(Time.time <= DashST + DashTime)
                {
                    myBody.velocity = new Vector2(speed * 2 * transform.localScale.x, myBody.velocity.y);
                }
                else
                    myAnimator.SetBool("Dash", false);
            }

            else
                myAnimator.SetBool("Dash", false);
        }
        
        
        
        /*if (Input.GetKey(KeyCode.X))
        {
            DashT += 1 * Time.deltaTime;

            if (DashT <0.35f)
            {
                myAnimator.SetBool("Dash", true);
                transform.Translate(Vector2.right * SDash * Time.fixedDeltaTime);
                transform.Translate(Vector2.left * SDash * Time.fixedDeltaTime);
            }
            else
            {
                Dash = false;
                myAnimator.SetBool("Dash", false);
            }
        }
        else
        {
            Dash = false;
            myAnimator.SetBool("Dash", false);
            DashT = 0;
        }*/
    }
    void Correr()
    {
        float movH = Input.GetAxis("Horizontal");
        //float movV = Input.GetAxis("Vertical");

        //se mueve con las flechas (forzado)
        //Vector2 movimiento = new Vector2(movH * speed, movV * speed) * Time.deltaTime;
        

        if (movH != 0 )
        {

            float direccion = Input.GetAxis("Horizontal");

            if (direccion != 0)
            {
                if(direccion < 0)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                    transform.localScale = new Vector2(1, 1);

                if (TouchingWalls() && direccion == dir)
                {
                    myAnimator.SetBool("Is Running", false);
                    myBody.velocity = new Vector2(0, myBody.velocity.y);
                }

                else
                    dir = transform.localScale.x;
                myAnimator.SetBool("Is Running", true);
                myBody.velocity = new Vector2(direccion * speed, myBody.velocity.y);


            }







            /*myAnimator.SetBool("Is Running", true);
            //se mueve con velocidad
            myBody.velocity = new Vector2(movH * speed, myBody.velocity.y);
            Vector2 movimiento = new Vector2(movH * speed, movV * speed) * Time.deltaTime;

            if (movH < 0 )
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
                transform.localScale = new Vector2(1, 1);

            if(TouchingWalls() && movH == dir)
            {
                myAnimator.SetBool("Running", false);
                myBody.velocity = new Vector2(0, myBody.velocity.y);

            }
            else
                dir = transform.localScale.x;*/
        }
        else
        {
            myBody.velocity = new Vector2(0 , myBody.velocity.y);
            myAnimator.SetBool("Is Running", false);

        }






    }

   
}
