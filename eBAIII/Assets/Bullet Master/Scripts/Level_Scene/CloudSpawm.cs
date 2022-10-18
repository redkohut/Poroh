using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloudSpawm : MonoBehaviour
{
    #region Spawm
    [Space(6)]
    [Header("\tCloud Spawn")]
    [Space(6)]
    [SerializeField] private List<GameObject> cloudPrefab;
    [SerializeField] private Transform spawnParent;
    [Space(6)]
    [Header("\tSpawm Position Cloud")]
    [Space(6)]
    [SerializeField] private float maxY = 22.0f;
    [SerializeField] private float minY = 11.5f;
    [Space(7)]
    [SerializeField] private float minX = 4.0f;
    [SerializeField] private float maxX = 30.0f;
    [Space(6)]
    [Header("\tSpawn Intervals")]
    [Space(6)]
    [SerializeField] private float startDelay = 2.0f;
    [SerializeField] private float repeatRate = 2.0f;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        StartSpawnCloud();
        InvokeRepeating("Spawn", startDelay, repeatRate);
    }

/*    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        Spawn();

    }*/

    private void Spawn()
    {
        Vector3 spawnVec = SpawnPosition();
        int randCloud = Random.Range(0, cloudPrefab.Count) - 1;
        if (randCloud < 0) randCloud = 0; // check idex position in the list
        GameObject cloud = Instantiate(cloudPrefab[randCloud], spawnVec, Quaternion.identity, spawnParent);
        //Destroy(cloud, 15f);
    }

    private Vector3 SpawnPosition()
    {
        float y = Random.Range(minY, maxY);
        float x = gameObject.transform.position.x;
        return new Vector3(x, y);
    }

    private void StartSpawnCloud()
    {
        for (int i = 0; i < 20; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Vector3 spawnVec = new Vector3(x, y);
            int randCloud = Random.Range(0, cloudPrefab.Count) - 1;
            if (randCloud < 0) randCloud = 0; // check idex position in the list
            GameObject cloud = Instantiate(cloudPrefab[randCloud], spawnVec, Quaternion.identity, spawnParent);
        }
    }
}
