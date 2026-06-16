using NUnit.Framework;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SliceInput : MonoBehaviour
{
    private Vector2 _previousPos;
    private bool _isSlicing;
    [SerializeField] private float _sphereRadius = 0.5f;
    [SerializeField] private BoolEventChannelSO _onGameOver;

    private InputSystem_Actions _inputSystem;
    private bool _isGameOver = false;

    private void Awake()
    {
        _inputSystem = new();
    }
    private void OnEnable()
    {
        _inputSystem.Enable();
        _inputSystem.UI.Click.performed += OnClick;
        _inputSystem.UI.Click.canceled += OnRelease;
        _onGameOver.OnEventRaise += OnGameOver;
    }

    private void OnDisable()
    {
        _inputSystem.UI.Click.performed -= OnClick;
        _inputSystem.UI.Click.canceled -= OnRelease;
        _onGameOver.OnEventRaise -= OnGameOver;
        _inputSystem.Disable();
    }

    private void Update()
    {
        if(_isGameOver)
        {
            return;
        }
        if(_isSlicing && _inputSystem.UI.Click.IsPressed())
        {
            Vector2 currentPos = _inputSystem.UI.Position.ReadValue<Vector2>();
            if(currentPos != _previousPos)
            {
                DetectSlide(_previousPos, currentPos);
                _previousPos = currentPos;
            }
        }
        else if (!_inputSystem.UI.Click.IsPressed() && _isSlicing)
        {
            // Fail-safe: Force turn off slicing if the input system missed a release frame
            _isSlicing = false;
        }
    }

    private void OnGameOver(bool isGameOver)
    {
        _isGameOver = isGameOver;
    }

    private void DetectSlide(Vector2 from, Vector2 to)
    {
        // Create rays pointing from the camera into the world
        Ray rayFrom = Camera.main.ScreenPointToRay(from);
        Ray rayTo = Camera.main.ScreenPointToRay(to);

        //  Define how far into the screen your targets are located
        float targetDepth = 10f;
        
        // Get the actual 3D points where the rays hit that specific depth plane
        Vector3 worldFrom = rayFrom.GetPoint(targetDepth);
        Vector3 worldTo = rayTo.GetPoint(targetDepth);

        // Calcualte direction and distance of the previous and current mouse position
        Vector3 direction = worldTo - worldFrom;
        float distance = direction.magnitude;

        if (distance < 0.01f) return;

        // Ray cast to check any hit in this slice
        RaycastHit[] hits = Physics.SphereCastAll(worldFrom, _sphereRadius, direction.normalized, distance);
        Debug.DrawLine(worldFrom, worldTo, Color.red, 1.0f);
        foreach(var hit in hits)
        {
            Target target = hit.collider.GetComponent<Target>();
            target?.OnSliced();
        }
    }

    private Vector3 GetPosition(Vector2 pos)
    {
        return Camera.main.ScreenToWorldPoint(
        new Vector3(pos.x, pos.y, Camera.main.nearClipPlane));
    }

    private void OnClick(InputAction.CallbackContext callback)
    {
        _isSlicing = true;
        _previousPos = _inputSystem.UI.Position.ReadValue<Vector2>();
    }

    private void OnRelease(InputAction.CallbackContext callback)
    {
        _isSlicing = false;
    }
}
