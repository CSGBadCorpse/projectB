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
    [Header("Player下的cube")]
    private Transform playerTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetLevel();
        }
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
        playerTransform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, -1);
        OnPlayerRespawn?.Invoke(this,EventArgs.Empty);
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }
}
