using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shmup
{
    [CreateAssetMenu(fileName = "EnemyType", menuName = "Shmup/Enemy Type", order = 0)]
    public class EnemyType : ScriptableObject
    {
        public GameObject enemyPrefab;
        public GameObject weaponPrefab;
        public float speed;
    }
}
