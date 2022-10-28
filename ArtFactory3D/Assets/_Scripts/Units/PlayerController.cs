using System;
using ArtFactory._Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArtFactory._Scripts.Units
{
    public class PlayerController : MonoBehaviour
    {
        private Vector3 direction;
        private Camera Cam;
        private Animator PlayerAnimator;
        private PlayerStackManager _playerStackManager;

        [SerializeField] private float PlayerSpeed;
        [SerializeField] private float TurnSpeed;
        [SerializeField] private FloatingJoystick _floatingJoystick;
         
        
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Carrying = Animator.StringToHash("Carrying");

        private void Start()
        {
            Cam = Camera.main;
            PlayerAnimator = this.GetComponent<Animator>();
            _playerStackManager = this.GetComponent<PlayerStackManager>();
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                JoyisticMovement();
            }
          
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
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

        private void JoyisticMovement()
        {
            float horizantal = _floatingJoystick.Horizontal;
            float vertical = _floatingJoystick.Vertical;
            
            Vector3 addedPos = new Vector3(horizantal * PlayerSpeed * Time.deltaTime, 0,
                vertical * PlayerSpeed * Time.deltaTime);
            var transform1 = transform;
            transform1.position += addedPos;

            if (horizantal != 0 || vertical != 0)
            {
                PlayerAnimator.SetBool(Run, true);
                Vector3 direction = Vector3.forward * vertical + Vector3.right * horizantal;
                transform.rotation = Quaternion.Slerp(transform1.rotation, Quaternion.LookRotation(direction), TurnSpeed * Time.deltaTime);
              //  transform.rotation = Quaternion.LookRotation(direction);
            }
            else
            {
                PlayerAnimator.SetBool(Run, false);
            }
           
        }
       /* private void Controller()
        {
            Plane plane = new Plane(Vector3.up, transform.position);
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                this.direction = ray.GetPoint(distance);
            }
           
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(direction.x,0f, direction.z), PlayerSpeed* Time.deltaTime);
            var offset = direction - transform.position;
            if (offset.magnitude > 1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), TurnSpeed * Time.deltaTime);
                //transform.LookAt(direction);
            }
        }*/
    }
}
