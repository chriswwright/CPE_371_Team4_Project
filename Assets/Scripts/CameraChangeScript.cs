using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeScript : MonoBehaviour
{
    [SerializeField] private GameObject tablet;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CompleteDrawing()
    {
        animator.SetBool("Drawing", !animator.GetBool("Drawing"));
        tablet.SetActive(!tablet.activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            animator.SetBool("Drawing", !animator.GetBool("Drawing"));
            tablet.SetActive(!tablet.activeSelf);
        }
    }
}
