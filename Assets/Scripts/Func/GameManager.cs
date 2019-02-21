using UnityEngine;

public class GameManager : MonoBehaviour
{
	private PuzzleBoardUI puzzleUI;
	private PuzzleData puzzleInputData;
	public static int index = 0;
	void Start () {
		puzzleInputData =  JsonManager.DefinePath("Sudoku");
		puzzleUI = GetComponent<PuzzleBoardUI>();
		
		NextPuzzle();
	}

	public	void NextPuzzle()
	{
		if (index<puzzleInputData.gridList.Count)
		{
			puzzleUI.FillInput(puzzleInputData.gridList[index]);
			int[][] solvedPuzzle = Solver.Solve(puzzleInputData.gridList[index]);
			puzzleInputData.solvedGridList.Add(solvedPuzzle);
			index++;
			JsonManager.EditJson(solvedPuzzle, index);
			
		}
	}
}
