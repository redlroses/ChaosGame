using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        //Get Fract Camera
        mainCamera = Camera.main;
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0.2f, 9.8f), Mathf.Clamp(transform.position.y, 0.2f, 9.8f));
    }
}
