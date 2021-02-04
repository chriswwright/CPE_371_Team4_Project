using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private CharacterController mover;
    [SerializeField] private float speed = 4;

    private void Start()
    {
        mover = GetComponent<CharacterController>();
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
            mover.SimpleMove(speed * direction);
        }
    }

    void OnCollisionEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Block"))
        {
            Vector3 direction = (transform.position - collider.transform.position);
            collider.transform.position += (direction.normalized * speed);
        }
    }

}
