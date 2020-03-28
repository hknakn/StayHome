using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private const float LANE_DISTANCE = 1.0f;

    // Movement
    private CharacterController controller;
    private float jumpForce = 4.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    private float speed = 3.0f;
    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gather the inputs on which lane we should be
        if (MobileInput.Instance.SwipeLeft)
            MoveLane(false);
        if (MobileInput.Instance.SwipeRight)
            MoveLane(true);

        // Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * LANE_DISTANCE;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * LANE_DISTANCE;

        // Calculate move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;
        moveVector.y = 0f;
        moveVector.z = speed;

        // Move player
        controller.Move(moveVector * Time.deltaTime);
    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }
}
