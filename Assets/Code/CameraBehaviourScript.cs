using UnityEngine;
using System.Collections;

public class CameraBehaviourScript : MonoBehaviour
{
    private float dampTime = 0.15f;
    [SerializeField] Transform target = default;
    [SerializeField] Camera camera = default;
    private float offset = 0.75f;


    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(new Vector3(target.position.x, target.position.y + offset, target.position.z));
            Vector3 delta = new Vector3(target.position.x, target.position.y + offset, target.position.z) - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;


            transform.position = Vector3.SmoothDamp(transform.position, destination, ref Vector3.zero, dampTime);
        }

    }
}