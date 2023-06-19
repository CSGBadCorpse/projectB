using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorAudioPlaye : MonoBehaviour
{
    [SerializeField]
    private AudioSource bellAudioSource1;
    [SerializeField]
    private AudioSource bellAudioSource2;

    private void Start()
    {
        bellAudioSource1.gameObject.SetActive(false);
        bellAudioSource2.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            bellAudioSource1.gameObject.SetActive(true);
            bellAudioSource2.gameObject.SetActive(true);
            bellAudioSource1.enabled = true;
            bellAudioSource2.enabled = true;
        }
    }
}
