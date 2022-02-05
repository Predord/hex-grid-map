using UnityEngine;
using HexGridProject.Map;
using HexGridProject.Core;

namespace HexGridProject.Camera
{
    public class CameraMain : MonoBehaviour
    {
        public Transform _transform;
        public HexGrid grid;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _transform.position = WrapPosition(_transform.position);
        }

        private void Update()
        {
            _transform.position = WrapPosition(_transform.position);
        }

        private Vector3 WrapPosition(Vector3 position)
        {
            float width = grid.cellCountX * HexMetrics.innerDiameter;
            while (position.x < 0f)
            {
                position.x += width;
            }
            while (position.x > width)
            {
                position.x -= width;
            }

            float height = grid.cellCountZ / 2 * 3.5f * HexMetrics.innerRadius;

            while (position.z < 0f)
            {
                position.z += height;
            }
            while (position.z > height)
            {
                position.z -= height;
            }

            grid.CenterMap(position.x, position.z);
            return position;
        }
    }
}
