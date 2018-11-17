using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnerController : MonoBehaviour
{
    [Range(1, 4)]
    public int spawnerNumber = 1;
    public bool isActive = true;
    public float spawnerRotation = 40;
    public GameObject[] initialObjects;
    public GameObject[] objectsToSpawn;
    public Transform spawnPositionFirstObj;

    private GameObject newObject;
    private ObjectsController newObjController;
    private Queue<ObjectsController> objects;
    private Vector2 positionToInstantiate;
    private Vector2 spawnPositionLastObj;
    private Transform spawnerPosition;


    public void Start()
    {
        this.spawnerPosition = this.GetComponent<Transform>();
        this.positionToInstantiate = this.spawnPositionFirstObj.position;
        this.objects = new Queue<ObjectsController>();

        this.SetObjects();

        this.spawnerPosition.Rotate(new Vector3(0, 0, this.spawnerRotation));
    }

    public void SetObjects()
    {
        for (int i = 0; i < this.initialObjects.Length; i++)
        {
            int randomIndex = Random.Range(0, this.initialObjects.Length);
            this.newObject = Instantiate(this.initialObjects[randomIndex]) as GameObject;

            this.newObjController = this.newObject.GetComponent<ObjectsController>();

            if (i > 0)
            {
                this.positionToInstantiate.y += this.newObjController.colliderRadius * 2 * i;
            }

            this.newObject.transform.SetParent(this.spawnPositionFirstObj);
            this.newObject.transform.position = this.positionToInstantiate;

            this.SetDirection();

            this.objects.Enqueue(newObjController);

            this.positionToInstantiate = this.spawnPositionFirstObj.position;

            if (i == this.initialObjects.Length - 1)
            {
                this.spawnPositionLastObj = this.newObject.transform.localPosition;
            }

        }
    }

    public void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
    }

    public void Fire()
    {
        this.objects.Dequeue().Eject();

        foreach (var obj in this.objects)
        {
            obj.MoveObject();
        }

        // instantiation of a new object happens in GateController
    }

    private void SetDirection()
    {
        switch (this.spawnerNumber)
        {
            case 1:
                this.newObjController.SetDirection(1, -1);
                break;
            case 2:
                this.newObjController.SetDirection(-1, -1);
                break;
            case 3:
                this.newObjController.SetDirection(1, 1);
                break;
            case 4:
                this.newObjController.SetDirection(-1, 1);
                break;
        }
    }

    public void InstantiateNewObject()
    {
        int randomIndex = Random.Range(0, this.objectsToSpawn.Length);
        this.newObject = Instantiate(this.objectsToSpawn[randomIndex]) as GameObject;
        this.newObjController = newObject.GetComponent<ObjectsController>();

        this.newObject.transform.SetParent(this.spawnPositionFirstObj);
        this.newObject.transform.localPosition = this.spawnPositionLastObj;

        this.SetDirection();

        this.objects.Enqueue(this.newObjController);
    }
}
