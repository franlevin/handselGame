using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject[] terrainBlocks;
    public float blockWidth;
    public float generationDelay;

    private float blockCount;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(GenerateTerrain());
    }

    private IEnumerator GenerateTerrain()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, terrainBlocks.Length);
            GameObject terrainBlock = terrainBlocks[randomIndex];

            Vector3 blockPosition = new Vector3(blockCount * blockWidth, 0, 0);
            GameObject newBlock = Instantiate(terrainBlock, blockPosition, Quaternion.identity);

            blockCount++;

            yield return new WaitForSeconds(generationDelay);

            if (playerTransform.position.x >= (blockCount - 1) * blockWidth + blockWidth)
            {
                Destroy(newBlock);
                blockCount--;
            }
        }
    }
}
