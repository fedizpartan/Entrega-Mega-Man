using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountEnemy : MonoBehaviour
{
    [SerializeField] int NumberEnemies;

    GameObject[] enemigos;
    public Text ContadorEnemigos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        ContadorEnemigos.text = "Enemigos:" + enemigos.Length.ToString();

    }




}
