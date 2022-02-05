using UnityEngine;
using HexGridProject.Core;

namespace HexGridProject.Map
{
    public class HexGrid : MonoBehaviour
    {
        public int cellCountX = 60;
        public int cellCountZ = 60;

        public int seed;
        public RectTransform cellLabelPrefab;
        public HexCell cellPrefab;
        public HexGridChunk chunkPrefab;

        private int currentCenterColumnIndex = -1;
        private int currentCenterRowIndex = -1;
        private int chunkCountX;
        private int chunkCountZ;
        private HexCell[] _cells;
        private HexGridChunk[] _chunks;

        private void Awake()
        {
            HexMetrics.InitializeHashGrid(seed);
            CreateMap(cellCountX, cellCountZ);
        }

        public bool CreateMap(int x, int z)
        {
            if (x <= 0 || x % HexMetrics.chunkSizeX != 0 || z <= 0 || z % HexMetrics.chunkSizeZ != 0 || cellCountZ % 2 != 0)
            {
                Debug.LogError("Unsupported map size.");
                return false;
            }

            if (_chunks != null)
            {
                for (int i = 0; i < _chunks.Length; i++)
                {
                    Destroy(_chunks[i].gameObject);
                }
            }

            cellCountX = x;
            cellCountZ = z;
            chunkCountX = cellCountX / HexMetrics.chunkSizeX;
            chunkCountZ = cellCountZ / HexMetrics.chunkSizeZ;
            currentCenterColumnIndex = -1;
            HexMetrics.wrapSizeX = cellCountX;
            HexMetrics.wrapSizeZ = cellCountZ;

            CreateChunks();
            CreateCells();

            return true;
        }

        public void CenterMap(float xPosition, float zPosition)
        {
            int centerColumnIndex = (int)(xPosition / (HexMetrics.innerDiameter * HexMetrics.chunkSizeX));
            int centerRowIndex = (int)(zPosition / (1.75f * HexMetrics.innerRadius * HexMetrics.chunkSizeZ));

            if (centerRowIndex != currentCenterRowIndex)
            {
                currentCenterRowIndex = centerRowIndex;

                int minRowIndex = centerRowIndex - chunkCountZ / 2;
                int maxRowIndex = centerRowIndex + chunkCountZ / 2;

                for (int i = 0; i < chunkCountZ; i++)
                {
                    float position = 0f;
                    if (i < minRowIndex)
                    {
                        position = chunkCountZ * (1.75f * HexMetrics.innerRadius * HexMetrics.chunkSizeZ) - 0.865f * HexMetrics.innerRadius;
                    }
                    else if (i > maxRowIndex)
                    {
                        position = chunkCountZ * -(1.75f * HexMetrics.chunkSizeZ * HexMetrics.innerRadius) + 0.865f * HexMetrics.innerRadius;
                    }

                    for (int j = 0; j < chunkCountX; j++)
                    {
                        _chunks[i * chunkCountX + j].transform.localPosition =
                            new Vector3(_chunks[i * chunkCountX + j].transform.localPosition.x, 0f, position);
                    }
                }

            }

            if (centerColumnIndex != currentCenterColumnIndex)
            {
                currentCenterColumnIndex = centerColumnIndex;

                int minColumnIndex = centerColumnIndex - chunkCountX / 2;
                int maxColumnIndex = centerColumnIndex + chunkCountX / 2;

                for (int i = 0; i < chunkCountX; i++)
                {
                    float position = 0f;
                    if (i < minColumnIndex)
                    {
                        position = chunkCountX * (HexMetrics.innerDiameter * HexMetrics.chunkSizeX);
                    }
                    else if (i > maxColumnIndex)
                    {
                        position = chunkCountX * -(HexMetrics.innerDiameter * HexMetrics.chunkSizeX);
                    }

                    for (int j = 0; j < chunkCountZ; j++)
                    {
                        _chunks[j * chunkCountX + i].transform.localPosition =
                            new Vector3(position, 0f, _chunks[j * chunkCountX + i].transform.localPosition.z);
                    }
                }
            }
        }

