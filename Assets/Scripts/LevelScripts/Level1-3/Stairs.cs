using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private bool end;
    // Start is called before the first frame update
    void Start()
    {
        end = false;
        BossEnd.Instance.OnBossEnd += BossEnd_OnBossEnd;
    }

    private void BossEnd_OnBossEnd(object sender, System.EventArgs e)
    {
        end  = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (end) 
        {
            if (System.Math.Abs(transform.position.x - 0) > 0.2)
            {
                transform.Translate(-2 * Time.deltaTime, 0, 0);
            }
        }
        
        
    }
}
