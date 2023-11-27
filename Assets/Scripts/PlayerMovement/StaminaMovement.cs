using FragileReflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaminaMovement : MonoBehaviour
{
    [SerializeField] private RectTransform _objectToMove;
    [SerializeField] private RectTransform _targetPositionEnd;
    [SerializeField] private RectTransform _targetPositionBegin;

    private bool _isMoving = false;
    private float _stamina;

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
        float interpFactor = 1 - (_stamina / 100f);
        if (_isMoving)
        {
            _objectToMove.position = Vector3.Lerp(_targetPositionBegin.position, _targetPositionEnd.position, interpFactor);

            if (_stamina <= 0f)
            {
                _objectToMove.position = _targetPositionEnd.position;
            }
        }
        else
        {
            _objectToMove.position = Vector3.Lerp(_targetPositionBegin.position, _targetPositionEnd.position, interpFactor);
        }
    }

    private void StartMoving(float amount)
    {
        _stamina = amount;
        _isMoving = true;
    }

    private void StopMoving(float amount)
    {
        _stamina = amount;
        _isMoving = false;
    }
}
