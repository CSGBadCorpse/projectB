using UnityEngine;
using UnityEngine.EventSystems;

public class CircleActive : MonoBehaviour, IBeginDragHandler, IPointerEnterHandler
{

    public void OnBeginDrag(PointerEventData eventData)
    {
       /* if (TimeController.Instance.IsNow())
        {
            isDragging = true;
        }


        lastMousePosition = eventData.position;
        centerPosition = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, firstCircle.position);*/
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*Debug.Log(transform.name);
        if (transform.name == "FirstCircle")
        {
            CirclePuzzle.Instance.FirstCircleActive();
        }
        else if (transform.name == "SecondCircle")
        {
            CirclePuzzle.Instance.SecondCircleActive();
        }*/
    }
}
