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
        if (scene.name == "Level1-1")
        {
            Shader.SetGlobalFloat("SphereRadius", 0.25f);
            Shader.SetGlobalFloat("TopDown_Radius", 0.25f);
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
                    
                    SceneManager.LoadScene("Level1-2");
                    break;
                case "Level1-2":
                    Shader.SetGlobalFloat("SphereRadius", 1.4f);
                    Shader.SetGlobalFloat("TopDown_Radius", 1.4f);
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
