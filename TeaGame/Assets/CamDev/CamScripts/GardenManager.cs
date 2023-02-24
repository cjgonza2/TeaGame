using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    [SerializeField] private string sceneName; //this will tell the manager which scene it's in and which garden to populate.

    [SerializeField] private List<GameObject> teaPlants = new List<GameObject>();

    [SerializeField] private List<Vector3> gardenPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < teaPlants.Count; i++)
        {
            GameObject _plant;
            _plant = Instantiate(teaPlants[i], gardenPositions[i], Quaternion.identity);
            Debug.Log(teaPlants[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
