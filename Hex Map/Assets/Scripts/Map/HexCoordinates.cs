using System;
using UnityEngine;
using HexGridProject.Core;

namespace HexGridProject.Map
{
    [Serializable]
    public struct HexCoordinates
    {
        public int X
        {
            get
            {
                return x;
            }
        }
        public int Z
        {
            get
            {
                return z;
            }
        }
        public int Y
        {
            get
            {
                return -X - Z;
            }
        }

        [SerializeField] private int x, z;

        public HexCoordinates(int x, int z)
        {
            if (z < 0)
            {
                int oX = x;

                x += (z + 1) / 2;
                z += HexMetrics.wrapSizeZ;

                int currentChunk = z / HexMetrics.chunkSizeZ;
                int offset = HexMetrics.chunkSizeZ / 2 * currentChunk + z % HexMetrics.chunkSizeZ / 2 + 1;

                x -= offset;

                if (oX < 0)
                {
                    z++;
                    if (z % 2 == 0)
                    {
                        x--;
                    }
                }
            }
            else if (z >= HexMetrics.wrapSizeZ)
            {
                int oX = x;

                x += z / 2;
                z -= HexMetrics.wrapSizeZ;

                int currentChunk = z / HexMetrics.chunkSizeZ;
                int offset = HexMetrics.chunkSizeZ / 2 * currentChunk + z % HexMetrics.chunkSizeZ / 2;

                x -= offset;

                if (oX < 0)
                {
                    z++;
                    if (z % 2 == 0)
                    {
                        x--;
                    }
                }
            }
            else
            {
                int oX = x + z / 2;
                if (oX < 0)
                {
                    x += HexMetrics.wrapSizeX;
                }
                else if (oX >= HexMetrics.wrapSizeX)
                {
                    x -= HexMetrics.wrapSizeX;
                }
            }

            this.x = x;
            this.z = z;
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new HexCoordinates(x - z / 2, z);
        }

        public static HexCoordinates FromPosition(Vector3 position)
        {
            float x = position.x / HexMetrics.innerDiameter;
            float y = -x;
            float offset = position.z / (HexMetrics.outerRadius * 3f);
            x -= offset;
            y -= offset;

            int clickedX = Mathf.RoundToInt(x);
            int clickedY = Mathf.RoundToInt(y);
            int clickedZ = Mathf.RoundToInt(-x - y);

            if (clickedX + clickedY + clickedZ != 0)
            {
                float deltaX = Mathf.Abs(x - clickedX);
                float deltaY = Mathf.Abs(y - clickedY);
                float delatZ = Mathf.Abs(-x - y - clickedZ);

                if (deltaX > deltaY && deltaX > delatZ)
                {
                    clickedX = -clickedY - clickedZ;
                }
                else if (delatZ > deltaY)
                {
                    clickedZ = -clickedX - clickedY;
                }
            }

            return new HexCoordinates(clickedX, clickedZ);
        }

        public int DistanceTo(HexCoordinates other)
        {
            return
                ((x < other.x ? other.x - x : x - other.x) +
                (Y < other.Y ? other.Y - Y : Y - other.Y) +
                (z < other.z ? other.z - z : z - other.z)) / 2;
        }

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ") ";
        }
    }
}
