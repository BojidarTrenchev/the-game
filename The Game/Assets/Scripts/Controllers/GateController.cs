using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour
{
    private SpawnerController spawner;
    private ObjectsController objcontroller;
    public void Start()
    {
        this.spawner = this.GetComponentInParent<SpawnerController>();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            this.objcontroller = other.gameObject.GetComponent<ObjectsController>();
            if (!this.objcontroller.ejected)
            {
                this.objcontroller.ejected = true;
                this.spawner.InstantiateNewObject();
            }
        }
    }
}
