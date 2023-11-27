using FragileReflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaminaMovement : MonoBehaviour
{
    [SerializeField] private RectTransform _objectToMove;
    [SerializeField] private RectTransform _targetPosition;
    private float _moveSpeed = 5f;

    private bool _isMoving = false;

    private void Start()
    {
        GameEvents.onStaminaUsed += StartMoving;
        GameEvents.onStaminaRegenerated += StopMoving;
    }

    private void OnDestroy()
    {
        GameEvents.onStaminaUsed -= StartMoving;
        GameEvents.onStaminaRegenerated -= StopMoving;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _objectToMove.position = Vector3.Lerp(_objectToMove.position, _targetPosition.position, Time.deltaTime * _moveSpeed);
        }
    }

    private void StartMoving(float ammount)
    {
        _isMoving = true;
    }

    private void StopMoving()
    {
        _isMoving = false;
    }
}
