using UnityEngine;
using UnityEngine.InputSystem;

public class SliceTrail : MonoBehaviour
{
   [SerializeField] private TrailRenderer _trail;
    
    private void Update()
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 world = Camera.main.ScreenToWorldPoint(
            new Vector3(screen.x, screen.y, 10f)); // fixed depth in front of camera
        transform.position = world;
        _trail.emitting = Mouse.current.leftButton.isPressed;
    }

}
