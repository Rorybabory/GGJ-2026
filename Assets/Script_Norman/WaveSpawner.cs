using UnityEngine;
using System.Collections.Generic;


public class WaveSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    private Transform[] childTransforms;
    private GameObject waveText;
    private float waveTextTimer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveText = transform.Find("WaveText").gameObject;
        childTransforms = GetComponentsInChildren<Transform>();
        SpawnWave();
    }

    void SpawnWave(int numEnemies = 5)
    {
        waveTextTimer = 0.0f;
        waveText.SetActive(true);
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
        if (waveText.activeSelf)
        {
            waveTextTimer += Time.deltaTime;
            if (waveTextTimer > 2.0)
            {
                waveText.SetActive(false);
            }
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            SpawnWave();
        }
    }
}
