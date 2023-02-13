using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 6f;
    public float turnSmoothTime = .1f;
    float gravity = -9.81f;
    public float groundDistance = 1f;
    public float jump = 3f;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;
    [SerializeField] Animator animator;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= .1f){
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            if (isGrounded){
                animator.SetBool("isMoving", true);
            }
            else {
                animator.SetBool("isMoving", false);
            }
        }

        else {
            animator.SetBool("isMoving", false);
        }

        StartCoroutine(Jump());

        // if (Input.GetButtonDown("Cancel"))
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Enemy")){
            Debug.Log("Kena woy");
        }
        }

    IEnumerator Jump(){
        if (Input.GetButtonDown("Jump")){
            if (isGrounded){
                velocity.y = Mathf.Sqrt(jump * -2f * gravity);
                animator.SetBool("isJump", true);
                yield return new WaitForSecondsRealtime(1f);
                // yield return new WaitForSeconds(3f);
                animator.SetBool("isJump", false);
            }
        }
    }
}
