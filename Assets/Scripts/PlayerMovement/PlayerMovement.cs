using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace ShadowChimera
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        public Vector2 _move;
        public Vector2 _look;
        public float aimValue;
        public float fireValue;

        private Vector3 nextPosition;
        private Quaternion nextRotation;

        public float rotationPower = 3f;
        public float rotationLerp = 0.5f;

        public float speed = 1f;

        [SerializeField] private CinemachineVirtualCamera _camMove;
        [SerializeField] private CinemachineVirtualCamera _camAim;

        private bool aiming = false;

        public void OnMove(InputValue value)
        {
            _move = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            _look = value.Get<Vector2>();
        }

        public void OnAim(InputValue value)
        {
            aimValue = value.Get<float>();
        }

        //public void OnFire(InputValue value)
        //{
        //    fireValue = value.Get<float>();
        //}

        public GameObject followTransform;

        private void Update()
        {

            #region Follow Transform Rotation

            //Rotate the Follow Target transform based on the input
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

            #endregion

            #region Vertical Rotation
            followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = followTransform.transform.localEulerAngles.x;

            //Clamp the Up/Down rotation
            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }


            followTransform.transform.localEulerAngles = angles;
            #endregion


            nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);

            if (_move.x == 0 && _move.y == 0)
            {
                nextPosition = transform.position;

                if (aimValue == 1)
                {
                    SwitchCamera(true);
                    //Set the player rotation based on the look transform
                    transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                    //reset the y rotation of the look transform
                    followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                }
                else if(aimValue == 0)
                {
                    SwitchCamera(false);
                }

                return;
            }
            float moveSpeed = speed / 100f;
            Vector3 vertical = new Vector3(0f, Physics.gravity.y * Time.deltaTime, 0f);
            Vector3 position = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed) + vertical;
            
            characterController.Move(position);


            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
            //reset the y rotation of the look transform
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        private void SwitchCamera(bool aim)
        {
            aiming = aim;
            _camMove.gameObject.SetActive(!aiming);
            _camAim.gameObject.SetActive(aiming);
        }
    }

    
}
