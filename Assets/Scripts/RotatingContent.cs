using UnityEngine;

/// <summary>
/// Rotates object on drag
/// </summary>
public class RotatingContent : MonoBehaviour
{

    [SerializeField] float speed = 1;

    bool isPressed;
    float mouseStartPosition;

    // https://www.youtube.com/watch?v=wGxOkqiITpE
    void Update()
    {
        if (isPressed)
        {
            float currentMousePosition = Input.mousePosition.x;
            float movement = currentMousePosition - mouseStartPosition;

            transform.Rotate(Vector3.up, -movement * speed * Time.deltaTime);
            mouseStartPosition = currentMousePosition;
        }
    }

    private void OnMouseDown()
    {
        mouseStartPosition = Input.mousePosition.x;
        isPressed = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
    }
}
