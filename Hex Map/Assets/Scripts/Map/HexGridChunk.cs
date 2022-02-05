using UnityEngine;
using HexGridProject.Core;

namespace HexGridProject.Map
{
    public class HexGridChunk : MonoBehaviour
    {
        public HexMesh terrain;

        private HexCell[] _cells;
        private Canvas _gridCanvas;

        private static Color color1 = new Color(1f, 0f, 0f);
        private static Color color2 = new Color(0f, 1f, 0f);
        private static Color color3 = new Color(0f, 0f, 1f);

        private void Awake()
        {
            _gridCanvas = GetComponentInChildren<Canvas>();

            _cells = new HexCell[HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ];
        }

        private void LateUpdate()
        {
            Triangulate();
            enabled = false;
        }

        public void Refresh()
        {
            enabled = true;
        }

        public void AddCell(int index, HexCell cell)
        {
            _cells[index] = cell;
            cell.chunk = this;
            cell.transform.SetParent(transform, false);
            cell.uiRect.SetParent(_gridCanvas.transform, false);
        }

        public void ShowUI(bool visible)
        {
            _gridCanvas.gameObject.SetActive(visible);
        }

        public void Triangulate()
        {
            terrain.Clear();

            for (int i = 0; i < _cells.Length; i++)
            {
                Triangulate(_cells[i]);
            }

            terrain.Apply();
        }

        private void Triangulate(HexCell cell)
        {
            for (HexDirection direction = HexDirection.NorthEast; direction <= HexDirection.NorthWest; direction++)
            {
                Triangulate(direction, cell);
            }
        }

        private void Triangulate(HexDirection direction, HexCell cell)
        {
            Vector3 center = cell.Position;

            EdgeVertices edge = new EdgeVertices(center + HexMetrics.GetFirstSolidCorner(direction), center + HexMetrics.GetSecondSolidCorner(direction));

            if (cell.CellType == HexCellType.Mountain)
                center.y += HexMetrics.additionalMountainPickElevation;

            TriangulateEdgeFan(center, edge, cell.TerrainTypeIndex);

            if (direction <= HexDirection.SouthEast)
            {
                TriangulateConnection(direction, cell, edge);
            }
        }

        private void TriangulateEdgeFan(Vector3 center, EdgeVertices edge, float type)
        {
            terrain.AddTriangle(center, edge.vertix1, edge.vertix2);
            terrain.AddTriangle(center, edge.vertix2, edge.vertix3);
            terrain.AddTriangle(center, edge.vertix3, edge.vertix4);
            terrain.AddTriangle(center, edge.vertix4, edge.vertix5);

            terrain.AddTriangleColor(color1);
            terrain.AddTriangleColor(color1);
            terrain.AddTriangleColor(color1);
            terrain.AddTriangleColor(color1);

            Vector3 types;
            types.x = types.y = types.z = type;
            terrain.AddTriangleTerrainTypes(types);
            terrain.AddTriangleTerrainTypes(types);
            terrain.AddTriangleTerrainTypes(types);
            terrain.AddTriangleTerrainTypes(types);
        }

        private void TriangulateEdgeStrip(
            EdgeVertices edge1, Color color1, float type1,
            EdgeVertices edge2, Color color2, float type2)
        {
            terrain.AddQuad(edge1.vertix1, edge1.vertix2, edge2.vertix1, edge2.vertix2);
            terrain.AddQuad(edge1.vertix2, edge1.vertix3, edge2.vertix2, edge2.vertix3);
            terrain.AddQuad(edge1.vertix3, edge1.vertix4, edge2.vertix3, edge2.vertix4);
            terrain.AddQuad(edge1.vertix4, edge1.vertix5, edge2.vertix4, edge2.vertix5);

            terrain.AddQuadColor(color1, color2);
            terrain.AddQuadColor(color1, color2);
            terrain.AddQuadColor(color1, color2);
            terrain.AddQuadColor(color1, color2);

            Vector3 types;
            types.x = types.z = type1;
            types.y = type2;
            terrain.AddQuadTerrainTypes(types);
            terrain.AddQuadTerrainTypes(types);
            terrain.AddQuadTerrainTypes(types);
            terrain.AddQuadTerrainTypes(types);
        }

        private void TriangulateConnection(HexDirection direction, HexCell cell, EdgeVertices edge1)
        {
            HexCell neighbor = cell.GetNeighbor(direction);
            if (neighbor == null)
            {
                return;
            }

            Vector3 bridge = HexMetrics.GetBridge(direction);
            bridge.y = neighbor.Position.y - cell.Position.y;
            EdgeVertices edge2 = new EdgeVertices(
                edge1.vertix1 + bridge,
                edge1.vertix5 + bridge
            );

            TriangulateEdgeStrip(edge1, color1, cell.TerrainTypeIndex, edge2, color2, neighbor.TerrainTypeIndex);

            HexCell nextNeighbor = cell.GetNeighbor(direction.Next());
            if (direction <= HexDirection.East && nextNeighbor != null)
            {
                Vector3 vertix5 = edge1.vertix5 + HexMetrics.GetBridge(direction.Next());
                vertix5.y = nextNeighbor.Position.y;

                if (cell.Elevation <= neighbor.Elevation)
                {
                    if (cell.Elevation <= nextNeighbor.Elevation)
                    {
                        TriangulateCorner(edge1.vertix5, cell, edge2.vertix5, neighbor, vertix5, nextNeighbor);
                    }
                    else
                    {
                        TriangulateCorner(vertix5, nextNeighbor, edge1.vertix5, cell, edge2.vertix5, neighbor);
                    }
                }
                else if (neighbor.Elevation <= nextNeighbor.Elevation)
                {
                    TriangulateCorner(edge2.vertix5, neighbor, vertix5, nextNeighbor, edge1.vertix5, cell);
                }
                else
                {
                    TriangulateCorner(vertix5, nextNeighbor, edge1.vertix5, cell, edge2.vertix5, neighbor);
                }
            }
        }

        private void TriangulateCorner(
            Vector3 bottom, HexCell bottomCell,
            Vector3 left, HexCell leftCell,
            Vector3 right, HexCell rightCell
        )
        {
            terrain.AddTriangle(bottom, left, right);
            terrain.AddTriangleColor(color1, color2, color3);
            Vector3 types;
            types.x = bottomCell.TerrainTypeIndex;
            types.y = leftCell.TerrainTypeIndex;
            types.z = rightCell.TerrainTypeIndex;
            terrain.AddTriangleTerrainTypes(types);
        }
    }
}
