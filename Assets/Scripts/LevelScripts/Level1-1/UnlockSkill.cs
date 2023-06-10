using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockSkill : MonoBehaviour
{

    [SerializeField]
    private GameObject text;

    private void Start()
    {
        text.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //Debug.Log("Enter");
            text.gameObject.SetActive(true);
            PlayerController.Instance.hasChangeSpaceSkill = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
        }
    }
}
