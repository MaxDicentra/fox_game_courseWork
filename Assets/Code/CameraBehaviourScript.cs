using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class CameraBehaviourScript : MonoBehaviour
{
    private float dampTime = 0.15f;
    [SerializeField] Transform target = default;
    [SerializeField] Camera camera = default;
    private float offset = 0.75f;
    private float coordX = 0.5f;
    private float coordY = 0.5f;
    Vector3 zero = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(new Vector3(target.position.x, target.position.y + offset, target.position.z));
            Vector3 delta = new Vector3(target.position.x, target.position.y + offset, target.position.z) - camera.ViewportToWorldPoint(new Vector3(coordX, coordY, point.z));
            Vector3 destination = transform.position + delta;


            transform.position = Vector3.SmoothDamp(transform.position, destination, ref zero, dampTime);
        }

    }
}