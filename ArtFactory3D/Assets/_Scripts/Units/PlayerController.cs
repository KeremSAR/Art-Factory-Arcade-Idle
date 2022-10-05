using System;
using UnityEngine;

namespace ArtFactory._Scripts.Units
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 direction;
        private Camera Cam;
        private Animator PlayerAnimator;
        [SerializeField] private float PlayerSpeed;
        private static readonly int Run = Animator.StringToHash("Run");

        private void Start()
        {
            Cam = Camera.main;
            PlayerAnimator = this.GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Controller();
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlayerAnimator.SetBool(Run, true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                PlayerAnimator.SetBool(Run,false);
            }
        }

        private void Controller()
        {
            Plane plane = new Plane(Vector3.up, transform.position);
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                this.direction = ray.GetPoint(distance);
            }

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(direction.x,0f, direction.z), PlayerSpeed* Time.deltaTime);
            var offset = direction - transform.position;
            if (offset.magnitude > 1f)
            {
                transform.LookAt(direction);
            }
        }
    }
}
