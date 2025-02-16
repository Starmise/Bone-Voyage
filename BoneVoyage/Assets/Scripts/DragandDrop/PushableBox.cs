using UnityEngine;

public class PushableBox : MonoBehaviour
{
    public float moveDistance = 1.0f; // La distancia que se moverá la caja en cada empuje
    public LayerMask obstacleLayer;   // Capas que bloquearán el movimiento

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * moveDistance;

        // Verifica si hay un obstáculo en la dirección
        if (!Physics.Raycast(transform.position, direction, moveDistance, obstacleLayer))
        {
            rb.MovePosition(targetPosition);
        }
    }
}