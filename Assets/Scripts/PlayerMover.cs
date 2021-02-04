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

 /*   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("BONK");
            Vector3 direction = transform.position - collision.transform.position;
            direction.Normalize();
            collision.rigidbody.MovePosition(direction * speed + collision.transform.position);
        }
    }*/

}
