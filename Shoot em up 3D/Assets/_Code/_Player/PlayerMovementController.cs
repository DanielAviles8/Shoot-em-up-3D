using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] private InputActionsHolder inputActionsHolder;

    private GameInputActions _inputActions;
    private CharacterController _characterController;

    [Header("Movement Options")]
    [SerializeField] private float _moveSpeed = 5f;
    
    private Vector2 _inputVector;
    private Vector3 _mouseDirection;

    private void OnDestroy()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Prepare();
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraBehaviour._inCinematic || CameraBehaviour._inMenu)
            return;

        _inputVector = _inputActions.Player.Movement.ReadValue<Vector2>();
        _mouseDirection = _inputActions.Player.FaceTo.ReadValue<Vector2>();

        MovePLayer();
    }

    void FixedUpdate()
    {
        if (CameraBehaviour._inCinematic || CameraBehaviour._inMenu)
            return;

        PlayerFacingTo();
    }
    private void Prepare()
    {
        _characterController = GetComponent<CharacterController>();
        _inputActions = inputActionsHolder._GameInputActions;
    }
    private void MovePLayer()
    {
        Vector3 horizontalVelocity = (Vector3.right * _inputVector.x + Vector3.forward * _inputVector.y) * _moveSpeed;
        _characterController.Move(horizontalVelocity * Time.deltaTime);

        Vector3 fixedPosition = transform.position;
        fixedPosition.y = 0.79f;
        transform.position = fixedPosition;
    }
    void PlayerFacingTo()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mouseDirection);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            LookAt(point);
        }
    }
    void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectoPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectoPoint);
    }
}
