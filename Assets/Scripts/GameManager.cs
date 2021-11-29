using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] Megaman player;
  

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && gameoverUI == false)
            Pausegame();



    }

    public void RestartLevel(int Nivel)
    {
        SceneManager.LoadScene(Nivel);
        Time.timeScale = 1;
        gameoverUI.SetActive(true);
    }

    public void Pausegame()
    {
        /*gamePaused = gamePaused ? false : true;
        player.gamePaused = gamePaused;
        pauseUI.SetActive(gamePaused);
        Time.timeScale = gamePaused ? 0 : 1;*/
    }

    
}
