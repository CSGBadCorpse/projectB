using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstCircle : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static FirstCircle Instance { get; private set; }
    public event EventHandler Event_OnCircleZero;
    public event EventHandler Event_OnCircleNotZero;
    public event EventHandler Event_OnFirstCircleEnd;

    [SerializeField]
    private RectTransform thirdCircle;
    private bool isDragging = false;
    private Vector2 lastMousePosition;
    private Vector2 centerPosition;

    private bool solved = false;

    private float offset = 4f;

    private bool firstCircleRotate = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DoorPuzzle.Instance.Event_OnPuzzleSolved += DoorPuzzle_Event_OnPuzzleSolved;
    }

    private void DoorPuzzle_Event_OnPuzzleSolved(object sender, EventArgs e)
    {
        solved = true;
    }

    private void Update()
    {
        if (this.GetComponent<RectTransform>().rotation.eulerAngles.z > offset || this.GetComponent<RectTransform>().rotation.eulerAngles.z < -offset)
        {
            Event_OnCircleNotZero?.Invoke(this, EventArgs.Empty);
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (TimeController.Instance.IsNow())
        {
            isDragging = true;
        }


        lastMousePosition = eventData.position;
        centerPosition = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, transform.position);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        
        isDragging = false;
        if ((this.GetComponent<RectTransform>().rotation.eulerAngles.z < offset && this.GetComponent<RectTransform>().rotation.eulerAngles.z > -offset ||
            this.GetComponent<RectTransform>().rotation.eulerAngles.z < 360 + offset && this.GetComponent<RectTransform>().rotation.eulerAngles.z > 360 - offset))
        {
            //this.GetComponent<RectTransform>().rotation = Quaternion.identity;
            //Event_OnPuzzleSolved?.Invoke(this, EventArgs.Empty);
            Event_OnCircleZero?.Invoke(this, EventArgs.Empty);
            Event_OnFirstCircleEnd?.Invoke(this, EventArgs.Empty);
        }
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if (isDragging && firstCircleRotate && !solved)
        {
            Vector2 mouseDelta = eventData.position - lastMousePosition;
            float angle = Vector2.SignedAngle(lastMousePosition - centerPosition, eventData.position - centerPosition);
            this.GetComponent<RectTransform>().Rotate(Vector3.forward, angle, Space.World);
            thirdCircle.Rotate(Vector3.forward, angle / 4, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(this.GetInstanceID().ToString());
        if (transform.name == "FirstCircle")
        {
            firstCircleRotate = true;
        }
    }
}

