using UnityEngine;

namespace shmup
{
    [CreateAssetMenu(fileName = "TripleShot", menuName = "Shmup/WeaponStrategy/TripleShot")]
    public class TripleShot : WeaponStrategy
    {
        public float spreadAngle = 15f;

        public override void Fire(Transform firepoint, LayerMask layer)
        {
            for (int i = 0; i < 3; i++)
            {
                var projectile = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);
                projectile.transform.SetParent(firepoint);
                projectile.transform.Rotate(0f, 0f, spreadAngle * (i - 1));
                projectile.layer = layer;

                var projectileComponent = projectile.GetComponent<Projectile>();
                projectileComponent.SetSpeed(projectileSpeed);

                Destroy(projectile, projectileLifetime);
            }
        }
    }
}
