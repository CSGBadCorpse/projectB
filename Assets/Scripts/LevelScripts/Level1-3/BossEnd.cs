using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class BossEnd : MonoBehaviour
{
    public static BossEnd Instance;
    public bool bossDead = false;
    [SerializeField]
    private BoxCollider[] bossArms;
    [SerializeField]
    private AudioSource mBossAudioSource;
    [SerializeField]
    private AudioClip hitGroundClip;
    [SerializeField]
    private AudioClip fistHitClip;
    [SerializeField]
    private Right2DHand rightHand;
    [SerializeField]
    private Left2DHand leftHand;

    public event EventHandler OnBossEnd;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var boss in bossArms)
        {
            boss.enabled = false;
        }
        Respawn.Instance.OnPlayerRespawn += Respwan_OnPlayerRespawn;
    }

    private void Respwan_OnPlayerRespawn(object sender, System.EventArgs e)
    {
        if (!bossDead)
        {
            ResetBoss();
        }
        
    }

    public void bossEnd()
    {
        bossDead = true;
        OnBossEnd?.Invoke(this, EventArgs.Empty);
    }
    public void MeshColliderActivate()
    {
        foreach (var boss in bossArms)
        {
            boss.enabled = true;
        }
    }
    private void ResetBoss()
    {
        bossDead = false;
        foreach (var boss in bossArms)
        {
            boss.enabled = false;
        }
    }

    public void HitGroundClip()
    {
        mBossAudioSource.clip = hitGroundClip;
        mBossAudioSource.Play();
    }
    public void FistHitClip()
    {
        mBossAudioSource.clip = fistHitClip;
        mBossAudioSource.Play();
    }

    public void Activate2DHand()
    {
        rightHand.Activate2DHit();
        leftHand.Activate2DHit();
    }
    public void Deactivate2DHand()
    {
        rightHand.Deactivate2DHit();
        leftHand.Deactivate2DHit();
    }
    public void ActivateRightHand()
    {
        rightHand.Activate2DHit();
    }
    public void DeactivateRightHand()
    {
        rightHand.Deactivate2DHit();
    }
    public void ActivateLeftHand()
    {
        leftHand.Activate2DHit();
    }
    public void DeactivateLeftHand()
    {
        leftHand.Deactivate2DHit();
    }
}
