using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public int enemiesAlive;
    public GameObject[] enemyPrefab;
    private Vector3 spawnLocation;
    public float spawnRate = 1.0f;
    public int[] enemyCount;
    public int roundNumber;
    void Awake()
    {
        instance = this;
    }
    public void StartWave(int _roundNumber)
    {
        roundNumber = _roundNumber - 1;
        enemiesAlive = enemyCount[roundNumber];
        spawnLocation = transform.Find("Spawn Location").gameObject.transform.position;
        StartCoroutine("SpawnEnemy");
    }
    IEnumerator SpawnEnemy()
    {
        GameObject enemyToSpawn = enemyPrefab[roundNumber];
        spawnRate = 5.0f / enemyToSpawn.GetComponent<Enemy>().speed;
        for(int i=0; i<enemyCount[roundNumber]; i++)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(enemyToSpawn, spawnLocation, enemyToSpawn.transform.rotation);
        }
    }
}
