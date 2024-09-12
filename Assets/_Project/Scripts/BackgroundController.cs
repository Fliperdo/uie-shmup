using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace shmup
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] Transform[] backgrounds;
        [SerializeField] float smoothing = 10f;
        [SerializeField] float multiplier = 15f;

        Transform cam;
        Vector3 previousCamPos;

        void Awake()
        {
            cam = Camera.main.transform;
        }

        void Start()
        {
            previousCamPos = cam.position;
        }

        void Update()
        {
            for (var i = 0; i < backgrounds.Length; i++)
            {
                var parallax = (previousCamPos.x - cam.position.x) * (i * multiplier);
                var targetX = backgrounds[i].position.x + parallax;

                var targetPos = new Vector3(targetX, backgrounds[i].position.y, backgrounds[i].position.z);

                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPos, smoothing * Time.deltaTime);
            }

            previousCamPos = cam.position;
        }

    }
}
