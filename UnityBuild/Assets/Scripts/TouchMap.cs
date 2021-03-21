using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TouchMap : MonoBehaviour
{
    /*private Vector3 torchStart;
    private float MizZ;
    private float MaxZ;
    private float sensitivity;*/
    private Camera _camera;
    public float speed;
    private Vector2 startPos;
    private float targetPos;
    private float targetPos2;


    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        var position = _camera.transform.position;
        if(Input.GetMouseButtonDown(0)) startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        else if (Input.GetMouseButton(0))
        {
            float pos = _camera.ScreenToWorldPoint(Input.mousePosition).x - startPos.x;
            targetPos = Mathf.Clamp(position.x - pos, -26f, 26f);
            float pos2 = _camera.ScreenToWorldPoint(Input.mousePosition).y - startPos.y;
            targetPos2 = Mathf.Clamp(position.y - pos2, -33f, 33f);
        }

        
        transform.position = new Vector3(Mathf.Lerp(position.x, targetPos, speed * Time.deltaTime), Mathf.Lerp(position.y, targetPos2, speed * Time.deltaTime), position.z);
        
        /*if(Input.touchCount == 2 || Input.touchCount == 1)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
        }

        if (!Input.GetMouseButtonDown(0)) return;
        if (_camera is { })
        {
            Vector3 direction = torchStart - _camera.ScreenToWorldPoint(Input.mousePosition);
            _camera.transform.position += direction;
        }*/
    }
}
