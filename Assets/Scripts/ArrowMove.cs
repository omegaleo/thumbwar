using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    
    enum Movement { UP_DOWN, LEFT_RIGHT }

    [SerializeField] private Movement movement;
    [SerializeField] private float offSet = 1.0f;
    [SerializeField] private float speed = 2.0f;

    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 positivePosition;
    [SerializeField] private Vector3 negativePosition;
    [SerializeField] private bool moveToNegative = true;
    [SerializeField] private bool moveToPositive = false;

    private void Start()
    {
        initialPosition = this.transform.position;
        if(movement == Movement.UP_DOWN)
        {
            negativePosition =  new Vector3(initialPosition.x, initialPosition.y + offSet, initialPosition.z);
        }
        else
        {
            negativePosition = new Vector3(initialPosition.x - offSet, initialPosition.y, initialPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(moveToNegative)
        {
            if(movement == Movement.UP_DOWN)
            {
                if (Round(transform.position.y,2) < Round(negativePosition.y, 2))
                {
                    transform.position = Vector3.Lerp(transform.position, negativePosition, Time.deltaTime * speed);
                }
                else
                {
                    moveToNegative = false;
                    moveToPositive = true;
                }
            }
            else if(movement == Movement.LEFT_RIGHT)
            {
                if (Round(transform.position.x,2) > Round(negativePosition.x, 2))
                {
                    transform.position = Vector3.Lerp(transform.position, negativePosition, Time.deltaTime * speed);
                }
                else
                {
                    moveToNegative = false;
                    moveToPositive = true;
                }
            }
        }
        else if(moveToPositive)
        {
            if (movement == Movement.UP_DOWN)
            {
                if (Round(transform.position.y, 2) > Round(initialPosition.y, 2))
                {
                    transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * speed);
                }
                else
                {
                    moveToNegative = true;
                    moveToPositive = false;
                }
            }
            else if (movement == Movement.LEFT_RIGHT)
            {
                if (Round(transform.position.x, 2) < Round(initialPosition.x, 2))
                {
                    transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * speed);
                }
                else
                {
                    moveToNegative = true;
                    moveToPositive = false;
                }
            }
        }
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
}
