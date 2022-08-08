using System.Collections;
using UnityEngine;
using Helper;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera cam = default;
    [SerializeField] private float aimDeadZone = default;

    public Vector2 movementInput { get; private set; }
    public Vector2 mouseDirection { get; private set; }

    void Update()
    {
        if (UIManager.IsPaused) return;

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementInput = input.normalized;

        Vector2 transformPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = cam.ScreenToWorldPoint(mousePosition);
        if(!VectorFunctions.SqrDistanceSmaller(worldPosition, transformPosition, aimDeadZone))
            mouseDirection = VectorFunctions.VectorFromAtoB(transformPosition, worldPosition);
    }
}