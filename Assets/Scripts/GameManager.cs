using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] Megaman player;
    [SerializeField] int NumberEnemies;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        WinUI.SetActive(false);
        gameoverUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartLevel( int Nivel)
    {
        SceneManager.LoadScene(Nivel);
        Time.timeScale = 1;
    }

    public void ReduceEnemies()
    {
        NumberEnemies = NumberEnemies - 1;
        if(NumberEnemies < 1)
        {
            win();
          
        }

    }

    void win()
    {
        gameOver = true;
        Time.timeScale = 0;
        //player.gamePaused = true;
        gameoverUI.SetActive(true);
    }

    
}
