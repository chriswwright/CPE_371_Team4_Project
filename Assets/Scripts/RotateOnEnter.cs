using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnEnter : MonoBehaviour
{
    public bool rotate;
    public float rotate_speed = 0.1f;
    private Quaternion start_rotation;
    // Start is called before the first frame update
    void Start()
    {
        start_rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.GetComponent<LineRenderer>().positionCount = 0;
            rotate = !rotate;
            if (rotate)
            {
                MeshCollider mc = gameObject.AddComponent<MeshCollider>();
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            else
            {
                Destroy(gameObject.GetComponent<MeshCollider>());
            }
        }

        if (rotate)
        {
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), rotate_speed);
        }
        else
        {
            transform.rotation = start_rotation;
        }
    }
}
