using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneratorV2 : MonoBehaviour
{
    public GameObject[] terrainModules; // The array with every possible module.
    public TerrainModuleData Data;

    private float minTerrainLength = 100f;
    private float fullLength;
    private float lengthReached;

    // Start is called before the first frame update
    void Start()
    {
        TerrainModule.TerrainModuleDestroyed += DecreaseFullLength;
        //StartTerrain();
        //AddTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("FullLength es: " + fullLength);
        AddTerrain();
    }

    private void StartTerrain()
    {
        
    }

    private void DecreaseFullLength (float length)
    {
        fullLength -= length;
        Debug.Log("Decrece la length");
    }

    private void AddTerrain()
    {
        if (fullLength <= minTerrainLength)
        {
            Debug.Log("Arma nuevo terrain");
            int randomIndex = Random.Range(0, terrainModules.Length);
            GameObject terrainModule = terrainModules[randomIndex];

            // Determines the position for a new block and instantiates it
            Vector3 blockPosition = new Vector3(lengthReached, 0, 0);
            GameObject newBlock = Instantiate(terrainModule, blockPosition, Quaternion.identity);
            fullLength += terrainModule.GetComponent<TerrainModule>().data.length;
            lengthReached += terrainModule.GetComponent<TerrainModule>().data.length;
        }

        //Debug.Log(terrainModule.GetComponent<TerrainModule>().GetLength());


    }
}
