using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class TouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform rect;

    private Vector3 newPosition;
    private bool touched;
    private int fingerID;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = true;
            fingerID = eventData.pointerId;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position, Camera.main, out newPosition);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (touched && fingerID == eventData.pointerId)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position, Camera.main, out newPosition);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (touched && fingerID == eventData.pointerId)
        {
            touched = false;
        }
    }

    public Vector2 GetPosition()
    {
        return newPosition;
    }
}
