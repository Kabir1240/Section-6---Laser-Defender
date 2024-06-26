using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Update()
    {
        InitBounds();
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0 + paddingLeft, 0 + paddingBottom));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1 - paddingRight, 1 - paddingTop));
    }

    void Move()
    {
        Vector3 delta = rawInput * speed * Time.deltaTime;

        // make sure the new x and y positions cannot exceed the min and max bounds of the screen.
        Vector2 newPos = new()
        {
            x = Math.Clamp(transform.position.x + delta.x, minBounds.x, maxBounds.x),
            y = Math.Clamp(transform.position.y + delta.y, minBounds.y, maxBounds.y)
        };

        transform.position = newPos;
    }

    void OnMove(InputValue value){
        rawInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        if (shooter != null)
        {
            shooter.isShooting = true;
        }
    }
}
