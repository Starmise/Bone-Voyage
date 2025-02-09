using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 3.0f; // Velocidad del obstáculo
    public Vector3 moveDirection = Vector3.right; // Dirección del movimiento

    public float resetPosition = 6.0f; // Punto donde se reinicia el obstáculo
    public float startPosition = -6.0f; // Punto inicial del obstáculo

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;

        // Si el obstáculo sale del área de juego, lo reiniciamos
        if (Mathf.Abs(transform.position.x) > resetPosition)
        {
            transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        }
    }
}
