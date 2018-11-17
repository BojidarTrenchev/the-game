using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour
{
    public int shieldHealth = 5;

    private ParticleManager particleManager;

    private int initialShieldHealth;
    private ObjectsController obj;
    private SpriteRenderer shieldSprite;
    private Collider2D shiledCollider;
    private Rigidbody2D rb;

    public void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.shieldSprite = this.GetComponent<SpriteRenderer>();
        this.shiledCollider = this.GetComponent<Collider2D>();
        this.particleManager = GameObject.FindGameObjectWithTag("ParticleEffects").GetComponent<ParticleManager>();
        this.initialShieldHealth = this.shieldHealth;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            this.obj = other.gameObject.GetComponent<ObjectsController>();
            this.shieldHealth += this.obj.GetEffectPoints();
            this.obj.TryDestroyObject();

            this.particleManager.PlayParticle(0, other.contacts[0].point);

            ChangeColor();

            if (this.shieldHealth <= 0)
            {
                this.shiledCollider.enabled = false;
            }
        }
    }

    private void ChangeColor()
    {
        Color shieldColor = this.shieldSprite.color;
        if (this.shieldHealth <= 0)
        {
            shieldColor.a = 0;
        }
        else
        {
            shieldColor.a -= shieldColor.a / this.initialShieldHealth;
        }

        this.shieldSprite.color = shieldColor;
    }
}
