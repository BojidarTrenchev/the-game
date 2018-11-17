using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public float smoothingValue;
    public RectTransform tr;

    private Vector2 direction;
    private Vector2 origin;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            this.touched = true;
            this.pointerID = eventData.pointerId;
            this.origin = eventData.position;
            Vector3 f = new Vector3();
            RectTransformUtility.ScreenPointToWorldPointInRectangle(this.tr, this.origin, Camera.main, out f);
            //Debug.Log(f);

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (touched && this.pointerID == eventData.pointerId)
        {
            this.direction = (eventData.position - this.origin).normalized;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (touched && this.pointerID == eventData.pointerId)
        {
            this.direction = Vector2.zero;
            this.touched = false;
        }
    }

    public Vector2 GetDirection()
    {
        this.smoothDirection = Vector2.MoveTowards(this.smoothDirection, this.direction, this.smoothingValue);
        return this.smoothDirection;
    }
}
