using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField] AudioClip sfx_Die;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(sfx_Die, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
