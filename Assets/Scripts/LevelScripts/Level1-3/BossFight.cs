using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    [SerializeField]
    private Animator mAnimator;
    [SerializeField]
    private Animation mAnimation;
    [SerializeField]
    private GameObject[] hitAreas;
    [SerializeField]
    private GameObject[] hit2DHandsArea;
    /*[SerializeField]
    private AudioSource mSource;*/

    private void Start()
    {
        Respawn.Instance.OnPlayerRespawn += Respwan_OnPlayerRespawn;
    }

    private void Respwan_OnPlayerRespawn(object sender, System.EventArgs e)
    {
        //mAnimator.Update(0f);
        /*AnimationState state = mAnimation["Take 001"];
        state.time = 0;
        mAnimation.Sample();*/

        mAnimator.SetBool("Entered", false);
        foreach (var hit in hit2DHandsArea)
        {
            hit.GetComponent<BoxCollider>().enabled = false;
        }
        foreach (var hit in hitAreas)
        {
            hit.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(StartFight());
           
            //StartCoroutine(PlayBGM());
            
        }
    }
    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(5f);
        foreach (var hit in hitAreas) 
        {
            hit.SetActive(true);
        }
        
        mAnimator.SetBool("Entered", true);
    }
    /*IEnumerator PlayBGM()
    {
        yield return new WaitForSeconds(3f); 
        mSource.gameObject.SetActive(true);
        mSource.Play();
    }*/
}
