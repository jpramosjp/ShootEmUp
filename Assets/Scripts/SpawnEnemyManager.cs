using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    public static SpawnEnemyManager Instance { get; private set; }


    public List<Transform> spawnPoints;
    public List<GameObject> enemys;

    public float cooldown;
    public float baseTime;

    public float chanceSpawnDuploOuTriplo = 10f;
    public bool canSpawn;
    public float SpawnBoss;
    public GameObject boss;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {

            Instance = this;
        }
    }
    void Start()
    {
        boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn)
        {
            if(cooldownEnemys())
            {
                int indiceSpawnPoint = Random.Range(0, spawnPoints.Count);
                Transform spawnPoint = spawnPoints[indiceSpawnPoint];

                bool spawnDuploOuTriplo = Random.Range(0f, 100f) <= chanceSpawnDuploOuTriplo;
                int quantidadeInimigos = spawnDuploOuTriplo ? Random.Range(2, 4) : 1;
                for (int i = 0; i < quantidadeInimigos; i++)
                {
                    int selectedEnemy = Random.Range(0, 3);
                    Instantiate(enemys[selectedEnemy], spawnPoint.position, spawnPoint.rotation);
                }
            
            }
            SpawnBoss -= Time.deltaTime;
            if(SpawnBoss <= 0)
            {
                canSpawn = false;
                InvokeBoss();
            } 
        }
    }

    public bool cooldownEnemys()
    {
        if(cooldown > baseTime)
        {
            cooldown = 0;
            chanceSpawnDuploOuTriplo++;
            return true;
        }

        cooldown += Time.deltaTime;
        return false;
    }


    public void InvokeBoss()
    {
        boss.SetActive(true);
        this.enabled = false;
    }
}
