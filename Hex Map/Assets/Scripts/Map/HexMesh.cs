using System;
using System.Collections.Generic;
using UnityEngine;
using HexGridProject.Core;

namespace HexGridProject.Map 
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        private Mesh _hexMesh;
        private MeshCollider _meshCollider;

        [NonSerialized] private List<Vector3> _vertices;
        [NonSerialized] private List<Vector3> _terrainTypes;
        [NonSerialized] private List<Color> _colors;
        [NonSerialized] private List<int> _triangles;

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = _hexMesh = new Mesh();

            _meshCollider = gameObject.AddComponent<MeshCollider>();
            _hexMesh.name = "Hex Mesh";
        }

        public void Clear()
        {
            _hexMesh.Clear();
            _vertices = ListPool<Vector3>.Get();
            _colors = ListPool<Color>.Get();
            _terrainTypes = ListPool<Vector3>.Get();
            _triangles = ListPool<int>.Get();
        }

        public void Apply()
        {
            _hexMesh.SetVertices(_vertices);
            ListPool<Vector3>.Add(_vertices);

            _hexMesh.SetColors(_colors);
            ListPool<Color>.Add(_colors);

            _hexMesh.SetUVs(2, _terrainTypes);
            ListPool<Vector3>.Add(_terrainTypes);

            _hexMesh.SetTriangles(_triangles, 0);
            ListPool<int>.Add(_triangles);

            _meshCollider.sharedMesh = _hexMesh;

            _hexMesh.RecalculateNormals();
        }

        public void AddTriangleColor(Color color)
        {
            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);
        }

        public void AddTriangleColor(Color color1, Color color2, Color color3)
        {
            _colors.Add(color1);
            _colors.Add(color2);
            _colors.Add(color3);
        }

        public void AddTriangle(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3)
        {
            int vertexIndex = _vertices.Count;
            _vertices.Add(vertex1);
            _vertices.Add(vertex2);
            _vertices.Add(vertex3);
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
        }

        public void AddTriangleTerrainTypes(Vector3 types)
        {
            _terrainTypes.Add(types);
            _terrainTypes.Add(types);
            _terrainTypes.Add(types);
        }

        public void AddQuad(Vector3 vertix1, Vector3 vertix2, Vector3 vertix3, Vector3 vertix4)
        {
            int vertexIndex = _vertices.Count;
            _vertices.Add(vertix1);
            _vertices.Add(vertix2);
            _vertices.Add(vertix3);
            _vertices.Add(vertix4);
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 2);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
            _triangles.Add(vertexIndex + 3);
        }

        public void AddQuadColor(Color color1, Color color2)
        {
            _colors.Add(color1);
            _colors.Add(color1);
            _colors.Add(color2);
            _colors.Add(color2);
        }

        public void AddQuadTerrainTypes(Vector3 types)
        {
            _terrainTypes.Add(types);
            _terrainTypes.Add(types);
            _terrainTypes.Add(types);
            _terrainTypes.Add(types);
        }
    }
}
