using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public GameObject panel;
    public Animator animator;
    private PlayerLevels playerLevels;
    public List<Sprite> buttonSprites;
    public List<Image> buttons;

    private void Start()
    {
        playerLevels = new PlayerLevels();
        playerLevels.UnlockLevel(PlayerLevels.LevelIndex.LEVEL_01);
        for(int i = 0; i < buttons.Count; ++i)
        {
            if(playerLevels.IsLevelUnlocked((PlayerLevels.LevelIndex)i))
            {
                buttons[i].sprite = buttonSprites[i + 1];
            }
            else
            {
                buttons[i].sprite = buttonSprites[0];
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene((int)LevelIndex.MAIN_MENU);
        }
    }

    public void SelectLevelButton(string levelIndex)
    {
        switch(levelIndex)
        {
            case "1":
                if(playerLevels.IsLevelUnlocked(PlayerLevels.LevelIndex.LEVEL_01))
                {
                    var sceneIndex = playerLevels.GetLevelIndex(PlayerLevels.LevelIndex.LEVEL_01);
                    StartCoroutine(LoadScene(sceneIndex));
                }              
                else
                {

                }
                break;
        }
    }

    IEnumerator LoadScene(int _sceneIndex)
    {
        panel.SetActive(true);
        animator.SetBool("Start", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_sceneIndex + 2);
    }
}
