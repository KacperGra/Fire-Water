using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    public Animator buttonStartAnimation;
    public GameObject clickParticles;
    public GameObject panel;

    private void Start()
    {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(2f);
        buttonStartAnimation.SetBool("End", true);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var clickEffect = Instantiate(clickParticles) as GameObject;    
            clickEffect.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            clickEffect.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(EndGame());
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene((int)ScenesIndex.LEVEL_01));
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
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        panel.SetActive(true);
        animator.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
