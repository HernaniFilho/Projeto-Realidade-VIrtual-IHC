using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    void Start()
    {
        // Travar e ocultar o cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
