using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectFromLine : MonoBehaviour
{
    [System.Serializable]   
    public enum inkEnum{
        BlackInk,
        RedInk,
        LightBlueInk,
        numTypes
    }

    private Camera draw_camera;
    public float line_width = 1f;

    public float max_draw_distance = 19.99f;
    public float ink_scalar = 1f;
    public List<Material> line_materials;
    public List<bool> objects_needed;
    public float weight_scalar = 0.1f;
    public int line_selector = 0;
    public List<GameObject> inkHolders;
    public GameObject prefab;
    public GameObject player;
    private GameObject line_display;
    private GameObject placeholder;
    public GameObject tablet;
    public List<GameObject> percentages;
    public List<float> ink_remaining;
    public List<float> max_ink;
    public bool isMouseDown = false;
    public List<Vector3> line_points = new List<Vector3>();
    void Start()
    {
        draw_camera = Camera.main;
        line_display = Instantiate(new GameObject());
    }

    public void SetBlack()
    {
        line_selector = (int)inkEnum.BlackInk;
    }
    public void SetRed()
    {
        line_selector = (int)inkEnum.RedInk;
    }

    public void SetLightBlue()
    {
        line_selector = (int)inkEnum.LightBlueInk;
    }

    public void UpdatePercentages()
    {
        int i = 0;
        foreach(GameObject ink_text in percentages)
        {
            ink_text.GetComponent<TextMeshProUGUI>().text = string.Format("{0:0.#}",(100 * ink_remaining[i] / max_ink[i])) + "%";
            i++;
        }
    }

    public void ResetTablet()
    {
        for(int i = 0; i < (int)inkEnum.numTypes; i++)
        {
            GameObject target = inkHolders[i];
            inkHolders[i] = null;
            Destroy(target);
            ink_remaining[i] = max_ink[i];
            objects_needed[i] = false;
        }
    }

    public float GetWeight()
    {
        float weight = 0;
        int i = 0;
        foreach (float ink_remain in ink_remaining)
        {
            weight = max_ink[i] - ink_remain;
            i++;
        }
        return weight * weight_scalar;
    }

    public void SetWidth(float width)
    {
       
        line_width = .1f + (.9f * width);
        foreach(GameObject inkHolder in inkHolders)
        {
            if (inkHolder != null)
            {
                inkHolder.GetComponent<LineRenderer>().widthMultiplier = line_width;
            }
        }
    }
    void Update()
    {
        if (!objects_needed[line_selector])
        {
            objects_needed[line_selector] = true;
            inkHolders[line_selector] = Instantiate(prefab);
            inkHolders[line_selector].transform.parent = line_display.transform;
            inkHolders[line_selector].transform.localPosition = Vector3.zero;
            LineRenderer lineR = inkHolders[line_selector].AddComponent<LineRenderer>();
            lineR.positionCount = 0;
            lineR.material = line_materials[line_selector];
            lineR.widthMultiplier = line_width;
            MeshFilter mf = inkHolders[line_selector].AddComponent<MeshFilter>();
            mf.mesh = new Mesh();
            MeshRenderer mr = inkHolders[line_selector].AddComponent<MeshRenderer>();
            mr.material = line_materials[line_selector];
            player.GetComponent<SummonObject>().block = line_display;

        }
        if (Input.GetMouseButtonDown(0) && ink_remaining[line_selector] > 0 || Input.GetMouseButton(0) && !isMouseDown && ink_remaining[line_selector] > 0)
        {
            LineRenderer lr = inkHolders[line_selector].GetComponent<LineRenderer>();
            lr.material = line_materials[line_selector];
            isMouseDown = true;
        }
        if (isMouseDown)
        {
            Vector3 point_to_add = GetMouseCameraPoint();
            if (PointWithinBounds(point_to_add) && ink_remaining[line_selector] > 0)
            {
                line_points.Add(point_to_add);
                inkHolders[line_selector].GetComponent<LineRenderer>().positionCount = line_points.Count;
                inkHolders[line_selector].GetComponent<LineRenderer>().SetPosition(line_points.Count - 1, line_points[line_points.Count - 1]);
                if (line_points.Count > 1)
                {
                    Vector3 result = (line_points[line_points.Count - 1] - line_points[line_points.Count - 2]);
                    ink_remaining[line_selector] -= result.magnitude * ink_scalar * line_width;
                }
                if (ink_remaining[line_selector] < 0)
                {
                    ink_remaining[line_selector] = 0;
                }
            }
            else
            {
                isMouseDown = false;
                if (ink_remaining[line_selector] < 0)
                {
                    ink_remaining[line_selector] = 0;
                }

            }

        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(0) && !isMouseDown || Input.GetMouseButton(0) && ink_remaining[line_selector] <= 0)
        {
            line_display.transform.position = Vector3.zero;
            Mesh temp_mesh = new Mesh();
            LineRenderer lr = inkHolders[line_selector].GetComponent<LineRenderer>();
            lr.alignment = LineAlignment.TransformZ;
            for (int i = 0; i < lr.positionCount; ++i)
            {
                lr.SetPosition(i, lr.GetPosition(i) - tablet.transform.position);
            }
            lr.BakeMesh(temp_mesh, Camera.main, false);
            temp_mesh.RecalculateNormals();
            placeholder = Instantiate(prefab);
            MeshFilter mf = placeholder.AddComponent<MeshFilter>();
            mf.mesh = temp_mesh;
            CombineInstance[] mesh_array = new CombineInstance[2];
            mesh_array[0] = new CombineInstance();
            mesh_array[0].mesh = temp_mesh;
            mesh_array[0].transform = placeholder.GetComponent<MeshFilter>().transform.localToWorldMatrix;
            mesh_array[1] = new CombineInstance();
            mesh_array[1].mesh = inkHolders[line_selector].GetComponent<MeshFilter>().mesh;
            mesh_array[1].transform = inkHolders[line_selector].GetComponent<MeshFilter>().transform.localToWorldMatrix;
            Mesh combo_mesh = new Mesh();
            combo_mesh.CombineMeshes(mesh_array);
            inkHolders[line_selector].GetComponent<MeshFilter>().mesh = combo_mesh;
            Destroy(placeholder);
            
            line_points = new List<Vector3>();
            isMouseDown = false;
            lr.positionCount = 0;
            line_display.transform.position = tablet.transform.position;

        }
        player.GetComponent<SummonObject>().weight = GetWeight();
        UpdatePercentages();
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
