using UnityEngine;

namespace HexGridProject.Map
{
    public struct EdgeVertices
    {
        public Vector3 vertix1, vertix2, vertix3, vertix4, vertix5;

        public EdgeVertices(Vector3 corner1, Vector3 corner2)
        {
            vertix1 = corner1;
            vertix2 = Vector3.Lerp(corner1, corner2, 0.25f);
            vertix3 = Vector3.Lerp(corner1, corner2, 0.5f);
            vertix4 = Vector3.Lerp(corner1, corner2, 0.75f);
            vertix5 = corner2;
        }
    }
}
