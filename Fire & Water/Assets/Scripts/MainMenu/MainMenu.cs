using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject clickParticles;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var clickEffect = Instantiate(clickParticles) as GameObject;    
            clickEffect.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            clickEffect.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene((int)SceneIndexes.LEVEL_01);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
