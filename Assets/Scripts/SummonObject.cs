﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonObject : MonoBehaviour
{
    public GameObject block;
    public float offset = 1f;
    public float scale = 1f;
    public void Summon()
    {
        if (block != null)
        {
            GameObject new_block = Instantiate(block);
            new_block.AddComponent<MeshCollider>();
            Rigidbody rb = new_block.AddComponent<Rigidbody>();
            new_block.tag = "Block";
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            new_block.transform.localScale = new_block.transform.localScale * scale;
            new_block.transform.position = transform.position + (transform.forward * offset);
            new_block.transform.Rotate(new Vector3(1, 0, 0), 90);

        }

    }
}
