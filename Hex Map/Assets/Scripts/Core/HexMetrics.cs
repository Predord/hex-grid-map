using UnityEngine;

namespace HexGridProject.Core
{
    public static class HexMetrics
    {
        public const float outerRadius = 2f;
        public const float innerRadius = outerRadius * 0.866025404f;
        public const float innerDiameter = 2f * innerRadius;
        public const float solidFactor = 0.8f;
        public const float blendFactor = 1f - solidFactor;
        public const float elevationStep = 0.8f;
        public const int normalMountainElevation = 3;
        public const int maxAdditionalMountainElevation = 2;
        public const float additionalMountainPickElevation = 1f * elevationStep;
        public const int chunkSizeX = 5;
        public const int chunkSizeZ = 4;
        public const int hashGridSize = 256;
        public const float hashGridScale = 1.25f;

        public static int wrapSizeX;
        public static int wrapSizeZ;

        private static HexHash[] _hashGrid;

        private static Vector3[] corners =
        {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius),
    };

        public static Vector3 GetFirstSolidCorner(HexDirection direction)
        {
            return corners[(int)direction] * solidFactor;
        }

        public static Vector3 GetSecondSolidCorner(HexDirection direction)
        {
            return corners[(int)direction + 1] * solidFactor;
        }

        public static Vector3 GetBridge(HexDirection direction)
        {
            return (corners[(int)direction] + corners[(int)direction + 1]) * blendFactor;
        }

        public static HexHash SampleHashGrid(Vector3 position)
        {
            int x = (int)(position.x * hashGridScale) % hashGridSize;
            if (x < 0)
            {
                x += hashGridSize;
            }
            int z = (int)(position.z * hashGridScale) % hashGridSize;
            if (z < 0)
            {
                z += hashGridSize;
            }
            return _hashGrid[x + z * hashGridSize];
        }

        public static void InitializeHashGrid(int seed)
        {
            _hashGrid = new HexHash[hashGridSize * hashGridSize];
            Random.State currentState = Random.state;
            Random.InitState(seed);
            for (int i = 0; i < _hashGrid.Length; i++)
            {
                _hashGrid[i] = HexHash.Create();
            }
            Random.state = currentState;
        }
    }
}
