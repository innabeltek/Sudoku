using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleBoardUI : MonoBehaviour {

	public GameObject puzzleBoard;
	public GameObject finalText;
	public Text title;
	public Color origin;
	
	private void Start()
	{
		title.text = "Game_" + (GameManager.index);
	}

	public void FillInput(int[][] puzzle)//Fill UI cells with new grid and clean cells after previous grid
	{
		
		for(int i=0;i<9;i++)
		{
			for (int j = 0; j < 9; j++)
			{
				var inputField =  puzzleBoard.transform.GetChild(i).transform.GetChild(j);
				inputField.gameObject.AddComponent<ActiveCells>();//ActiveCells script highlight block, row and column of the cell
				inputField.GetComponent<InputField>().text = string.Empty;	
				if (puzzle[i][j] == 0)
				{
					if(!inputField.transform.FindChild("Placeholder").GetComponent<Text>().text.Equals(string.Empty))
						inputField.transform.FindChild("Placeholder").GetComponent<Text>().text = string.Empty;
					inputField.GetComponent<InputField>().readOnly = false;
					Color tmp = new Color(255,255,255,255);
					inputField.GetComponent<InputField>().caretColor = tmp;
					
				}
				else
				{
					inputField.GetComponent<InputField>().transition = Selectable.Transition.None;
					inputField.GetComponent<InputField>().readOnly = true;
					Color tmp = new Color(0,0,0,0);
					inputField.GetComponent<InputField>().caretColor = tmp;
					inputField.transform.FindChild("Placeholder").GetComponent<Text>().text = puzzle[i][j].ToString();
				}
				
			}
		}
	}

	public void Reset()
	{
		FillInput(JsonManager.puzzleData.gridList[GameManager.index-1]);
	}

	public void Help()
	{
		for(int i=0;i<9;i++)
		{
			for (int j = 0; j < 9; j++)
			{
				var inputField =  puzzleBoard.transform.GetChild(i).transform.GetChild(j);
				var txt = inputField.GetComponent<InputField>().text;
				if (!txt.Equals(string.Empty) && !txt.Equals(Solver.solvingPuzzle[i][j].ToString()))
					StartCoroutine(Highlight(inputField));
			}
		}
	}

	public void Solve()
	{
		FillInput(Solver.solvingPuzzle);
	}

	IEnumerator Highlight(Transform inputField)
	{
		Color tmp = origin;
		
		for (float i = 1; i <= 0; i -= Time.deltaTime)
		{
			tmp.a = i;
			inputField.GetComponent<Image>().color = tmp;
		}
		for (float i = 0; i <= 1; i += Time.deltaTime)
		{
			
			tmp.a = i;
			inputField.GetComponent<Image>().color = tmp;
			yield return null;
		}
	}

	public void Complete()
	{
		if (GameManager.index == 7)
		{
			finalText.SetActive(true);
			return;
		}
		bool completed = true;
		for(int i=0;i<9;i++)
		{
			for (int j = 0; j < 9; j++)
			{
				var inputField =  puzzleBoard.transform.GetChild(i).transform.GetChild(j);
				var txt = inputField.GetComponent<InputField>().text;
				var plcHolder = inputField.transform.FindChild("Placeholder").GetComponent<Text>().text;
				if ( !plcHolder.Equals(Solver.solvingPuzzle[i][j].ToString()) && !txt.Equals(Solver.solvingPuzzle[i][j].ToString()))
				{
					completed = false;
					StartCoroutine(Highlight(inputField));
				}
					
			}
		}

		if (completed)
		{
			GetComponent<GameManager>().NextPuzzle();
			title.text = "Game_" + (GameManager.index);
		}
	}

	public void Exit()
	{
		Application.Quit();
	}
}
