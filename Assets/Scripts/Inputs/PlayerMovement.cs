using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputPlayer input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    private float moveSpeed = 3f;

    private void Awake()
    {
        input = new InputPlayer();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() {
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCanceled;
    }
    private void OnDisable() {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCanceled;
    }
    private void FixedUpdate() {
        rb.velocity = moveVector*moveSpeed;
    }
    private void OnMovementPerformed(InputAction.CallbackContext value) {
        moveVector = value.ReadValue<Vector2>();
    }
    private void OnMovementCanceled(InputAction.CallbackContext value) {
        moveVector = Vector2.zero;
    }
}
