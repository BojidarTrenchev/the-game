using UnityEngine;
using CnControls;

public class TouchInputManager : MonoBehaviour
{
    public Transform xBoundaryRight;
    public Transform yBoundaryUp;
    public Transform yBoundaryDown;
    public GameObject joystick;
    [HideInInspector]
    public bool isJoystickActive;

    private float xBound;
    private float yBoundUp;
    private float yBoundDown;
    private bool dragging;
    private float speedRatio;
    private Vector2 newPosition;
    private Vector2 newVelocity;
    private Vector2 direction;

    void Start()
    {
        this.direction = new Vector2();
        this.newPosition = new Vector2();
        this.newVelocity = new Vector2();
        float radius = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<SphereCollider>().radius;

        this.xBound = this.xBoundaryRight.position.x - radius;
        this.yBoundUp = this.yBoundaryUp.position.y - radius;
        this.yBoundDown = this.yBoundaryDown.position.y + radius;

        if (!this.isJoystickActive)
        {
            this.joystick.SetActive(false);
        }
    }

    void Update()
    {
        if (this.isJoystickActive)
        {
            GetSpeedRatio();
        }
        else
        {
            GetTouchPosition();
        }
    }

    public Vector2 GetNewPosition()
    {
        return this.newPosition;
    }

    public Vector2 GetNewVelocity(float maxSpeed)
    {
        this.newVelocity = this.direction * (this.speedRatio * maxSpeed);
        return this.newVelocity;
    }

    private void GetSpeedRatio()
    {
        this.direction.x = CnInputManager.GetAxis("Horizontal");
        this.direction.y = CnInputManager.GetAxis("Vertical");
        this.speedRatio = Mathf.Max(Mathf.Abs(this.direction.x), Mathf.Abs(this.direction.y));
    }

    private void GetTouchPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray))
                {
                    dragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && this.dragging)
            {
                this.newPosition = Camera.main.ScreenToWorldPoint(touch.position);
                //this.newPosition.x *= Time.deltaTime;
                //this.newPosition.y *= Time.deltaTime;

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                this.dragging = false;
            }
        }
    }

    public Vector2 ClampVector(Vector2 vector)
    {
        Vector2 newVector = vector;
        newVector.x = Mathf.Clamp(vector.x, -this.xBound, this.xBound);
        newVector.y = Mathf.Clamp(vector.y, this.yBoundDown, this.yBoundUp);
        return newVector;
    }
}
