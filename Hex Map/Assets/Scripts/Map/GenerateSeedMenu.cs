using UnityEngine;

namespace HexGridProject.Map
{
    public class GenerateSeedMenu : MonoBehaviour
    {
        public HexGrid grid;
        public HexMapGenerator mapGenerator;

        public void GenerateMap()
        {
            mapGenerator.GenerateMap(grid.cellCountX, grid.cellCountZ);
        }

        public void SetSeed(string seed)
        {
            grid.seed = int.Parse(seed);
        }
    }
}
