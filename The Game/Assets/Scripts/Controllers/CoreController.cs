using UnityEngine;
using System.Collections;

public class CoreController : MonoBehaviour
{
    public int coreHealth = 3;

    private ParticleManager particleManager;
    private ObjectsController obj;
    private Collider2D coreCollider;

    public void Start()
    {
        this.coreCollider = this.GetComponent<Collider2D>();
        this.particleManager = GameObject.FindGameObjectWithTag("ParticleEffects").GetComponent<ParticleManager>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            this.obj = other.gameObject.GetComponent<ObjectsController>();
            this.coreHealth += this.obj.GetEffectPoints();
            this.obj.TryDestroyObject();

            this.particleManager.PlayParticle(2, other.contacts[0].point);

            if (this.coreHealth <= 0)
            {
                //big explosion
                this.particleManager.PlayParticle(1, other.contacts[0].point);
                PlayerController.isDead = true;
                this.coreCollider.enabled = false;
            }
        }
    }
    
}
