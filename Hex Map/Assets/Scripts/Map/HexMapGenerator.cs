using UnityEngine;
using HexGridProject.Core;

namespace HexGridProject.Map
{
	public class HexMapGenerator : MonoBehaviour
	{
		[Range(0f, 0.5f)]
		public float jitterProbability = 0.25f;

		[Range(20, 200)]
		public int chunkSizeMin = 30;

		[Range(20, 200)]
		public int chunkSizeMax = 100;

		[Range(5, 95)]
		public int newTerrainPercentage = 50;

		public HexGrid grid;

		private int searchFrontierPhase;
		private int cellCount;
		private HexCellPriorityQueue searchFrontier;

		public void GenerateMap(int x, int z)
		{
			Random.State originalRandomState = Random.state;
			Random.InitState(grid.seed);

			cellCount = x * z;
			grid.CreateMap(x, z);

			if (searchFrontier == null)
			{
				searchFrontier = new HexCellPriorityQueue();
			}

			CreateLand();
			SetTerrainType();

			for (int i = 0; i < cellCount; i++)
			{
				grid.GetCell(i).SearchPhase = 0;
			}

			Random.state = originalRandomState;
		}

		private void CreateLand()
		{
			int landBudget = Mathf.RoundToInt(cellCount * newTerrainPercentage * 0.01f);

			while (landBudget > 0)
			{
				landBudget = RaiseMountains(Random.Range(chunkSizeMin, chunkSizeMax + 1), landBudget);
			}
		}

		private int RaiseMountains(int chunkSize, int budget)
		{
			searchFrontierPhase += 1;
			HexCell firstCell = GetRandomCell();
			firstCell.SearchPhase = searchFrontierPhase;
			firstCell.Distance = 0;
			firstCell.SearchHeuristic = 0;
			searchFrontier.Enqueue(firstCell);
			HexCoordinates center = firstCell.coordinates;

			int size = 0;
			while (size < chunkSize && searchFrontier.Count > 0)
			{
				HexCell current = searchFrontier.Dequeue();

				if (current.CellType != HexCellType.Mountain)
				{
					current.CellType = current.CellType + 1;
					if (current.Elevation == 1 && --budget == 0)
					{
						break;
					}
				}

				size += 1;

				for (HexDirection d = HexDirection.NorthEast; d <= HexDirection.NorthWest; d++)
				{
					HexCell neighbor = current.GetNeighbor(d);
					if (neighbor && neighbor.SearchPhase < searchFrontierPhase)
					{
						neighbor.SearchPhase = searchFrontierPhase;
						neighbor.Distance = neighbor.coordinates.DistanceTo(center);
						neighbor.SearchHeuristic = Random.value < jitterProbability ? 1 : 0;
						searchFrontier.Enqueue(neighbor);
					}
				}
			}

			searchFrontier.Clear();

			return budget;
		}

		private void SetTerrainType()
		{
			for (int i = 0; i < cellCount; i++)
			{
				HexCell cell = grid.GetCell(i);
				cell.TerrainTypeIndex = (int)cell.CellType;
			}
		}

		private HexCell GetRandomCell()
		{
			return grid.GetCell(Random.Range(0, cellCount));
		}
	}
}
