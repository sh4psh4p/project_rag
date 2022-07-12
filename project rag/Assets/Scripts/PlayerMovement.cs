using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float yaw = 0;
    private float pitch = 0;
    private float speed;

    private Rigidbody rb;

    private GameObject cam;

    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float sensitivity = 2;
    public float jumpHeight = 2;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.transform.GetComponent<Rigidbody>();

        cam = transform.GetChild(0).transform.gameObject;

        QualitySettings.vSyncCount = 1;

        Application.targetFrameRate = 165;
    }

    void Update()
    {
        Look();

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(rb.transform.position, Vector3.down, 1 + 0.001f))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }

        else
        {
            speed = walkSpeed;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Look()
    {
        pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -90, 90);
        yaw += Input.GetAxisRaw("Mouse X") * sensitivity;

        cam.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }

    void Movement()
    {
        Vector2 axis = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")).normalized * speed;
        Vector3 forward = Quaternion.Euler(0, yaw, 0) * Vector3.forward;
        Vector3 wishDirection = (forward * axis.x + cam.transform.right * axis.y + Vector3.up * rb.velocity.y);
        rb.velocity = wishDirection;
    }
}