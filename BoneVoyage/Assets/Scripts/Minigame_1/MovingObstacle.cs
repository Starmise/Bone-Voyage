using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 3.0f; // Velocidad del obst�culo
    public Vector3 moveDirection = Vector3.right; // Direcci�n del movimiento

    public float resetPosition = 6.0f; // Punto donde se reinicia el obst�culo
    public float startPosition = -6.0f; // Punto inicial del obst�culo

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;

        // Si el obst�culo sale del �rea de juego, lo reiniciamos
        if (Mathf.Abs(transform.position.x) > resetPosition)
        {
            transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        }
    }
}
