using UnityEngine;

public class ObjectsController : MonoBehaviour
{
    public float ejectSpeed = 15;
    public bool isGood;
    public int effectPoints;// if these points are positive then the object will heal the player otherwise it will damage it
    public int lifePoints = 1;// only special balls have more than 1 life point

    [HideInInspector]
    public float colliderRadius;
    [HideInInspector]
    public bool ejected;

    private CircleCollider2D circle;
    private Rigidbody2D rb;
    private Transform objTransform;
    private Vector2 direction;
    private Vector2 newLocalPosition;

    public void Awake()
    {
        //Physics2D.IgnoreLayerCollision(8, this.gameObject.layer);
        this.circle = this.GetComponent<CircleCollider2D>();
        this.colliderRadius = this.circle.radius;
        this.rb = this.GetComponent<Rigidbody2D>();
        this.objTransform = this.GetComponent<Transform>();
        this.newLocalPosition = new Vector2();
    }

    public void Eject()
    {
        this.rb.isKinematic = false;
        this.rb.AddForce(this.direction * this.ejectSpeed, ForceMode2D.Impulse);
    }

    public void SetDirection(float directionX, float directionY)
    {
        this.direction.x = directionX;
        this.direction.y = directionY;
    }

    public void MoveObject()
    {
        // not very good method for moving an object with RB, but this is the easiest and most understandable way
        //because I cannot use local position in RB
        this.newLocalPosition = this.objTransform.localPosition;
        this.newLocalPosition.y -= this.colliderRadius * 2;
        this.objTransform.localPosition = this.newLocalPosition;
    }

    public int GetEffectPoints()
    {
        if (!this.isGood)
        {
            return -this.effectPoints;
        }
        else
        {
            return this.effectPoints;
        }
    }

    public void TryDestroyObject()
    {
        this.lifePoints--;
        if (this.lifePoints <= 0)
        {
            //play animation/particle (happens in ShieldController)
            Destroy(this.gameObject);
        }
    }
}
