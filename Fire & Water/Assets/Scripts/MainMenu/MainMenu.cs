using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public GameObject clickParticles;
    public GameObject panel;

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
        StartCoroutine(LoadScene((int)SceneIndexes.LEVEL_01));
    }

    IEnumerator LoadScene(int _sceneIndex)
    {
        panel.SetActive(true);
        animator.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_sceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
