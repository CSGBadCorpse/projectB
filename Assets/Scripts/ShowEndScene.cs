using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEndScene : MonoBehaviour
{
    [Header("切换遮罩")]
    [SerializeField]
    private CanvasGroup effectCanvas;
    [SerializeField]
    private float disappearSpeed;

    public bool show;
    // Start is called before the first frame update
    void Start()
    {
        show = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            effectCanvas.alpha -= disappearSpeed;
            if (effectCanvas.alpha == 0)
            {
                show = false;
            }

        }
    }
}
