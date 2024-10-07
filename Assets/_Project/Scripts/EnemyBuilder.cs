using UnityEngine;
using UnityEngine.Splines;
using utilities;

namespace shmup
{
    public class EnemyBuilder
    {
        public GameObject enemyPrefab;
        public SplineContainer spline;
        public GameObject weaponPrefab;
        public float speed = 1.0f;

        public EnemyBuilder SetBasePrefab(GameObject prefab)
        {
            enemyPrefab = prefab;
            return this;
        }

        public EnemyBuilder SetSpline(SplineContainer spline)
        {
            this.spline = spline;
            return this;
        }

        public EnemyBuilder SetWeaponPrefab(GameObject weaponPrefab)
        {
            this.weaponPrefab = weaponPrefab;
            return this;
        }

        public EnemyBuilder SetSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }

        public GameObject Build()
        {
            GameObject instance = GameObject.Instantiate(enemyPrefab);

            SplineAnimate splineAnimate = instance.GetOrAdd<SplineAnimate>();
            splineAnimate.Container = spline;
            splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
            splineAnimate.ObjectUpAxis = SplineAnimate.AlignAxis.ZAxis;
            splineAnimate.ObjectForwardAxis = SplineAnimate.AlignAxis.NegativeXAxis;
            splineAnimate.Alignment = SplineAnimate.AlignmentMode.SplineElement;
            splineAnimate.MaxSpeed = speed;
            splineAnimate.Restart(true);

            // weapons later

            instance.transform.position = spline.EvaluatePosition(0f);
            // if instantiaing waves, could set the positoin along the spline in a stagegexd value 0f to 1f

            return instance;
        }
    }
}
