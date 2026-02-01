using UnityEngine;
using System.Collections.Generic;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    private Transform[] childTransforms;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childTransforms = GetComponentsInChildren<Transform>();
        SpawnWave();
    }

    void SpawnWave(int numEnemies = 5)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            int childIndex = Random.Range(0,childTransforms.Length);
            int prefabIndex = Random.Range(0,prefabs.Length);
            Instantiate(prefabs[prefabIndex], childTransforms[childIndex].position, childTransforms[childIndex].rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            SpawnWave();
        }
    }
}
