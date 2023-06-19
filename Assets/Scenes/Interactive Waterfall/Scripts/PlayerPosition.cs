using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    void Update()
    {

        Shader.SetGlobalVector("_playerDistance", new Vector4(this.transform.position.x, this.transform.position.y, this.transform.position.z, this.transform.localScale.x));
        Shader.SetGlobalFloat("Global_Radius", (this.transform.localScale.x/2f));
           
    }
}
