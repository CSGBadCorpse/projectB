using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    Scene scene;
    public void Update()
    {
        scene = SceneManager.GetActiveScene();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log(scene.name);
            switch (scene.name)
            {
                case "Level1-1":
                    SceneManager.LoadScene("Level1-2");
                    break;
                case "Level1-2":
                    SceneManager.LoadScene("Level1-3");
                    break;
                /*case "Level1-3":
                    SceneManager.LoadScene("Level2-1");
                    break;*/
                case "Level2-1":
                    SceneManager.LoadScene("Level2-2");
                    break;
            }
            
        }
    }
}
