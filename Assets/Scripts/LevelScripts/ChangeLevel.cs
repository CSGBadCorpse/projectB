using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ChangeLevel : MonoBehaviour
{
    Scene scene;
    [SerializeField]
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
        }
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        SceneManager.LoadScene("Level1-1");
    }

    public void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Level1-1")
        {
            Shader.SetGlobalFloat("SphereRadius", 0.25f);
            Shader.SetGlobalFloat("TopDown_Radius", 0.25f);
        }
        if(scene.name == "Level1-3")
        {
            Shader.SetGlobalFloat("SphereRadius", 1.4f);
            Shader.SetGlobalFloat("TopDown_Radius", 1.4f);
            PlayerController.Instance.hasChangeSpaceSkill = true;
        }
        if (scene.name == "Level2-1")
        {
            PlayerController.Instance.hasChangeSpaceSkill = true;
        }
        if (scene.name == "Level2-2")
        {
            PlayerController.Instance.hasChangeSpaceSkill = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log(scene.name);
            switch (scene.name)
            {
                case "Level1-1":
                    Shader.SetGlobalFloat("SphereRadius", 1.4f);
                    Shader.SetGlobalFloat("TopDown_Radius", 1.4f);
                    SceneManager.LoadScene("Level1-3");
                    break;
                /*case "Level1-2":
                    Shader.SetGlobalFloat("SphereRadius", 1.4f);
                    Shader.SetGlobalFloat("TopDown_Radius", 1.4f);
                    SceneManager.LoadScene("Level1-3");
                    break;*/
                case "Level1-3":
                    SceneManager.LoadScene("Level2-1");
                    break;
                case "Level2-1":
                    SceneManager.LoadScene("Level2-2");
                    break;
            }
            
        }
    }
}
