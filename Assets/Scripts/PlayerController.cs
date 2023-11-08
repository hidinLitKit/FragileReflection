using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace FragileReflection
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _transformPlayer;
        [SerializeField] private Character m_character;
        [SerializeField] private InputActionAsset m_inputActionAsset;
        [SerializeField] private Transform m_cameraTransform;
        [SerializeField] private float m_speedRotation = 200f;
        [SerializeField] private float m_topClamp = 70f;
        [SerializeField] private float m_bottomClamp = -9f;


        private float m_cameraTargetYaw;
        private float m_cameraTargetPitch;

        //Input
        private InputActionMap m_playerMap;
        private InputAction m_moveAction;
        private InputAction m_lookAction;
        private InputAction m_fireAction;

        private Vector2 move;
        private Vector2 look;

        private bool _zoomed = false;


        private void Awake()
        {
            m_playerMap = m_inputActionAsset.FindActionMap("Player");
            m_moveAction = m_playerMap.FindAction("Move");
            m_lookAction = m_playerMap.FindAction("Look");
            m_fireAction = m_playerMap.FindAction("Fire");
        }

        private void OnEnable()
        {
            m_playerMap.Enable();

            m_fireAction.performed += OnFireInput;
        }


        private void OnDisable()
        {
            m_playerMap.Disable();

            m_fireAction.performed -= OnFireInput;
        }

        private void OnFireInput(InputAction.CallbackContext context)
        {
            Debug.Log("Try fire!");
        }

        private void Update()
        {
            move = m_moveAction.ReadValue<Vector2>();
            look = m_lookAction.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Move(move, false);
        }

        private void LateUpdate()
        {
            CameraRotation(look);
        }

        private void Move(Vector2 move, bool isSprint)
        {
            m_character.Move(move, isSprint, _transformPlayer.eulerAngles.y);
        }

        private void CameraRotation(Vector2 look)
        {
            const float threshold = 0.01f;

            if (look.sqrMagnitude >= threshold)
            {
                float deltaTimeMultiplier = m_speedRotation * Time.deltaTime;

                m_cameraTargetYaw += look.x * deltaTimeMultiplier;
                m_cameraTargetPitch += look.y * deltaTimeMultiplier;
            }

            m_cameraTargetYaw = ClampAngle(m_cameraTargetYaw, float.MinValue, float.MaxValue);

            m_cameraTargetPitch = ClampAngle(m_cameraTargetPitch, m_bottomClamp, m_topClamp);

            m_character.Look(Quaternion.Euler(0f, m_cameraTargetYaw, 0f));



            //if (rotate.sqrMagnitude < 0.01)
            //    return;

            //var scaledRotateSpeed = m_speedRotation * Time.deltaTime;
            //look.y += rotate.x * scaledRotateSpeed;
            //if (!_zoomed)
            //{
            //    look.x = Mathf.Clamp(look.x - rotate.y * scaledRotateSpeed, -30, 10);
            //    _transformPlayer.localEulerAngles = look;
            //}
            //else
            //{
            //    look.x = Mathf.Clamp(look.x - rotate.y * scaledRotateSpeed, -20, 10);
            //    _transformPlayer.localEulerAngles = look;
            //}

        }

        private static float ClampAngle(float angle, float min, float max)
        {

            angle = angle < -360f ? 0f : (angle > 360f ? 0f : angle);

            return Mathf.Clamp(angle, min, max);
        }
    }
}
