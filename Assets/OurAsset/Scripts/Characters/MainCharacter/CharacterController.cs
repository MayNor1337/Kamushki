using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    private Rigidbody _characterRb;
    private PlayerInput _playerInput;
    private bool _groundedPlayer;
    private Vector3 _playerVelocity;
    private Vector2 moveDirection;
    private void Start()
    {
        _characterRb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        _playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Jump.performed += Jump;
    }
    private void OnDisable()
    {
        _playerInput.Player.Jump.performed -= Jump;
        _playerInput.Disable();
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector2 moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
        _characterRb.velocity = new Vector3(moveDirection.x * _moveSpeed, _characterRb.velocity.y, moveDirection.y * _moveSpeed);
    }
    void Jump(InputAction.CallbackContext ctx)
    {
        if(_groundedPlayer)
            _characterRb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground") _groundedPlayer = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") _groundedPlayer = false;
    }
}
