using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropZone : MonoBehaviour
{
    public string correctCubeType; // Tipo correcto (ejemplo: "Rojo")
    private List<DragAndDrop> stackedCubes = new List<DragAndDrop>(); // Objetos ya colocados
    public float spacing = 1.5f;     // Espacio entre posiciones (puedes ajustar este valor)

    private void OnTriggerEnter(Collider other)
    {
        DragAndDrop cube = other.GetComponent<DragAndDrop>();
        if (cube != null)
        {
            CheckCube(cube);
        }
    }

    public void CheckCube(DragAndDrop cube)
    {
        if (cube.cubeType == correctCubeType)
        {
            // Objeto correcto: se coloca en el DropZone y se bloquea
            PlaceCube(cube);
            Debug.Log("Objeto colocado correctamente en la zona " + correctCubeType);
        }
        else
        {
            Debug.Log("¡Objeto incorrecto! (" + cube.cubeType + ") no pertenece a esta zona (" + correctCubeType + ").");
            StartCoroutine(ReturnToOriginalPosition(cube));
        }
    }

    private void PlaceCube(DragAndDrop cube)
    {
        // Calcula la posición final dentro de la zona
        Vector3 newPosition = FindAvailablePosition(cube);
        cube.transform.position = newPosition;
        cube.transform.rotation = Quaternion.identity;

        // Bloquea el objeto para que no se mueva
        cube.LockCubeInPlace();

        // Lo añade a la lista de objetos colocados
        stackedCubes.Add(cube);
    }

    private Vector3 FindAvailablePosition(DragAndDrop cube)
    {
        Vector3 positionToCheck = transform.position; // Usamos la posición base del DropZone

        bool foundSpot = false;
        Vector3 newPos = positionToCheck;

        while (!foundSpot)
        {
            foundSpot = true;

            // Revisamos las posiciones alrededor del cubo para ver si hay espacio libre
            foreach (DragAndDrop stackedCube in stackedCubes)
            {
                if (IsOverlapping(stackedCube, newPos, cube))
                {
                    foundSpot = false;
                    newPos = RandomizePosition(newPos); // Si hay superposición, intentamos otro lugar
                    break;
                }
            }
        }

        return newPos;
    }

    private bool IsOverlapping(DragAndDrop stackedCube, Vector3 newPosition, DragAndDrop cube)
    {
        // Comprobamos si el nuevo cubo choca con los cubos ya apilados
        Vector3 cubeSize = cube.GetComponent<Renderer>().bounds.size;

        if (newPosition.x < stackedCube.transform.position.x + stackedCube.GetComponent<Renderer>().bounds.size.x &&
            newPosition.x + cubeSize.x > stackedCube.transform.position.x &&
            newPosition.z < stackedCube.transform.position.z + stackedCube.GetComponent<Renderer>().bounds.size.z &&
            newPosition.z + cubeSize.z > stackedCube.transform.position.z)
        {
            return true; // Hay superposición
        }
        return false; // No hay superposición
    }

    private Vector3 RandomizePosition(Vector3 lastPosition)
    {
        // Hacemos pequeños cambios aleatorios para encontrar un lugar libre
        float randomX = Random.Range(lastPosition.x - spacing, lastPosition.x + spacing);
        float randomZ = Random.Range(lastPosition.z - spacing, lastPosition.z + spacing);

        // Limitar para que no se vaya demasiado lejos
        return new Vector3(randomX, lastPosition.y, randomZ);
    }

    // Si el objeto es incorrecto, se devuelve a su posición original mediante interpolación
    private IEnumerator ReturnToOriginalPosition(DragAndDrop cube)
    {
        Vector3 targetPos = cube.initialPosition;
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPos = cube.transform.position;

        while (elapsed < duration)
        {
            cube.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cube.transform.position = targetPos;
    }
}
