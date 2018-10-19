using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]


public class PatrollingEnemies : MonoBehaviour
{
    public float distanceToMove;
    public int horizontalPatroll;      // 1 significa horizontal, 0 vertical. 
    public float speed;
    public int actualTarget;
    private Vector3 initPos;            // Posición inicial
    public Vector3[] movementPositions;
    private Rigidbody2D myRb;
    private bool upRightDownLeft;

    // Use this for initialization
    void Start()
    {
        upRightDownLeft = true;
        actualTarget = 0;
        myRb = GetComponent<Rigidbody2D>();
        movementPositions = new Vector3[2];

        SaveMovementData();
    }

    // Update is called once per frame
    void Update()
    {
        switch (horizontalPatroll)
        {
            case 0:
                AddVerticalVelocity();
                break;
            case 1:
                AddHorizontalVelocity();
                break;
            default:
                break;
        }


        switch (actualTarget)
        {
            case 0:
                if (transform.position == movementPositions[0])
                {
                    ToggleTarget();
                }
                break;
            case 1:
                if (transform.position == movementPositions[1])
                {
                    ToggleTarget();
                }
                break;
            default:
                break;
        }

    }

    private void SaveMovementData()
    {
        initPos = gameObject.transform.position;

        switch (horizontalPatroll)
        {
            case 0:
                SaveVerticalData();
                break;
            case 1:
                SaveHorizontalData();
                break;
            default:
                break;
        }

    }

    private void SaveVerticalData()
    {
        for (int i = 0; i < movementPositions.Length; i++)
        {
            if (i > 0)  //Guardo objetivos a priori
            {
                movementPositions[i] = new Vector3(initPos.x, initPos.y - distanceToMove, transform.position.z);
            }
            else
            {
                movementPositions[i] = new Vector3(initPos.x, initPos.y + distanceToMove, transform.position.z);
            }
        }
    }

    private void SaveHorizontalData()
    {
        for (int i = 0; i < movementPositions.Length; i++)
        {
            if (i > 0)  //Guardo objetivos a priori
            {
                movementPositions[i] = new Vector3(initPos.x - distanceToMove, initPos.y);
            }
            else
            {
                movementPositions[i] = new Vector3(initPos.x + distanceToMove, initPos.y);
            }
        }
    }

    private void AddVerticalVelocity()
    {
        if (upRightDownLeft)
        {
            AddForce(0f, speed);
        }
        else
        {
            AddForce(0f, speed * -1);
        }
    }

    private void AddHorizontalVelocity()
    {
        if (upRightDownLeft)
        {
            AddForce(speed , 0f);
        }
        else
        {
            AddForce(speed * -1, 0f);
        }
    }

    private void ToggleTarget()
    {
        if (actualTarget == 0)
        {
            actualTarget = 1;
        }
        else
        {
            actualTarget = 0;
        }

        if (upRightDownLeft)
        {
            upRightDownLeft = false;
        }
        else if (upRightDownLeft == false)
        {
            upRightDownLeft = true;
        }
    }

    private void AddForce(float velocityX, float velocityY)
    {
        myRb.velocity = new Vector2(velocityX, velocityY);
    }
}
