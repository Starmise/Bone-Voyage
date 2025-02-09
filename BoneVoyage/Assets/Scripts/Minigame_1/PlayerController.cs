using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 1.0f;  // Distancia fija de movimiento
    public float moveSpeed = 5.0f;     // Velocidad del movimiento
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                Move(Vector3.forward);
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                Move(Vector3.back);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                Move(Vector3.left);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                Move(Vector3.right);
        }
    }

    void Move(Vector3 direction)
    {
        targetPosition = transform.position + direction * moveDistance;
        StartCoroutine(MoveCoroutine());
    }

    System.Collections.IEnumerator MoveCoroutine()
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // Ajusta la posición exacta
        isMoving = false;
    }

    // Detecta las colisiones con obstáculos
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Aquí se maneja el caso en que el jugador toca un obstáculo
            Debug.Log("¡Perdiste! Colisionaste con un obstáculo.");
            // Reinicia la escena
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else if (other.CompareTag("Goal"))
        {
            // Aquí se maneja el caso en que el jugador toca la meta
            Debug.Log("¡Ganaste! Llegaste a la meta.");
            // Puedes reiniciar la escena o hacer algo para mostrar que se ganó
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
