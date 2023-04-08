using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBricks : MonoBehaviour
{


    public static BrokenBricks Instance { private set; get; }

    [SerializeField]
    private float brokenTime = 1.5f;
    private float currentTime;
    private bool startCoutdown;
    //当角色碰到这个板砖的时候开始计时
    //计时结束，板砖消失

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startCoutdown = false;
        currentTime = brokenTime;
        Respawn.Instance.OnPlayerRespawn += Respawn_OnPlayerRespawn;
    }

    private void Respawn_OnPlayerRespawn(object sender, System.EventArgs e)
    {
        ResetLevel();
    }

    private void Update()
    {
        if (startCoutdown)
        {
            currentTime -= Time.deltaTime;
        }
        if(currentTime <= 0)
        {
            currentTime = 0;
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("Player"))
        {
            startCoutdown = true;
        }
    }

    public void ResetLevel()
    {
        this.gameObject.SetActive(true);
        currentTime = brokenTime;
        startCoutdown = false;
    }
}
