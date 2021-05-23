using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 0.7f;
    public GameObject pivot;
    public float minY = 0.8f;
    public float maxY = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 prePos = pivot.transform.position;
        float scroll = Input.mouseScrollDelta.y * -1;
        pivot.transform.position += pivot.transform.forward * scroll * zoomSpeed;

        if (pivot.transform.position.y < minY || pivot.transform.position.y > maxY)
        {
            pivot.transform.position = prePos;
        }
    }
}
