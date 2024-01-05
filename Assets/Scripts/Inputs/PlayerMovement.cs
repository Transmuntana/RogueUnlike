using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private InputPlayer input = null;
    private Vector2 moveVector = Vector2.zero;
    public Vector2 lastMove = new Vector2(1,0);
    private Rigidbody2D rb = null;
    private float moveSpeed = 3f;

    private void Awake()
    {
        input = new InputPlayer();
        rb = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this.gameObject);
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
        if (GameManager._instance.playerHP <= 0) Kill();

    }
    private void OnMovementPerformed(InputAction.CallbackContext value) {
        moveVector = value.ReadValue<Vector2>();
        lastMove = moveVector;
    }
    private void OnMovementCanceled(InputAction.CallbackContext value) {
        moveVector = Vector2.zero;
    }
    private void Kill()
    {
        if (GameManager._instance.playerHP <= 0 && GameManager._instance.inventory[3] > 0)
        {
            GameManager._instance.playerHP = GameManager._instance.playerMaxHP;
            GameManager._instance.inventory[3]--;
        }
        else {
            //Destroy(GameManager._instance);
            Debug.Log("You lost!");
            for (int i = 0; i < 5; i++)
            {
                GameManager._instance.inventory[i] = 0;
            }
            GameManager._instance.playerHP = GameManager._instance.playerMaxHP;
            SceneManager.LoadScene("RandomizedLevel");
        }
    }
}
