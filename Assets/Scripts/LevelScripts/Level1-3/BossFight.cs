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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            mAnimator.SetBool("Entered",true);
        }
    }
}
