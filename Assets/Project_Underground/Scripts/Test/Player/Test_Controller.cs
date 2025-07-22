using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Test_Controller : MonoBehaviour
{
    [SerializeField] private InputSystem_Actions _actions;

    [SerializeField] private Vector2 _linearVelocity;
    [SerializeField] private Vector2 _maxLinearVelocity;
    [SerializeField] private float _angularVelocity;
    [SerializeField] private float _maxAngularVelocity;

    private InputAction _rotate;
    private InputAction _move;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _actions = new InputSystem_Actions();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rotate = _actions.Player.Rotate;
        _move = _actions.Player.Move;

        _rotate.Enable();
        _move.Enable();
    }

    private void OnDisable()
    {
        _rotate.Disable();
        _move.Disable();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        float rotationInput = _rotate.ReadValue<Vector2>().x;

        if (rotationInput != 0f)
            Rotate(rotationInput > 0f ? 1 : -1);
    }

    private void Rotate(int mod)
    {
        if (-1 * mod * _rb.angularVelocity < _maxAngularVelocity)
        {
            _rb.AddTorque(
                -1 * mod * _angularVelocity * (_maxAngularVelocity - Mathf.Abs(_rb.angularVelocity)) /
                _maxAngularVelocity * Mathf.Deg2Rad *
                _rb.inertia,
                ForceMode2D.Impulse);
        }
    }

    private void HandleMovement()
    {
        Vector2 movementInput = _move.ReadValue<Vector2>();

        if (movementInput.x != 0 || movementInput.y != 0)
            Move(movementInput);
    }

    private void Move(Vector2 direction)
    {
        int modX = direction.x > 0 ? 1 : -1;
        int modY = direction.y > 0 ? 1 : -1;

        Vector2 relativeVelocity = RotateVector(_rb.linearVelocity, transform.rotation.z);
        if (direction.x != 0 && modX * relativeVelocity.x < _maxLinearVelocity.x)
        {
            _rb.AddForce(
                modX * transform.right * _linearVelocity.x * (_maxLinearVelocity.x - Mathf.Abs(relativeVelocity.x)) /
                _maxLinearVelocity.x, ForceMode2D.Impulse);
        }

        if (direction.y != 0 && modY * relativeVelocity.y < _maxLinearVelocity.y)
        {
            _rb.AddForce(transform.up * modY * _linearVelocity, ForceMode2D.Impulse);

            _rb.AddForce(
                modY * transform.up * _linearVelocity.y * (_maxLinearVelocity.y - Mathf.Abs(relativeVelocity.y)) /
                _maxLinearVelocity.y, ForceMode2D.Impulse);
        }
    }

    public static Vector2 RotateVector(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
