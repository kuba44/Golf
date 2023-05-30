using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] Rigidbody2D ballRigidbody;

    [SerializeField] float speed;
    [SerializeField] float drag;
    Vector3 lastVelocity;

    [SerializeField] Transform arrowPoint;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballRigidbody.velocity.magnitude < 0.1 ) 
        {
            ballRigidbody.velocity = Vector2.zero;       
        }

        ballRigidbody.drag = drag;

        lastVelocity = ballRigidbody.velocity;
    }

    private void OnMouseDrag()
    {
        if (ballRigidbody.velocity.magnitude > 0.1) { return; }

        arrowPoint.gameObject.SetActive(true);

        Vector3 mousePosition = Input.mousePosition;
        Vector3 ballPosition = mainCamera.WorldToScreenPoint(transform.localPosition);

        Vector3 pointDirection = new Vector2(ballPosition.x - mousePosition.x, ballPosition.y - mousePosition.y);

        float angle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;

        angle -= 90;

        arrowPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnMouseUp()
    {
        if (ballRigidbody.velocity.magnitude > 0.1 ) { return; }

        arrowPoint.gameObject.SetActive(false);

        Vector3 ballPosition = mainCamera.WorldToScreenPoint(transform.localPosition);
        Vector3 mousePositionEnd = Input.mousePosition;

        Vector3 hitDirection = new Vector2(ballPosition.x - mousePositionEnd.x, ballPosition.y - mousePositionEnd.y);

        ballRigidbody.AddForce(hitDirection * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float currentSpeed = lastVelocity.magnitude;
        Vector3 direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        ballRigidbody.velocity = direction * currentSpeed;
    }
}
