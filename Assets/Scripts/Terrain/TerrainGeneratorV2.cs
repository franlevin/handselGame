using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneratorV2 : MonoBehaviour
{
    public GameObject[] terrainModules; // The array with every possible module.
    public TerrainModuleData Data;

    private float minTerrainLength = Screen.width;
    private float forwardTerrainLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        //StartTerrain();
        Debug.Log("Screen width is" + minTerrainLength);
        //AddTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        AddTerrain();
    }

    private void StartTerrain()
    {
        
    }

    private void AddTerrain()
    {
        

        if (forwardTerrainLength <= minTerrainLength)
        {
            int randomIndex = Random.Range(0, terrainModules.Length);
            GameObject terrainModule = terrainModules[randomIndex];

            // Determines the position for a new block and instantiates it
            Vector3 blockPosition = new Vector3(forwardTerrainLength, 0, 0);
            GameObject newBlock = Instantiate(terrainModule, blockPosition, Quaternion.identity);
            forwardTerrainLength += terrainModule.GetComponent<TerrainModule>().data.length;
        }

        //Debug.Log(terrainModule.GetComponent<TerrainModule>().GetLength());
        Debug.Log("Prueba en terrain generator: " + forwardTerrainLength);


    }
}
