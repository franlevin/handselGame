using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModule : MonoBehaviour
{
    [SerializeField] public TerrainModuleData data;
    [SerializeField] private float length;


    // Start is called before the first frame update
    void Start()
    {
        length = data.length;
        Debug.Log("El length es:" + length);
        //StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetLength()
    {
        return length;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SelfDestruct());
        }
    }

    private void OnDisable()
    {
        //IEnumerator
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
