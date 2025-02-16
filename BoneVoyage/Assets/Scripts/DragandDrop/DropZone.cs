using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropZone : MonoBehaviour
{
    public string correctCubeType;
    private List<DragAndDrop> stackedCubes = new List<DragAndDrop>();
    public float spacing = 1.5f;
    public int requiredCubes = 1; // Número de cubos necesarios para completar la zona

    public bool IsCompleted => stackedCubes.Count >= requiredCubes;

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
            StartCoroutine(WaitAndPlaceCube(cube));
        }
        else
        {
            Debug.Log("Cubo incorrecto! (" + cube.cubeType + ")");
            StartCoroutine(ReturnToOriginalPosition(cube));
        }
    }

    private IEnumerator WaitAndPlaceCube(DragAndDrop cube)
    {
        yield return new WaitForSeconds(0.2f);  // Espera a que el cubo caiga antes de bloquearlo
        PlaceCube(cube);
    }

    private void PlaceCube(DragAndDrop cube)
    {
        Vector3 newPosition = FindAvailablePosition(cube);
        cube.transform.position = newPosition;
        cube.LockCubeInPlace();

        stackedCubes.Add(cube);
    }

    private Vector3 FindAvailablePosition(DragAndDrop cube)
    {
        Vector3 positionToCheck = transform.position;
        bool foundSpot = false;
        Vector3 newPos = positionToCheck;

        while (!foundSpot)
        {
            foundSpot = true;
            foreach (DragAndDrop stackedCube in stackedCubes)
            {
                if (IsOverlapping(stackedCube, newPos, cube))
                {
                    foundSpot = false;
                    newPos = RandomizePosition(newPos);
                    break;
                }
            }
        }

        return newPos;
    }

    private bool IsOverlapping(DragAndDrop stackedCube, Vector3 newPosition, DragAndDrop cube)
    {
        Vector3 cubeSize = cube.GetComponent<Renderer>().bounds.size;
        return newPosition.x < stackedCube.transform.position.x + stackedCube.GetComponent<Renderer>().bounds.size.x &&
               newPosition.x + cubeSize.x > stackedCube.transform.position.x &&
               newPosition.z < stackedCube.transform.position.z + stackedCube.GetComponent<Renderer>().bounds.size.z &&
               newPosition.z + cubeSize.z > stackedCube.transform.position.z;
    }

    private IEnumerator ReturnToOriginalPosition(DragAndDrop cube)
    {
        yield return new WaitForSeconds(0.2f); // Espera antes de devolver el cubo
        cube.ReturnToInitialPosition();
    }

    private Vector3 RandomizePosition(Vector3 basePosition)
    {
        return basePosition + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
    }
}
