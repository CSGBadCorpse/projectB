using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    [SerializeField]
    private BossEnd bossEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && !bossEnd.bossDead)
        {
            Respawn.Instance.ResetLevel();
        }
    }
}
