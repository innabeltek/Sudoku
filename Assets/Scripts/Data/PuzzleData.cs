using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class PuzzleData 
{
	public List<int[][]> gridList;
	[JsonProperty("Grid_1")]
	public int[][] Grid_1 { get; set; }
	[JsonProperty("Grid_2")]
	public int[][] Grid_2 { get; set; }
	[JsonProperty("Grid_3")]
	public int[][] Grid_3 { get; set; }
	[JsonProperty("Grid_4")]
	public int[][] Grid_4 { get; set; }
	[JsonProperty("Grid_5")]
	public int[][] Grid_5 { get; set; }
	[JsonProperty("Grid_6")]
	public int[][] Grid_6 { get; set; }
	[JsonProperty("Grid_7")]
	public int[][] Grid_7 { get; set; }

	public void FillGridList()
	{
		gridList = new List<int[][]>()
			{Grid_1,Grid_2,Grid_3,Grid_4,Grid_5,Grid_6,Grid_7};
	}
	
	public List<int[][]> solvedGridList;
	[JsonProperty("SolvedGrid_1")]
	public int[][] SolvedGrid_1 { get; set; }
	[JsonProperty("SolvedGrid_2")]
	public int[][] SolvedGrid_2 { get; set; }
	[JsonProperty("SolvedGrid_3")]
	public int[][] SolvedGrid_3 { get; set; }
	[JsonProperty("SolvedGrid_4")]
	public int[][] SolvedGrid_4 { get; set; }
	[JsonProperty("SolvedGrid_5")]
	public int[][] SolvedGrid_5 { get; set; }
	[JsonProperty("SolvedGrid_6")]
	public int[][] SolvedGrid_6 { get; set; }
	[JsonProperty("SolvedGrid_7")]
	public int[][] SolvedGrid_7 { get; set; }

}
