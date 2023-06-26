using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonClick : MonoBehaviour
{
    [SerializeField]
    private GameObject coverCanvas;
    [SerializeField] private Button playButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void aniBtn()
    {
        playButton.transform.DOPunchPosition(new Vector3(0, 20, 0), 0.4f, 6, 0.3f);
        playButton.transform.DOPunchScale(new Vector3(0.3f, 0, 0), 0.3f, 4, 0.4f);
        coverCanvas.SetActive(true);
    }
}
