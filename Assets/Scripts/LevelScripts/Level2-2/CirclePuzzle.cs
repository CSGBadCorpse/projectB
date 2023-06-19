using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class CirclePuzzle : MonoBehaviour, IPointerEnterHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static CirclePuzzle Instance { get; private set; }
    public event EventHandler Event_OnPuzzleSolved;
    [SerializeField]
    private RectTransform firstCircle;
    [SerializeField]
    private RectTransform secondCircle;
    [SerializeField]
    private RectTransform thirdCircle;

    RawImage firstRawImage;
    RawImage secondRawImage;
    RawImage thirdRawImage;

    private int firstCircleAngle;
    private Quaternion targetRotation;
    //private float speed=30f;

    private bool isDragging = false;
    private Vector2 lastMousePosition;
    private Vector2 centerPosition;

    private bool firstCircleRotate = false;
    private bool secondCircleRotate = false;

    private float offset = 4f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        firstRawImage = firstCircle.gameObject.GetComponent<RawImage>();
        secondRawImage = secondCircle.gameObject.GetComponent<RawImage>();
        thirdRawImage = thirdCircle.gameObject.GetComponent<RawImage>();
    }

    public void ActiveAllCircles()
    {
        firstRawImage.enabled = true;
        secondRawImage.enabled = true;
        thirdRawImage.enabled = true;
    }
        
    public void DeactiveAllCircles()
    {
        firstRawImage.enabled = false;
        secondRawImage.enabled = false;
        thirdRawImage.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (TimeController.Instance.IsNow())
        {
            isDragging = true;
        }
        

        lastMousePosition = eventData.position;
        centerPosition = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, firstCircle.position);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        /*Debug.Log("firstAngle" + firstCircle.rotation.eulerAngles.z % 360f);
        Debug.Log("secondCircle" + secondCircle.rotation.eulerAngles.z % 360f);
        Debug.Log("thirdCircle" + thirdCircle.rotation.eulerAngles.z % 360f);*/
        if ((firstCircle.rotation.eulerAngles.z < offset && firstCircle.rotation.eulerAngles.z > -offset ||
            firstCircle.rotation.eulerAngles.z < 360+offset && firstCircle.rotation.eulerAngles.z > 360-offset )&& 
           (secondCircle.rotation.eulerAngles.z < offset && secondCircle.rotation.eulerAngles.z > -offset ||
           secondCircle.rotation.eulerAngles.z < 360+offset && secondCircle.rotation.eulerAngles.z > 360-offset )&&
           (thirdCircle.rotation.eulerAngles.z < offset && thirdCircle.rotation.eulerAngles.z  > -offset||
           thirdCircle.rotation.eulerAngles.z < 360+offset && thirdCircle.rotation.eulerAngles.z > 360-offset))
        {
            firstCircle.rotation = Quaternion.identity;
            secondCircle.rotation = Quaternion.identity;
            thirdCircle.rotation = Quaternion.identity;
            Event_OnPuzzleSolved?.Invoke(this, EventArgs.Empty);
            //Debug.Log("Finished!!");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if (isDragging && firstCircleRotate)
        {
            Vector2 mouseDelta = eventData.position - lastMousePosition;
            float angle = Vector2.SignedAngle(lastMousePosition-centerPosition, eventData.position - centerPosition);
            firstCircle.Rotate(Vector3.forward, angle, Space.World);
            thirdCircle.Rotate(Vector3.forward, angle/4, Space.World);
            lastMousePosition = Input.mousePosition;
        }
        if(isDragging && secondCircleRotate)
        {
            //Debug.Log("OnBeginDrag");
            Vector2 mouseDelta = eventData.position - lastMousePosition;
            float angle = Vector2.SignedAngle(lastMousePosition - centerPosition, eventData.position - centerPosition);
            secondCircle.Rotate(Vector3.forward, angle, Space.World);
            thirdCircle.Rotate(Vector3.forward, angle / 2, Space.World);
            lastMousePosition = Input.mousePosition;
        }

    }

    /*public void FirstCircleActive()
    {
        firstCircleRotate = true;
        secondCircleRotate = false;
    }
    public void SecondCircleActive()
    {
        firstCircleRotate = false;
        secondCircleRotate = true;
    }*/

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(this.GetInstanceID().ToString());
        if (transform.name == firstCircle.name)
        {
            firstCircleRotate = true;
        }
        if (transform.name == secondCircle.name)
        {
            //Debug.Log("SecondCircleBeginRotate");
            secondCircleRotate = true;
        }
    }

    /*private void Update()
    {
        //Debug.Log(firstCircle.);
    }*/
}
