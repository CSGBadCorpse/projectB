using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    //[SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            //Loader.Load(Loader.Scene.GameScene);
            StartCoroutine(LoadSceneMode());
            //SceneManager.LoadScene("LoadingPage");
        });
        /*quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });*/
        Time.timeScale = 1f;
    }
    IEnumerator LoadSceneMode()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LoadingPage");
    }
}
