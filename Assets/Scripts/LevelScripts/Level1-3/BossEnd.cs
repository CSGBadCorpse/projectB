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
}
