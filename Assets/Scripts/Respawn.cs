using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Respawn Instance { private set; get; }
    public event EventHandler OnPlayerRespawn;

    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private Transform playerTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            ResetLevel();
        }
    }

    private void ResetLevel()
    {
        playerTransform.position = respawnPoint.transform.position;
        //BrokenBricks.Instance.ResetLevel();
        OnPlayerRespawn?.Invoke(this,EventArgs.Empty);
    }
}
