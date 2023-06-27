using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup memberListShow;

    [System.Obsolete]
    private void Update()
    {
        if (!memberListShow.gameObject.active)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
