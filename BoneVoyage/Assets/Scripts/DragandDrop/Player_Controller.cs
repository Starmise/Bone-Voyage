using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float speed = 5f;
    public float acceleration = 10f;  // Movimiento más suave
    public float jumpForce = 7f;
    public float pushDistance = 0.8f; // Distancia mínima para empujar una caja

    private CharacterController controller;
    private Vector3 velocity;
    public Transform raycastPoint;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Verificar si el personaje está en el suelo usando GroundCheck
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        // Resetear velocidad en Y si está en el suelo
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movimiento más suave
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 targetVelocity = new Vector3(moveX, 0, moveZ).normalized * speed;
        Vector3 moveDirection = Vector3.Lerp(controller.velocity, targetVelocity, acceleration * Time.deltaTime);

        controller.Move(moveDirection * Time.deltaTime);

        // SALTO - Solo puede saltar si está tocando el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpForce;
        }

        // Aplicar gravedad
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Intentar empujar cajas si nos movemos
        if (moveDirection.magnitude > 0)
        {
            TryPush(moveDirection);
        }
    }

    void TryPush(Vector3 direction)
    {
        RaycastHit hit;

        // El personaje debe estar más cerca para empujar
        if (Physics.Raycast(raycastPoint.position, direction, out hit, pushDistance))
        {
            PushableBox box = hit.collider.GetComponent<PushableBox>();

            if (box != null)
            {
                box.Move(direction);
            }
        }
    }
}