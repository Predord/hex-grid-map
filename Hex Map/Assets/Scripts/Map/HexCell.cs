using UnityEngine;
using UnityEngine.UI;
using HexGridProject.Core;

namespace HexGridProject.Map
{
    public class HexCell : MonoBehaviour
    {
        public HexCoordinates coordinates;
        public HexGridChunk chunk;
        public RectTransform uiRect;

        public HexCell NextWithSamePriority { get; set; }

        [SerializeField] private HexCell[] _neighbors;

        public int Elevation
        {
            get
            {
                if (cellType == HexCellType.Desert || cellType == HexCellType.GrassLand)
                {
                    return 0;
                }
                else if (cellType == HexCellType.Hill)
                {
                    return 1;
                }
                else
                {
                    HexHash hash = HexMetrics.SampleHashGrid(Position);

                    return HexMetrics.normalMountainElevation + Mathf.RoundToInt(HexMetrics.maxAdditionalMountainElevation * hash.a);
                }
            }
        }

        public HexCellType CellType
        {
            get
            {
                return cellType;
            }
            set
            {
                if (value == cellType)
                    return;

                cellType = value;
                RefreshPosition();
                Refresh();
            }
        }

        private HexCellType cellType;

        public int TerrainTypeIndex
        {
            get
            {
                return terrainTypeIndex;
            }
            set
            {
                if (terrainTypeIndex != value)
                {
                    terrainTypeIndex = value;
                    Refresh();
                }
            }
        }

        private int terrainTypeIndex;

        public Vector3 Position
        {
            get
            {
                return transform.localPosition;
            }
        }

        public int Distance
        {
            get
            {
                return distance;
            }
            set
            {
                distance = value;
            }
        }

        private int distance;

        public int SearchPhase { get; set; }
        public int SearchHeuristic { get; set; }

        public int SearchPriority
        {
            get
            {
                return distance + SearchHeuristic;
            }
        }

        public HexCell GetNeighbor(HexDirection direction)
        {
            return _neighbors[(int)direction];
        }

        public void SetNeighbor(HexDirection direction, HexCell cell)
        {
            _neighbors[(int)direction] = cell;
            cell._neighbors[(int)direction.Opposite()] = this;
        }

        private void Refresh()
        {
            if (chunk)
            {
                chunk.Refresh();
                for (int i = 0; i < _neighbors.Length; i++)
                {
                    HexCell neighbor = _neighbors[i];
                    if (neighbor != null && neighbor.chunk != chunk)
                    {
                        neighbor.chunk.Refresh();
                    }
                }
            }
        }

        private void RefreshPosition()
        {
            Vector3 position = transform.localPosition;
            position.y = Elevation * HexMetrics.elevationStep;
            transform.localPosition = position;

            Vector3 uiPosition = uiRect.localPosition;
            uiPosition.z = -position.y;
            uiRect.localPosition = uiPosition;
        }
        
        public void DisableHighlight()
        {
            for (int i = 0; i < 6; i++)
            {
                Image highlight = uiRect.GetChild(i).GetComponent<Image>();
                highlight.enabled = false;
            }
        }

        public void DisableHighlight(HexDirection direction)
        {
            Image highlight = uiRect.GetChild((int)direction).GetComponent<Image>();
            highlight.enabled = false;
        }

        public void EnableHighlight(Color color)
        {
            for(int i = 0; i < 6; i++)
            {
                Image highlight = uiRect.GetChild(i).GetComponent<Image>();
                highlight.color = color;
                highlight.enabled = true;
            }
        }

        public void EnableHighlight(Color color, HexDirection direction)
        {
            Image highlight = uiRect.GetChild((int)direction).GetComponent<Image>();
            highlight.color = color;
            highlight.enabled = true;
        }
    }
}
