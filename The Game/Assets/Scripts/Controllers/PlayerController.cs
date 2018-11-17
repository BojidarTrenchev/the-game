using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 20;
    public bool isJoystickActive = true;

    public static bool isDead;

    private Rigidbody2D rb;// core rigidbody
    private CircleCollider2D circleCol;// used only for proper wall collision
    private TouchInputManager input;
    private Vector2 newPosition;
    private readonly int objectLayer = 10; // object layer number

    public void Start()
    {
        this.circleCol = this.GetComponentInChildren<CircleCollider2D>();
        Physics2D.IgnoreLayerCollision(this.circleCol.gameObject.layer, objectLayer);

        this.rb = GameObject.FindGameObjectWithTag("Core").GetComponent<Rigidbody2D>();
        this.input = GameObject.FindGameObjectWithTag("Canvas").GetComponent<TouchInputManager>();
        this.input.isJoystickActive = this.isJoystickActive;
    }

    public void Update()
    {
        if (isDead)
        {
            //stop game
        }
    }

    public void FixedUpdate()
    {
        if (this.isJoystickActive)
        {
            this.newPosition = this.input.GetNewVelocity(this.maxSpeed);
            this.rb.velocity = this.newPosition;
        }
        else
        {
            this.newPosition = this.input.GetNewPosition();
            this.rb.MovePosition(this.newPosition);
        }

        this.rb.position = this.input.ClampVector(this.rb.position);
    }
}