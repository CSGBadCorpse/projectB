using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DoorPuzzle : MonoBehaviour
{
    private const string PUZZLE_FIXED = "PuzzleFixed";
    public static DoorPuzzle Instance { get; private set; }
    public event EventHandler Event_OnPuzzleSolved;


    [SerializeField]
    private CinemachineVirtualCamera puzzleCamera;
    [SerializeField]
    private GameObject player;
    /*[SerializeField]
    private float hideTime = 60f;*/
    [SerializeField]
    private Canvas puzzleCanvas;
    [Header("开门动画")]
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private List<RawImage> circles;
    [SerializeField]
    private List<RawImage> oldCircles;
    [SerializeField]
    private List<RawImage> backgroundPics;

    [SerializeField]
    private GameObject triggerButton;



    private bool figuringPuzzle=false;
    private bool nearby = false;

    private bool firstCircle = false;
    private bool secondCircle = false;
    private bool thirdCircle = false;

    private void Awake()
    {
        Instance = this;
    }



    void Start()
    {
        FirstCircle.Instance.Event_OnCircleZero += FirstCircle_Event_OnCircleZero;
        SecondCircle.Instance.Event_OnCircleZero += SecondCircle_Event_OnCircleZero;
        ThirdCircle.Instance.Event_OnCircleZero += ThirdCircle_Event_OnCircleZero;

        FirstCircle.Instance.Event_OnCircleNotZero += FirstCircle_Event_OnCircleNotZero;
        SecondCircle.Instance.Event_OnCircleNotZero += SecondCircle_Event_OnCircleNotZero;
        ThirdCircle.Instance.Event_OnCircleNotZero += ThirdCircle_Event_OnCircleNotZero;
        foreach (RawImage image in circles)
        {
            image.enabled = false;
        }
        foreach (RawImage image in oldCircles)
        {
            image.enabled = false;
        }
        triggerButton.gameObject.SetActive(false);
    }

    private void ThirdCircle_Event_OnCircleNotZero(object sender, System.EventArgs e)
    {
        thirdCircle = false;
    }

    private void SecondCircle_Event_OnCircleNotZero(object sender, System.EventArgs e)
    {
        secondCircle = false;
    }

    private void FirstCircle_Event_OnCircleNotZero(object sender, System.EventArgs e)
    {
        firstCircle = false;
    }

    private void ThirdCircle_Event_OnCircleZero(object sender, System.EventArgs e)
    {
        thirdCircle = true;
    }

    private void FirstCircle_Event_OnCircleZero(object sender, System.EventArgs e)
    {
        firstCircle = true;
    }

    private void SecondCircle_Event_OnCircleZero(object sender, System.EventArgs e)
    {
        secondCircle = true;
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && nearby)
        {
            figuringPuzzle = !figuringPuzzle;
        }

        if (figuringPuzzle)
        {
            
            puzzleCamera.Priority = 10;
            StartCoroutine(HidePlayerDelay());

        }
        if (!figuringPuzzle)
        {
            StartCoroutine(ShowPlayer());
            firstCircle = false;
            secondCircle = false;
            thirdCircle = false;
            puzzleCamera.Priority = 0;
            //Debug.Log("active Player");

        }
        //Debug.Log("FirstCircle: "+ firstCircle);
        //Debug.Log("SecondCircle: " + secondCircle);
        //Debug.Log("ThirdCircle: " + thirdCircle);

        if (firstCircle && secondCircle && thirdCircle && TimeController.Instance.IsNow())
        {
            figuringPuzzle = false;
            //oldDoorOpen.SetActive(false);
            
            animator.SetTrigger(PUZZLE_FIXED);
            StartCoroutine(ShowPlayer());
            //puzzleState = PuzzleState.Solved;
            Event_OnPuzzleSolved?.Invoke(this, EventArgs.Empty);

        }


    }

    IEnumerator HidePlayerDelay()
    {
        yield return new WaitForSeconds(2f);
        player.SetActive(false); 

        foreach (RawImage image in backgroundPics)
        {
            image.enabled = true;
        }
        if (TimeController.Instance.IsNow())
        {
            foreach (RawImage image in circles)
            {
                image.enabled = true;
            }
            foreach (RawImage image in oldCircles)
            {
                image.enabled = false;
            }
        }else if (!TimeController.Instance.IsNow())
        {
            foreach (RawImage image in circles)
            {
                image.enabled = false;
            }
            foreach (RawImage image in oldCircles)
            {
                image.enabled = true;
            }
        }
    }
    IEnumerator ShowPlayer()
    {
        yield return new WaitForSeconds(2f);
        firstCircle = false;
        secondCircle = false;
        thirdCircle = false;
        foreach (RawImage image in backgroundPics)
        {
            image.enabled = false;
        }
        foreach (RawImage image in circles)
        {
            image.enabled = false;
        }
        foreach (RawImage image in oldCircles)
        {
            image.enabled = false;
        }
        player.SetActive(true);
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            nearby = true;
            triggerButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            nearby = false;
            triggerButton.gameObject.SetActive(false);
        }
    }

}
