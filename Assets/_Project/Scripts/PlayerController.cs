using UnityEngine;

namespace shmup
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5.0f;
        public float smoothness = 0.2f;
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

        private bool _useMouseInput;
        private Vector3 _distanceToMove;
        private Vector3 _previousMousePosition;

        void Start()
        {
            input = GetComponent<InputReader>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                _useMouseInput = true;
            }
            else if (Input.anyKeyDown)
            {
                _useMouseInput = false;
            }


            if (_useMouseInput)
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                _distanceToMove = mousePosition - transform.position;
                _distanceToMove = Vector3.ClampMagnitude(_distanceToMove, (speed * Time.deltaTime));
            }
            else
            {
                _distanceToMove = new Vector3(input.Move.x, input.Move.y, 0) * (speed * Time.deltaTime);
            }
            targetPosition += _distanceToMove;

            // Clamp target position within camera bounds
            var minPlayerX = camera.position.x + minX;
            var maxPlayerX = camera.position.x + maxX;
            var minPlayerY = camera.position.y + minY;
            var maxPlayerY = camera.position.y + maxY;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);

            // Smoothly move the player to the target position
            if (_useMouseInput)
            {
                transform.position = targetPosition;
            } else
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
            }

            var targetRollAngle = -_distanceToMove.normalized.x * rollMultiplier;
            var targetYawAngle = _distanceToMove.normalized.y * yawMultiplier;

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
