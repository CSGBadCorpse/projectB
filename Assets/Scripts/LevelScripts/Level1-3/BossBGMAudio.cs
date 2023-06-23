using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBGMAudio : MonoBehaviour
{

    [SerializeField]
    private AudioSource mSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            mSource.gameObject.SetActive(true);
            mSource.Play();
        }

    }
}
