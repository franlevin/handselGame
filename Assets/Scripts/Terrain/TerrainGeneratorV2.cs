using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneratorV2 : MonoBehaviour
{
    public GameObject[] terrainModules; // The array with every possible module.
    public TerrainModuleData Data;

    private float minTerrainLength = 300f;
    private float fullLength;
    private float lengthReached;

    // Start is called before the first frame update
    void Start()
    {
        
        // Subscription to static event for decreasing length of a destroyed module in full spawned length.
        TerrainModule.TerrainModuleDestroyed += DecreaseFullLength;
    }

    // Update is called once per frame
    void Update()
    {
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
            float floorPosition = (((terrainModule.transform.localScale.y) / 2)) * -1;
            Vector3 blockPosition = new Vector3(lengthReached, floorPosition, 0);
            Instantiate(terrainModule, blockPosition, Quaternion.identity);

            // Adds spawned block's length to "fullLength" & "lengthReached"
            fullLength += terrainModule.GetComponent<TerrainModule>().data.length;
            lengthReached += terrainModule.GetComponent<TerrainModule>().data.length;
        }
    }
}
