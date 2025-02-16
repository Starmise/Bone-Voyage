using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public string cubeType;         // Ejemplo: "Rojo", "Azul", etc.
    public Vector3 initialPosition; // Guarda la posición original

    private Vector3 offset;
    private float zCoord;
    private bool locked = false;    // Indica si el objeto ya fue colocado correctamente

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (locked)
            return;

        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (locked)
            return;

        Vector3 newPosition = GetMouseWorldPos() + offset;
        if (newPosition.y < 0.5f)
            newPosition.y = 0.5f;
        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // Bloquea el objeto para que ya no pueda ser movido
    public void LockCubeInPlace()
    {
        locked = true;
        this.enabled = false; // Desactiva el script de arrastre

        // Si tiene Rigidbody, lo congelamos para que no se mueva
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
