using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraChangeScript : MonoBehaviour
{
    [SerializeField] private GameObject tablet;
    private Animator animator;
    [SerializeField] private Volume postProcessing;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CompleteDrawing()
    {
        animator.SetBool("Drawing", !animator.GetBool("Drawing"));
        tablet.SetActive(!tablet.activeSelf);
        postProcessing.enabled = !postProcessing.enabled;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            animator.SetBool("Drawing", !animator.GetBool("Drawing"));
            tablet.SetActive(!tablet.activeSelf);
            postProcessing.enabled = !postProcessing.enabled;

        }
    }
}
