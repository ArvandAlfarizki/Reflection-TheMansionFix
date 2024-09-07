using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2f; // Kecepatan berjalan
    public float runSpeed = 4f; // Kecepatan lari
    public float turnSpeed = 100f; // Kecepatan rotasi pemain

    private float currentSpeed; // Kecepatan saat ini (berjalan atau lari)
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Mendapatkan referensi ke komponen Rigidbody
        animator = GetComponent<Animator>(); // Mendapatkan referensi ke komponen Animator
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;
        float moveInput = 0f;
        float turnInput = 0f;

        // Cek apakah pemain menekan tombol untuk berlari (shift atau tombol lain)
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Fire3"); // "Fire3" adalah default untuk tombol lari di input Unity
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Cek input layar sentuh
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;

            moveInput = (touchPosition.y > Screen.height / 2) ? 1 : -1; // Maju/Mundur berdasarkan layar sentuh
            turnInput = (touchPosition.x > Screen.width / 2) ? 1 : -1; // Rotasi berdasarkan layar sentuh
        }
        else
        {
            moveInput = Input.GetAxis("Vertical"); // Untuk maju/mundur
            turnInput = Input.GetAxis("Horizontal"); // Untuk rotasi kiri/kanan
        }

        // Gerakan maju/mundur berdasarkan arah karakter
        moveDirection = transform.forward * moveInput * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Rotasi karakter (kiri/kanan)
        float turn = turnInput * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Update animasi berdasarkan keadaan berjalan, lari, atau idle
        if (animator != null)
        {
            float movementMagnitude = Mathf.Abs(moveInput) * currentSpeed;

            // Set parameter animasi
            animator.SetFloat("Speed", movementMagnitude);
            animator.SetBool("IsRunning", isRunning);
            animator.SetBool("IsMoving", moveInput != 0 || turnInput != 0); // True jika pemain bergerak
        }
    }
}
//tes branch
//tes branch 2