using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    [Header(header: "Loading screen")]
    public GameObject loadingScreen;
    public Text progressText;
    public Slider progressSlider;

    private void Start()
    {
        
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
}
