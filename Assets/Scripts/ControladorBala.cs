using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBala : MonoBehaviour
{
    public float xSpeed = 0f;
    public float ySpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 position = transform.position;
        position.x += xSpeed;
        position.x += ySpeed;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
