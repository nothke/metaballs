using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Profiling;

public class Container : MonoBehaviour
{
    public float safeZone;
    public float resolution;
    public float threshold;
    public ComputeShader computeShader;
    public bool calculateNormals;

    private CubeGrid grid;

    // Mesh cache
    Mesh mesh;

    public void Start()
    {
        this.grid = new CubeGrid(this, this.computeShader);
        mesh = GetComponent<MeshFilter>().mesh;
    }

    public void Update()
    {
        Profiler.BeginSample("evaluateAll");
        this.grid.evaluateAll(this.GetComponentsInChildren<MetaBall>());
        Profiler.EndSample();

        Profiler.BeginSample("Mesh setting");
        mesh.Clear();

        mesh.SetVertices(grid.vertices);
        mesh.SetTriangles(grid.getTriangleList(), 0);

        if (this.calculateNormals)
        {
            mesh.RecalculateNormals();
        }
        Profiler.EndSample();
    }
}