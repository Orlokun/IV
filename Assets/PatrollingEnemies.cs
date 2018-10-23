using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]


public class PatrollingEnemies : MonoBehaviour
{

    #region GlobalVariables

    public float distanceToMove;
    public int typeOfPatrolling;      // 1 significa horizontal, 0 vertical. 
    public float speed;

    private int actualTarget;
    private Vector3 initPos;            // Posición inicial
    private Vector3[] movementPositions;    //Posiciones hacia las que se moverá por default

    private Rigidbody2D myRb;
    private bool upRightDownLeft;       //Sirve para saber si agregar fuerza positiva o negativa al rigidBody
    private bool canMove;

    #endregion

    // Use this for initialization
    void Start()
    {
        upRightDownLeft = true;
        actualTarget = 0;
        myRb = GetComponent<Rigidbody2D>();
        movementPositions = new Vector3[2];
        canMove = true;
        SaveMovementData();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            switch (typeOfPatrolling)
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


    #region MovementInfo

    private void SaveMovementData()
    {
        initPos = gameObject.transform.position;

        switch (typeOfPatrolling)
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

    #endregion

    #region AddVelocity
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


    private void AddForce(float velocityX, float velocityY)
    {
        myRb.velocity = new Vector2(velocityX, velocityY);
    }

    #endregion

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

    #region Events

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject incomingObj = collision.gameObject;
        if (gameObject.tag == null)
        {
            return;
        }
        else
        {
            string incomingTag = incomingObj.tag;
            switch (incomingTag)
            {
                case "Muralla":
                    ChangeMovement(incomingObj);
                    break;
                case "Player":
                    EvaluatePlayerTakeHit(incomingObj);
                    break;
                case "Projectile":
                    EvaluateTakeDamage(incomingObj);
                    break;
                default:
                    break;

            }
        }
    }
    #endregion

    #region EventUtils

    private void ChangeMovement(GameObject incomingObj)
    {
        if (actualTarget != 0)
        {
            movementPositions[1] = transform.position;
        }
        else
        {
            movementPositions[0] = transform.position;
        }

    }

    private void EvaluatePlayerTakeHit(GameObject incomingObj)
    {
        
    }

    private void EvaluateTakeDamage(GameObject incomingObj)
    {

    }

    #endregion
}
