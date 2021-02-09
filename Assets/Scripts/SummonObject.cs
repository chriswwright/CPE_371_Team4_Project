using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonObject : MonoBehaviour
{
    public GameObject block;
    public Material transparent_mats;
    public float weight;
    public float offset = 1f;
    public float scale = 1f;
    public void Summon()
    {
        if (block != null)
        {
            GameObject new_block = Instantiate(block);
            new_block.transform.position = Vector3.zero;
            MeshFilter[] mfs = new_block.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combs = new CombineInstance[mfs.Length];
            int i = 0;
            foreach(MeshFilter mf in mfs)
            {
                combs[i].mesh = mf.mesh;
                combs[i].transform = mf.transform.localToWorldMatrix;
                i++;
            }
            Mesh combined_mesh = new Mesh();
            combined_mesh.CombineMeshes(combs);
            MeshFilter meshF = new_block.AddComponent<MeshFilter>();
            meshF.mesh = combined_mesh;
            MeshCollider mc = new_block.AddComponent<MeshCollider>();
            mc.convex = true;
            Rigidbody rb = new_block.AddComponent<Rigidbody>();
            new_block.tag = "Block";
            rb.useGravity = false;
            rb.mass = weight;
            rb.drag = 10;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            new_block.transform.localScale = new_block.transform.localScale * scale;
            new_block.transform.position = transform.position + (transform.forward * offset);
            new_block.transform.Rotate(new Vector3(1, 0, 0), 90);

        }

    }
}
