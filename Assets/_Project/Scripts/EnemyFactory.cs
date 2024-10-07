using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utilities;
using UnityEngine.Splines;

namespace shmup
{
    public class EnemyFactory
    {
        public GameObject CreateEnemy(EnemyType enemyType, SplineContainer spline)
        {
            EnemyBuilder builder = new EnemyBuilder()
                .SetBasePrefab(enemyType.enemyPrefab)
                .SetSpline(spline)
                .SetSpeed(enemyType.speed);

            return builder.Build();
        }

        // More factory methods here, with enemies that may not even follow a spline
    }
}
