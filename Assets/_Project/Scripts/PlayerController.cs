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
            // Update target position based on input
            targetPosition += new Vector3(input.Move.x, input.Move.y, 0) * (speed * Time.deltaTime);

            // Clamp target position within camera bounds
            var minPlayerX = camera.position.x + minX;
            var maxPlayerX = camera.position.x + maxX;
            var minPlayerY = camera.position.y + minY;
            var maxPlayerY = camera.position.y + maxY;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);

            // Smoothly move the player to the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);

            // Calculate target roll and yaw angles based on input
            var targetRollAngle = -input.Move.x * rollMultiplier;
            var targetYawAngle = input.Move.y * yawMultiplier;

            // Smoothly interpolate to the target roll and yaw angles
            var currentRotation = transform.localEulerAngles;
            var targetRotation = new Vector3(
                Mathf.LerpAngle(currentRotation.x, targetRollAngle, leanSpeed * Time.deltaTime),
                0,
                Mathf.LerpAngle(currentRotation.z, targetYawAngle - 90, leanSpeed * Time.deltaTime)
            );

            // Apply the new rotation
            transform.localEulerAngles = targetRotation;
        }
    }
}
