using System.Collections.Generic;

namespace HexGridProject.Map
{
	public class HexCellPriorityQueue
	{
		private List<HexCell> list = new List<HexCell>();

		private int count = 0;
		private int minimum = int.MaxValue;

		public int Count
		{
			get
			{
				return count;
			}
		}

		public void Enqueue(HexCell cell)
		{
			count += 1;
			int priority = cell.SearchPriority;

			if (priority < minimum)
			{
				minimum = priority;
			}

			while (priority >= list.Count)
			{
				list.Add(null);
			}

			cell.NextWithSamePriority = list[priority];
			list[priority] = cell;
		}

		public HexCell Dequeue()
		{
			count -= 1;
			for (; minimum < list.Count; minimum++)
			{
				HexCell cell = list[minimum];
				if (cell != null)
				{
					list[minimum] = cell.NextWithSamePriority;
					return cell;
				}
			}

			return null;
		}

		public void Clear()
		{
			list.Clear();
			count = 0;
			minimum = int.MaxValue;
		}
	}
}