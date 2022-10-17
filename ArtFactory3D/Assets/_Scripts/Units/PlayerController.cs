using System;
using ArtFactory._Scripts.Managers;
using UnityEngine;

namespace ArtFactory._Scripts.Units
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 direction;
        private Camera Cam;
        private Animator PlayerAnimator;
        private PlayerStackManager _playerStackManager;
        [SerializeField] private float PlayerSpeed;
        
        
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Carrying = Animator.StringToHash("Carrying");

        private void Start()
        {
            Cam = Camera.main;
            PlayerAnimator = this.GetComponent<Animator>();
            _playerStackManager = this.GetComponent<PlayerStackManager>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Controller();
                if (_playerStackManager.balls.Count > 1)
                {
                    PlayerAnimator.SetBool(Carrying,true);
                }
                if (_playerStackManager.balls.Count <=1)
                {
                    PlayerAnimator.SetBool(Carrying,false);
                }
               
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlayerAnimator.SetBool(Run, true);
                if (_playerStackManager.balls.Count>1)
                {
                    PlayerAnimator.SetBool(Carrying,true);
                }
                
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
