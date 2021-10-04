using UnityEngine;

public class MouseLock : MonoBehaviour
{
    private void Awake()
    {
        //Cursor.visible = false;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
