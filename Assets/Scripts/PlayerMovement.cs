using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] float jumpSpeed;
    
    [SerializeField] float rotationSpeed;

    private float ySpeed;
    private Vector3 velocity;
    

    private CharacterController CC;
    private Animator animator;

    [SerializeField] private Transform cameraTransform;
    
    
    void Awake()
    {
        CC = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (CC.isGrounded)
        {
            ySpeed = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
            }
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
        

        velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        CC.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState =  CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState =  CursorLockMode.None;
        }
    }

    
}
