using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 4;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction.Normalize();
        if (direction.magnitude > 0)
        {
            transform.eulerAngles = Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
            rb.velocity = speed * direction;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Vision"))
        {
            Enemy spotter = collision.gameObject.GetComponentInParent<Enemy>();
            if(spotter != null)
            {
                spotter.Notice(transform);
            }

        }
    }

}
