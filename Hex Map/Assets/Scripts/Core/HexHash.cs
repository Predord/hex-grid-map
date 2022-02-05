using UnityEngine;

namespace HexGridProject.Core
{
    public struct HexHash
    {
        public float a;

        public static HexHash Create()
        {
            HexHash hash;
            hash.a = Random.value * 0.999f;

            return hash;
        }
    }
}
