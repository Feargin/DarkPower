using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMap : MonoBehaviour
{
    private Vector3 torchStart;
    private float MizZ;
    private float MaxZ;
    private float sensitivity;
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 direction = torchStart - GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            GetComponent<Camera>().transform.position += direction;
        }
    }
}
