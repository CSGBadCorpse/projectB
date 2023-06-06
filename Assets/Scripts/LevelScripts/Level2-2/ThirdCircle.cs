using System;
using UnityEngine;

public class ThirdCircle : MonoBehaviour
{

    public static ThirdCircle Instance { get; private set; }
    public event EventHandler Event_OnCircleZero;
    public event EventHandler Event_OnCircleNotZero;
    private float offset = 4f;

    private bool firstCircleEnd = false;
    private bool secondCircleEnd = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FirstCircle.Instance.Event_OnFirstCircleEnd += FirstCircle_Event_OnFirstCircleEnd;
        SecondCircle.Instance.Event_OnSecondCircleEnd += SecondCircle_Event_OnSecondCircleEnd;
    }


    private void SecondCircle_Event_OnSecondCircleEnd(object sender, EventArgs e)
    {
        secondCircleEnd = true;
    }

    private void FirstCircle_Event_OnFirstCircleEnd(object sender, EventArgs e)
    {
        firstCircleEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<RectTransform>().rotation.eulerAngles.z > offset || this.GetComponent<RectTransform>().rotation.eulerAngles.z < -offset)
        {
            Event_OnCircleNotZero?.Invoke(this, EventArgs.Empty);
        }

        if ( (this.GetComponent<RectTransform>().rotation.eulerAngles.z < offset && this.GetComponent<RectTransform>().rotation.eulerAngles.z > -offset ||
            this.GetComponent<RectTransform>().rotation.eulerAngles.z < 360 + offset && this.GetComponent<RectTransform>().rotation.eulerAngles.z > 360 - offset))
        {
            //this.GetComponent<RectTransform>().rotation = Quaternion.identity;
            //Event_OnPuzzleSolved?.Invoke(this, EventArgs.Empty);
            Event_OnCircleZero?.Invoke(this, EventArgs.Empty);
        }
    }
}
