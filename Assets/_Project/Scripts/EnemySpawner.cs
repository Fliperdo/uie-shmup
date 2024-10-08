using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace shmup
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<EnemyType> enemyTypes;
        public int maxEnemies = 10;
        public float spawnInterval = 2f;

        List<SplineContainer> splines;
        EnemyFactory enemyFactory;

        float spawnTimer;
        int enemiesSpawned;

        void OnValidate()
        {
            splines = new List<SplineContainer>(GetComponentsInChildren<SplineContainer>());
        }

        void Start()
        {
            enemyFactory = new EnemyFactory();
        }

        void Update()
        {
            spawnTimer += Time.deltaTime;

            if (enemiesSpawned < maxEnemies && spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }

        void SpawnEnemy()
        {
            EnemyType enemyType = enemyTypes[Random.Range(0, enemyTypes.Count)];
            SplineContainer spline = splines[Random.Range(0, splines.Count)];

            enemyFactory.CreateEnemy(enemyType, spline);
            enemiesSpawned++;
        }
    }
}
