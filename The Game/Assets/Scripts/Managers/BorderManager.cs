using UnityEngine;
using System.Collections;
using System;

public class BorderManager : MonoBehaviour
{
    public Transform leftBorderPosition;
    public Transform rightBorderPosition;
    public Transform upBorderPosition;
    public Transform downBorderPosition;
    public Transform[] spawners;

    public float sideOffset = 2f;
    public float upOffset = -0.5f;
    public float spawnerOffsetX = 0.2f;

    public const float initialColliderWitdth = 17;
    private BoxCollider2D colliderRect;

    void Awake()
    {
        //use playerprefs to save the borders and spawners positions
        this.colliderRect = this.GetComponent<BoxCollider2D>();
        SetColliderSize();
        SetBorderPositions();
        SetSpawnersPosition();
        Destroy(this.colliderRect);
    }

    private void SetSpawnersPosition()
    {
        var oldPosition = this.transform.position;
        var newPosition = new Vector2();

        for (int i = 0; i < this.spawners.Length; i++)
        {
            if (i % 2 == 0)
            {
                newPosition.x -= this.colliderRect.size.x / 2;
                newPosition.x += this.spawnerOffsetX;
            }
            else
            {
                newPosition.x += this.colliderRect.size.x / 2;
                newPosition.x -= this.spawnerOffsetX;
            }

            if (i == 0 || i == 1)
            {
                newPosition.y += this.colliderRect.size.y / 2;
            }
            else if(i == 2 || i == 3)
            {
                newPosition.y -= this.colliderRect.size.y / 2;
            }

            newPosition.y += this.colliderRect.offset.y;
            this.spawners[i].position = newPosition;
            newPosition = oldPosition;                
        }
    }

    private void SetBorderPositions()
    {
        var oldPosition = this.transform.position;
        oldPosition.y += this.colliderRect.offset.y;

        var newPosition = new Vector2();
        var scale = this.colliderRect.size.x / initialColliderWitdth;

        //left border
        newPosition = oldPosition;
        newPosition.x -= this.colliderRect.size.x / 2;
        this.leftBorderPosition.position = newPosition;

        //right border
        newPosition = oldPosition;
        newPosition.x += this.colliderRect.size.x / 2;
        this.rightBorderPosition.position = newPosition;

        //up border
        newPosition = oldPosition;
        newPosition.y += this.colliderRect.size.y / 2;
        this.upBorderPosition.position = newPosition;
        this.upBorderPosition.localScale = new Vector3(scale,1,1);

        //down border
        newPosition = oldPosition;
        newPosition.y -= this.colliderRect.size.y / 2;
        this.downBorderPosition.position = newPosition;
        this.downBorderPosition.localScale = new Vector3(scale, 1, 1);

    }

    private void SetColliderSize()
    {
        var colliderSize = this.colliderRect.size;
        colliderSize.x = (Camera.main.orthographicSize * 2 * Camera.main.aspect);// - this.sideOffset;
        this.colliderRect.size = colliderSize;

        var colliderOffset = this.colliderRect.offset;
        colliderOffset.x = 0;
        colliderOffset.y = this.upOffset;
        this.colliderRect.offset = colliderOffset;
    }
}
