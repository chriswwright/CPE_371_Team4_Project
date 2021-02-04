using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Actor
{
    private Vector3 startPos;
    [SerializeField] private float doorSpeed = 0.1f;
    [SerializeField] private float doorHeight = 0.1f;
    [SerializeField] private float fallingScale = 1;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (activated)
        {
            if (transform.position.y < startPos.y + doorHeight)
            {
                transform.Translate(Vector3.up * doorSpeed * Time.deltaTime);
            }
        }
        else
        {
            if(transform.position.y > startPos.y)
            {
                transform.Translate(Vector3.down * fallingScale * doorSpeed * Time.deltaTime);
            }
        }
    }

    private void ActionOn()
    {
        if (!activated)
        {
            activated = true;

        }   
    }

    private void ActionOff()
    {
        if (activated)
        {
            activated = false;
        }
    }
}
