using UnityEngine;

namespace shmup
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5.0f;
        public float smoothness = 0.5f;
        public float rollMultiplier = 15.0f;
        public float yawMultiplier = 5.0f;
        public float leanSpeed = 5.0f;

        public GameObject model;

        [Header("Camera Bounds")]
        public Transform camera;
        public float minX = -8.0f;
        public float maxX = 8.0f;
        public float minY = -4.0f;
        public float maxY = 4.0f;

        InputReader input;

        Vector3 currentVelocity;
        Vector3 targetPosition;

        void Start()
        {
            input = GetComponent<InputReader>();
        }

        void Update()
        {
            targetPosition += new Vector3(input.Move.x, input.Move.y, 0) * (speed * Time.deltaTime);

            var minPlayerX = camera.position.x + minX;
            var maxPlayerX = camera.position.x + maxX;
            var minPlayerY = camera.position.y + minY;
            var maxPlayerY = camera.position.y + maxY;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);

            // roll // https://www.google.com/search?q=what+is+yaw&rlz=1C1ONGR_enUS1064US1064&oq=what+is+yaw&gs_lcrp=EgZjaHJvbWUqDQgAEAAYgwEYsQMYgAQyDQgAEAAYgwEYsQMYgAQyCggBEAAYsQMYgAQyBwgCEAAYgAQyBwgDEAAYgAQyBwgEEAAYgAQyBwgFEAAYgAQyBwgGEAAYgAQyBwgHEAAYgAQyBwgIEAAYgAQyCQgJEAAYChiABNIBCDE2NDhqMGo3qAIAsAIA&sourceid=chrome&ie=UTF-8
            var targetRollAngle = -input.Move.y * rollMultiplier;
            var currentRoll = transform.localEulerAngles.x;
            var targetRoll = Mathf.LerpAngle(currentRoll, targetRollAngle, leanSpeed * Time.deltaTime);

            // yaw
            var targetYawAngle = input.Move.y * yawMultiplier;
            var currentYaw = transform.localEulerAngles.z;
            var targetYaw = Mathf.LerpAngle(currentYaw, targetYawAngle, leanSpeed * Time.deltaTime);


            transform.localEulerAngles = new Vector3(targetRoll, 0, targetYaw);
        }
    }
}
