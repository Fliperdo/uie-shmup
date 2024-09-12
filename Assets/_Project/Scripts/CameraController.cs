using UnityEngine;

namespace shmup
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;
        public float speed = 2f;

        void Start() => transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        void LateUpdate()
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
}
