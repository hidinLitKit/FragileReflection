using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using WeaponSystem;

namespace FragileReflection
{
    public class PlayerMovement : MonoBehaviour
    {
        //mine

        [Header("Контроллеры")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerAnimController playerAnimController;
        private Transform playerTransform;

        [Header("Значения состояний")]
        public Vector2 _move;
        public Vector2 _look;
        public bool aimValue;
        private bool _sprintValue;
        public float fireValue;
        public float walkValue;
        private float _crouchValue;
        private float _inventValue;

        private Vector3 _nextPosition;
        private Quaternion _nextRotation;

        public float rotationPower = 0.5f;
        public float rotationLerp = 0.5f;

        [Header("Движение")]
        public float speed = 1f;
        [SerializeField] private float _sprintSpeed = 2f;
        [SerializeField] private float _crouchSpeed = 0.5f;

        [Header("Настройки камеры")]
        [SerializeField] private CinemachineVirtualCamera _camMove;
        [SerializeField] private CinemachineVirtualCamera _camAim;

        [Header("Состояния")]
        private bool _aiming = false;
        private bool _moving = false;
        private bool _sprinting = false;
        private bool _crouching = false;

        [Header("Выносливость")]
        [SerializeField] private float stamina = 100f;  // начальное значение
        private float maxStamina = 100f;
        [SerializeField] private float staminaConsumptionRate = 5f;  // скорость расхода выносливости
        [SerializeField] private float staminaRegenerationRate = 2f; // скорость восстановления выносливости

        private bool isGamepad;
        private InputAction m_aim;
        private InputAction m_sprint;
        private void Awake()
        {
            
            playerTransform = characterController.gameObject.transform;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

#if UNITY_ANDROID
            rotationPower = 3f;
            isGamepad = true;
            m_aim = GetComponent<PlayerInput>().currentActionMap.FindAction("Aim");
            m_sprint = GetComponent<PlayerInput>().currentActionMap.FindAction("Sprint");

#endif

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
            if (WeaponManager.instance.currentWeapon == null)
                return;
            if (isGamepad)
            {
                if (m_aim.WasPerformedThisFrame()) aimValue = !aimValue;
            }
            else aimValue = value.Get<float>() != 0;
        }

        public void OnFire(InputValue value)
        {
            if (!_aiming)
                return;
            WeaponManager.instance.AttackPerform();
            //GameEvents.Fire();
        }
        public void OnReload(InputValue value)
        {
            //доавить сюда проверку на перезаряжаемся ли мы прямо сейчас?
            // хз а надо?
            if (!_aiming)
                return;
            WeaponManager.instance.ReloadPerfom();
        }
        public void MobileSprint(bool sprint)
        {
            _sprintValue = sprint;
            _sprinting = _sprintValue;
        }
        public void OnSprint(InputValue value)
        {
            /*if (_aiming || _move.y < 0 || !_moving)
            {
                _sprinting = false;
                _sprintValue = false;
                return;
            } */
            if (isGamepad)
            {
                if (m_sprint.WasPerformedThisFrame()) _sprintValue = !_sprintValue;
            }
            else _sprintValue = value.Get<float>() != 0;
            _sprinting = _sprintValue ;  
        }

        public void OnCrouch(InputValue value)
        {
            if (_aiming) 
                return;
            _crouchValue = value.Get<float>();
            _crouching = _crouchValue != 0;
        }
        public void OnSave(InputValue value)
        {
            DataPersistenceManager.instance.SaveGame();
        }
        public void OnLoad(InputValue inputValue)
        {
            DataPersistenceManager.instance.LoadGame();
        }
        public void OnNewGame(InputValue value)
        {
            DataPersistenceManager.instance.NewGame();
        }


        public void OnChangeWeapon1(InputValue value)
        {
            if(value.isPressed)
            {
                GameInventory.instance.inventory.database.Items[1].Use();
                //WeaponManager.SwitchWeapon(WeaponManager.weapons[0]);
            }
            
        }

        public void OnChangeWeapon2(InputValue value)
        {
            if (value.isPressed)
            {
                WeaponManager.instance.SwitchWeapon(WeaponManager.instance.weapons[1]);
            }

        }

        public void OnInventory(InputValue value)
        {
            GameEvents.SwitchMap("UI");
            GameEvents.InventoryUIAble(true);
            //if (value.isPressed)
            //{
                
            //}
        }

        private void OnParallel(InputValue value)
        {
            if(value.isPressed)
            {
                GameEvents.ActiveParallelWorld();
            }
        }

        private void OnPause(InputValue value)
        {
            GameEvents.SwitchMap("UI");
            GameEvents.GamePause(true);
            if (value.isPressed)
            {

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


            if (aimValue)
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
            else
            {
                if (_aiming)
                    GameEvents.Aim(false);

                SwitchCamera(false);
                _aiming = false;
            }

            Vector3 vertical = new Vector3(0f, Physics.gravity.y * Time.deltaTime, 0f);
            characterController.Move(vertical);
            
            preventSprint();
            
            if (_move.x == 0 && _move.y == 0)
            {
                _nextPosition = playerTransform.position;

                stamina = Mathf.Min(stamina + staminaRegenerationRate * Time.deltaTime, maxStamina);
                GameEvents.StaminaRegenerated(stamina);
                if (stamina == 100)
                    GameEvents.StaminaUIClose();

                return;
            }

            float moveSpeed = speed;
            
            if (_sprintValue && stamina > 0)
            {
                stamina -= staminaConsumptionRate * Time.deltaTime;
                GameEvents.StaminaUsed(stamina);
                GameEvents.StaminaUIOpen();

                moveSpeed = _sprintSpeed;
            }
            else
            { 
                if (_crouching) 
                    stamina = Mathf.Min(stamina + (staminaRegenerationRate * 1.5f) * Time.deltaTime, maxStamina);
                else 
                    stamina = Mathf.Min(stamina + staminaRegenerationRate * Time.deltaTime, maxStamina);
                GameEvents.StaminaRegenerated(stamina);
                if (stamina == 100)
                    GameEvents.StaminaUIClose();
            }

            if (_crouchValue == 1)
            {
                moveSpeed = _crouchSpeed;
            }
            MoveCharacter(moveSpeed, angles);
            

        }

        private void MoveCharacter(float moveSpeed, Vector3 angles)
        {
            Vector3 position = (playerTransform.forward * _move.y * moveSpeed) + (playerTransform.right * _move.x * moveSpeed);
            characterController.Move(position*Time.deltaTime);

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
            playerAnimController.WalkDir(_move.x, _move.y);
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
        private void preventSprint()
        {
            if (_aiming || _move.y < 0 || !_moving || stamina<=0)
            {
                _sprinting = false;
                _sprintValue = false;
                return;
            }
        }
    }

    
}
