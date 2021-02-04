using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFromLine : MonoBehaviour
{
    public Camera draw_camera;
    public float line_width = 1f;

    public Material line_material;
    public GameObject prefab;
    
    private GameObject line_display;
    private GameObject placeholder;
    public bool isMouseDown = false;
    public List<Vector3> line_points = new List<Vector3>();
    void Start()
    {
        line_display = Instantiate(prefab);
        var lr = line_display.AddComponent<LineRenderer>();
        lr.positionCount = 0;
        lr.material = line_material;
        lr.widthMultiplier = line_width;
        MeshFilter mf = line_display.AddComponent<MeshFilter>();
        mf.mesh = new Mesh();
        MeshRenderer mr = line_display.AddComponent<MeshRenderer>();
        mr.material = line_material;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LineRenderer lr = line_display.GetComponent<LineRenderer>();
            lr.positionCount = 0;
            isMouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Mesh temp_mesh = new Mesh();
            line_display.GetComponent<LineRenderer>().BakeMesh(temp_mesh, true);
            placeholder = Instantiate(prefab);
            MeshFilter mf = placeholder.AddComponent<MeshFilter>();
            mf.mesh = temp_mesh;
            CombineInstance[] mesh_array = new CombineInstance[2];
            mesh_array[0] = new CombineInstance();
            mesh_array[0].mesh = temp_mesh;
            mesh_array[0].transform = placeholder.GetComponent<MeshFilter>().transform.localToWorldMatrix;
            mesh_array[1] = new CombineInstance();
            mesh_array[1].mesh = line_display.GetComponent<MeshFilter>().mesh;
            mesh_array[1].transform = line_display.GetComponent<MeshFilter>().transform.localToWorldMatrix;
            Mesh combo_mesh = new Mesh();
            combo_mesh.CombineMeshes(mesh_array);
            line_display.GetComponent<MeshFilter>().mesh = combo_mesh;
            Destroy(placeholder);
            line_points = new List<Vector3>();
            isMouseDown = false;
        }
        if (isMouseDown)
        {
            line_points.Add(GetMouseCameraPoint());
            line_display.GetComponent<LineRenderer>().positionCount = line_points.Count;
            line_display.GetComponent<LineRenderer>().SetPosition(line_points.Count - 1, line_points[line_points.Count - 1]);

        }
    }

    private Vector3 GetMouseCameraPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        return draw_camera.ScreenToWorldPoint(mousePos);
    }
}
