using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Game2D,
    Game3D
}

public class CameraController : MonoBehaviour
{
    //用来切换2D视角和3D视角
    public static CameraController Instance { private set; get; }
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField]
    private Transform globalLight;

    private Camera mainCamera;

    private GameMode mode;

    private bool switchMode;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        switchMode = false;
        mainCamera = Camera.main;
        globalLight.GetComponent<Light>().shadows = LightShadows.None;
    }

    private void Update()
    {
        switchMode = Input.GetKeyDown(KeyCode.Tab);//切换视角技能键
        if (switchMode && ViewChange.Instance.IsChangeFinished() && PlayerController.Instance.hasChangeSpaceSkill)
        {
            if (virtualCamera.Priority > 2)//如果是3d就切换回2d
            {
                globalLight.GetComponent<Light>().shadows = LightShadows.None;//2d情况把阴影消除
                //globalLight.GetComponent<Light>().shadows = LightShadows.Soft;
                virtualCamera.Priority = 1;
                //virtualCamera.m_Lens.FieldOfView = 60f;
                ViewChange.Instance.ChangeProjection = true;
                mode = GameMode.Game2D;
                PlayerController.Instance.Fix2DPosition();

            }
            else if (virtualCamera.Priority < 2)//如果是2d就切换回3d
            {
                globalLight.GetComponent<Light>().shadows = LightShadows.Soft;
                virtualCamera.Priority = 3;
                //virtualCamera.m_Lens.FieldOfView = 80f;
                ViewChange.Instance.ChangeProjection = true;
                mode = GameMode.Game3D;
                PlayerController.Instance.Fix3DPosition();//让角色移动到z=0的位置

            }
        }
    }

    public bool Is2DGame()
    {
        return mode==GameMode.Game2D;
    }
    public bool Is3DGame()
    {
        return mode == GameMode.Game3D;
    }
    public bool IsOrthographic()
    {

        return mainCamera.orthographic;
    }
    public CinemachineVirtualCamera GetVirtualCamera()
    {
        return virtualCamera;
    }
}
