using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFromLine : MonoBehaviour
{
    private Camera draw_camera;
    public float line_width = 1f;

    public float max_draw_distance = 19.99f;

    public Material line_material;
    public GameObject prefab;

    public GameObject player;
    private GameObject line_display;
    private GameObject placeholder;
    public GameObject tablet;

    public bool isMouseDown = false;
    public List<Vector3> line_points = new List<Vector3>();
    void Start()
    {
        draw_camera = Camera.main;
        line_display = Instantiate(prefab);
        var lr = line_display.AddComponent<LineRenderer>();
        lr.positionCount = 0;
        lr.material = line_material;
        lr.widthMultiplier = line_width;
        MeshFilter mf = line_display.AddComponent<MeshFilter>();
        mf.mesh = new Mesh();
        MeshRenderer mr = line_display.AddComponent<MeshRenderer>();
        mr.material = line_material;
        player.GetComponent<SummonObject>().block = line_display;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && !isMouseDown)
        {
            LineRenderer lr = line_display.GetComponent<LineRenderer>();
            lr.positionCount = 0;
            isMouseDown = true;
        }
        if (isMouseDown)
        {
            Vector3 point_to_add = GetMouseCameraPoint();
            if (PointWithinBounds(point_to_add))
            {
                line_points.Add(point_to_add);
                line_display.GetComponent<LineRenderer>().positionCount = line_points.Count;
                line_display.GetComponent<LineRenderer>().SetPosition(line_points.Count - 1, line_points[line_points.Count - 1]);
            }
            else
            {
                isMouseDown = false;
            }

        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(0) && !isMouseDown)
        {
            line_display.transform.position = Vector3.zero;
            Mesh temp_mesh = new Mesh();
            LineRenderer lr = line_display.GetComponent<LineRenderer>();
            for (int i = 0; i < lr.positionCount; ++i)
            {
                lr.SetPosition(i, lr.GetPosition(i) - tablet.transform.position);
            }
            lr.BakeMesh(temp_mesh, true);
            temp_mesh.RecalculateNormals();
/*            Vector3[] temp_verticies = new Vector3[temp_mesh.vertexCount];
            for (int i = 0; i < temp_mesh.vertexCount; ++i)
            {
                  temp_verticies[i] = temp_mesh.vertices[i] - tablet.transform.position;
    
            }
*/
//            temp_mesh.SetVertices(temp_verticies);
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
            line_display.transform.position = tablet.transform.position;
        }

    }
    private bool PointWithinBounds(Vector3 check_point)
    {
        if (tablet.GetComponent<MeshCollider>().bounds.Contains(new Vector3(check_point.x, check_point.y, tablet.transform.position.z)))
        {
            return true;
        }
        return false;
    }

    private Vector3 GetMouseCameraPoint()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = max_draw_distance;
        return draw_camera.ScreenToWorldPoint(mousePos);
    }
}