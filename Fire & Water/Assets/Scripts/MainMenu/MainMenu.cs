using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject clickParticles;

    [Header(header:"Loading screen")]
    public GameObject loadingScreen;
    public Text progressText;
    public Slider progressSlider;

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
        loadingScreen.SetActive(true);

        StartCoroutine(LoadAsync((int)SceneIndexes.LEVEL_01));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (operation.isDone.Equals(false))
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = (progress * 100).ToString() + "%";
            progressSlider.value = progress;

            yield return null;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
