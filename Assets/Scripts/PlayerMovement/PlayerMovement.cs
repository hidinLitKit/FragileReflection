using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using WeaponSystem;

namespace FragileReflection
{
    public class PlayerMovement : MonoBehaviour
    {
        //mine
        

        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerAnimController playerAnimController;
        private Transform playerTransform;
        public Vector2 _move;
        public Vector2 _look;
        public float aimValue;
        public float fireValue;
        public float walkValue;

        private Vector3 _nextPosition;
        private Quaternion _nextRotation;

        public float rotationPower = 3f;
        public float rotationLerp = 0.5f;

        public float speed = 1f;
        [SerializeField] private float _sprintSpeed = 2f;
        [SerializeField] private float _crouchSpeed = 0.5f;

        [SerializeField] private CinemachineVirtualCamera _camMove;
        [SerializeField] private CinemachineVirtualCamera _camAim;

        private bool _aiming = false;
        private bool _moving = false;
        private bool _sprinting = false;
        private bool _crouching = false;


        private float _sprintValue;
        private float _crouchValue;

        private float _inventValue;

        private void Awake()
        {
            playerTransform = characterController.gameObject.transform;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //mine
            //WeaponManager.ChangeWeapon(weapon[0]);
        }

        public void OnExit(InputValue value)
        {
            Debug.Log("Exit");
        }
        public void OnMove(InputValue value)
        { 
            _move = value.Get<Vector2>();
            _moving = _move.x != 0 || _move.y != 0;
        }

        public void OnLook(InputValue value)
        {
            _look = value.Get<Vector2>();
        }

        public void OnAim(InputValue value)
        {
            if (WeaponManager.currentWeapon == null) 
                return;
            aimValue = value.Get<float>();
        }

        public void OnFire(InputValue value)
        {
            if (!_aiming) 
                return;
            WeaponManager.currentWeapon.Fire();
            //GameEvents.Fire();
        }
        public void OnReload(InputValue value)
        {
            //доавить сюда проверку на перезаряжаемся ли мы прямо сейчас?
            if (!_aiming) 
                return;
            WeaponManager.currentWeapon.Reload();
        }
        public void OnSprint(InputValue value)
        {
            if (_aiming) return;
            _sprintValue = value.Get<float>();
            _sprinting = _sprintValue != 0;
        }

        public void OnCrouch(InputValue value)
        {
            if (_aiming) return;
            _crouchValue = value.Get<float>();
            _crouching = _crouchValue != 0;
        }


        public void OnChangeWeapon1(InputValue value)
        {
            if(value.isPressed)
            {
                GameInventory.instance.inventory.database.GetItem[1].Use();
                //WeaponManager.SwitchWeapon(WeaponManager.weapons[0]);
            }
            
        }

        public void OnChangeWeapon2(InputValue value)
        {
            if (value.isPressed)
            {
                WeaponManager.SwitchWeapon(WeaponManager.weapons[1]);
            }

        }

        public void OnInventory(InputValue value)
        {
            _inventValue = value.Get<float>();

            if (_inventValue > 0)
            {
                GameEvents.SwitchMap("UI");
            }
        }

        public GameObject followTransform;

        private void Update()
        {
            HandleAnimations();

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


            _nextRotation = Quaternion.Lerp(followTransform.transform.rotation, _nextRotation, Time.deltaTime * rotationLerp);


            if (aimValue == 1)
            {
                if (!_aiming)
                    GameEvents.Aim(true);


                SwitchCamera(true);
                //Set the player rotation based on the look transform
                playerTransform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                //reset the y rotation of the look transform
                followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

                _aiming = true;
            }
            else if (aimValue == 0)
            {
                if (_aiming)
                    GameEvents.Aim(false);

                SwitchCamera(false);
                _aiming = false;
            }

            Vector3 vertical = new Vector3(0f, Physics.gravity.y * Time.deltaTime, 0f);
            characterController.Move(vertical);

            if (_move.x == 0 && _move.y == 0)
            {
                _nextPosition = playerTransform.position;
                return;
            }

            float moveSpeed = speed / 100f;
            
            if (_sprintValue == 1)
            {
                moveSpeed = _sprintSpeed / 100f;
            }
            if (_crouchValue == 1)
            {
                moveSpeed = _crouchSpeed / 100f;
            }
            
            MoveCharacter(moveSpeed, angles);
            

        }

        private void MoveCharacter(float moveSpeed, Vector3 angles)
        {
            Vector3 position = (playerTransform.forward * _move.y * moveSpeed) + (playerTransform.right * _move.x * moveSpeed);
            characterController.Move(position);

            //Set the player rotation based on the look transform
            playerTransform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
           
            //reset the y rotation of the look transform
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }

        private void SwitchCamera(bool aim)
        {
            _aiming = aim;
            _camMove.gameObject.SetActive(!_aiming);
            _camAim.gameObject.SetActive(_aiming);
        }
        private void HandleAnimations()
        {
            playerAnimController.Sprinting(_sprinting);
            playerAnimController.Walking(_moving);
            playerAnimController.Aiming(_aiming);
            playerAnimController.Crouching(_crouching);
        }
        private void CharacterRotation()
        {
            Vector3 rotationPivot;
            rotationPivot.x = _move.x;
            rotationPivot.y = 0;
            rotationPivot.z = _move.y;

            Quaternion currentRotation = playerTransform.rotation;
            if(_moving)
            {
                Quaternion targetRotation = Quaternion.LookRotation(rotationPivot);
                playerTransform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 30f*Time.deltaTime);
            }

        }
    }

    
}
