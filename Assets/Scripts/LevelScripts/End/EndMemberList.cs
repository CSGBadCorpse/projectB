using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMemberList : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup disapearCanvas;
    [SerializeField]
    private CanvasGroup memberCanvas;
    private bool show;
    [SerializeField]
    private float appearSpeed;
    // Start is called before the first frame update
    void Start()
    {
        show = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (disapearCanvas.alpha == 0)
        {
            StartCoroutine(ShowMemberList());
        }
        if (show)
        {
            memberCanvas.alpha += appearSpeed;
            if(memberCanvas.alpha == 1)
            {
                StartCoroutine(ReturnToMainMenu());
            }
        }
    }
    IEnumerator ShowMemberList()
    {
        yield return new WaitForSeconds(5f);
        show = true;

    }
    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MenuPage");
    }
}