        public HexCell GetCell(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            HexCoordinates coordinates = HexCoordinates.FromPosition(position);

            int index = coordinates.X + coordinates.Z * cellCountX + coordinates.Z / 2;
            if (index > _cells.Length)
            {
                return _cells[_cells.Length - 1];
            }
            else
            {
                return _cells[index];
            }
        }

        public HexCell GetCell(int cellIndex)
        {
            return _cells[cellIndex];
        }

        public HexCell GetCell(HexCoordinates coordinates)
        {
            int z = coordinates.Z;
            if (z < 0 || z >= cellCountZ)
            {
                return null;
            }
            int x = coordinates.X + z / 2;
            if (x < 0 || x >= cellCountX)
            {
                return null;
            }
            return _cells[x + z * cellCountX];
        }

        private void CreateChunks()
        {
            _chunks = new HexGridChunk[chunkCountX * chunkCountZ];

            for (int z = 0, i = 0; z < chunkCountZ; z++)
            {
                for (int x = 0; x < chunkCountX; x++)
                {
                    HexGridChunk chunk = _chunks[i++] = Instantiate(chunkPrefab);
                    chunk.transform.SetParent(transform, false);
                }
            }
        }

        private void CreateCells()
        {
            _cells = new HexCell[cellCountZ * cellCountX];

            for (int z = 0, i = 0; z < cellCountZ; z++)
            {
                for (int x = 0; x < cellCountX; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        private void CreateCell(int xPos, int zPos, int i)
        {
            Vector3 position = new Vector3((xPos + zPos * 0.5f - zPos / 2) * HexMetrics.innerDiameter, 0f, zPos * HexMetrics.outerRadius * 1.5f);

            HexCell cell = _cells[i] = Instantiate(cellPrefab);
            cell.transform.localPosition = position;
            cell.coordinates = HexCoordinates.FromOffsetCoordinates(xPos, zPos);

            if (xPos > 0)
            {
                cell.SetNeighbor(HexDirection.West, _cells[i - 1]);
                if (xPos == cellCountX - 1)
                {
                    cell.SetNeighbor(HexDirection.East, _cells[i - xPos]);
                }
            }

            if (zPos > 0)
            {
                if ((zPos & 1) == 0)
                {
                    cell.SetNeighbor(HexDirection.SouthEast, _cells[i - cellCountX]);
                    if (xPos > 0)
                    {
                        cell.SetNeighbor(HexDirection.SouthWest, _cells[i - cellCountX - 1]);
                    }
                    else
                    {
                        cell.SetNeighbor(HexDirection.SouthWest, _cells[i - 1]);
                    }
                }
                else
                {
                    cell.SetNeighbor(HexDirection.SouthWest, _cells[i - cellCountX]);
                    if (xPos < cellCountX - 1)
                    {
                        cell.SetNeighbor(HexDirection.SouthEast, _cells[i - cellCountX + 1]);
                    }
                    else
                    {
                        cell.SetNeighbor(HexDirection.SouthEast, _cells[i - cellCountX * 2 + 1]);
                    }

                    if (zPos == cellCountZ - 1)
                    {
                        cell.SetNeighbor(HexDirection.NorthWest, _cells[i - cellCountX * zPos]);

                        if (xPos == cellCountX - 1)
                        {
                            cell.SetNeighbor(HexDirection.NorthEast, _cells[0]);
                        }
                        else
                        {
                            cell.SetNeighbor(HexDirection.NorthEast, _cells[i - cellCountX * zPos + 1]);
                        }
                    }
                }
            }

            RectTransform label = Instantiate(cellLabelPrefab);
            label.anchoredPosition = new Vector2(position.x, position.z);
            cell.uiRect = label;

            AddCellToChunk(xPos, zPos, cell);
        }

        private void AddCellToChunk(int x, int z, HexCell cell)
        {
            int chunkX = x / HexMetrics.chunkSizeX;
            int chunkZ = z / HexMetrics.chunkSizeZ;
            HexGridChunk chunk = _chunks[chunkX + chunkZ * chunkCountX];

            int localX = x - chunkX * HexMetrics.chunkSizeX;
            int localZ = z - chunkZ * HexMetrics.chunkSizeZ;
            chunk.AddCell(localX + localZ * HexMetrics.chunkSizeX, cell);
        }
    }
}
