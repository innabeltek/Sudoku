using System.Collections.Generic;
using UnityEngine;
using System.Linq;


    public static class Solver
    {
	    private static List<int> passedBlocks = new List<int>();
	    public static int[][] solvingPuzzle = new int[9][];
		static int loop;
	    
        public static int[][] Solve(int[][] puzzle)
        {
	        loop = 0;
	        for (int i = 0; i < 9; i++)
	        {
		        solvingPuzzle[i] = (int[])puzzle[i].Clone();
	        }
	        
			FindMinBlock(solvingPuzzle);
			return solvingPuzzle;
		}

	    static void FillBlockCells(int[][] puzzle,int row)
	    {
		    List<int> possibleBlockValues = DefinePossibleBlockValues(puzzle,row);
		    for (int i = 0; i < puzzle[row].Length; i++)
		    {
			    List<int> possibleValue;
			    if (puzzle[row][i] == 0)
			    {
				    if (possibleBlockValues.Count == 1)      //if block has just one empty cell
				    {
					    puzzle[row][i] = possibleBlockValues[0];
					    possibleBlockValues.Remove(0);
				    }
				    else//cross check
				    {
					    possibleValue = DefinePossibleRowValues(puzzle, possibleBlockValues, row, i);
					    if (possibleValue.Count == 1)
					    {
						    puzzle[row][i] = possibleValue[0];
						    possibleBlockValues.Remove(possibleValue[0]);
					    }
					    else
					    {
						    possibleValue = DefinePossibleColValues(puzzle, possibleValue, row, i);
						    if (possibleValue.Count == 1)
						    {
							    puzzle[row][i] = possibleValue[0];
							    possibleBlockValues.Remove(possibleValue[0]);
						    }
					    }  
				    }
			    }
		    }
		    if (possibleBlockValues.Count > 0)
			    passedBlocks.Add(row);
		   
			FindMinBlock(puzzle);
	    }

	public static void FindMinBlock(int[][] puzzle)
	{
		int zeroCell = 9;
		var block = -1;
		for (int i = 0; i < puzzle.Length; i++)//looking for block that was not past with minimum empty cells
		{
			var matched = puzzle[i].Where(w => w == 0).Count();
			
			if (!passedBlocks.Contains(i) && zeroCell > matched && matched !=0)
			{
				zeroCell = matched;
				block = i;
			}
		}
		if (block >= 0)
			FillBlockCells(puzzle, block);
		else//check if grid is complete
		{
			int zero = 0;
			for (int i = 0; i < puzzle.Length; i++)
			{
				zero += puzzle[i].Where(w => w == 0).Count();
			}

			if (zero > 0  && loop <10)// if grid is not fill , repeat steps (FindMinBlock -> FillBlockCells)
			{
				loop++;
				passedBlocks = new List<int>();
				FindMinBlock(puzzle);
			}
			else if (zero > 0 && loop >= 10)
			{
				//TODO: bactracking
			}
			else
				return;
		}
	}

	static List<int> DefinePossibleBlockValues(int[][] puzzle, int row)
	{
		int[] possibleValues = {1, 2, 3, 4, 5, 6, 7, 8, 9};
		var res =	possibleValues.Where(x => !puzzle[row].Contains(x)).ToList();
		return res;
	}

	static List<int> DefinePossibleRowValues(int[][] puzzle, List<int> possibleBlockValues, int row, int col)
	{
		
		row = row < 3 ? 0 : row < 6 ? 3 : 6;
		col = col < 3 ? 0 : col < 6 ? 3 : 6;
		List<int> res = possibleBlockValues.ToList();
		 
		for (int i = row; i < row + 3; i++)
		{
			for (int j = col; j < col+ 3; j++)
			{
				if (res.Contains(puzzle[i][j]))
					res.Remove(puzzle[i][j]);
			}
		}
		return res;
	}
	
	static List<int> DefinePossibleColValues(int[][] puzzle, List<int> possibleBlockValues, int row, int col)
	{
		row = row < 3 ? row : row < 6 ? row - 3 : row - 6;
		col = col < 3 ? col : col < 6 ? col - 3 : col - 6;
		List<int> res = possibleBlockValues.ToList();;
		
		for (int i = row; i <= row + 6; i=i+3)
		{
			for (int j = col; j <= col + 6; j=j+3)
			{
				if (res.Contains(puzzle[i][j]))
					res.Remove(puzzle[i][j]);
			}
		}
		return res;
	}
   }