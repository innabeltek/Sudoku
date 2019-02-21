using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

[Serializable]
public static class JsonManager{

	static string path;
	static string content;
	public static PuzzleData puzzleData;
	
	public static PuzzleData DefinePath(string fileName)
	{
#if UNITY_IPHONE
        path = Application.temporaryCachePath.Replace("/Caches", "") + fileName + ".json";
#elif UNITY_ANDROID
		path = Application.persistentDataPath + "/" + fileName + ".json";
#else
		path = Application.persistentDataPath + "/" + fileName + ".json";
#endif
        
		if (!File.Exists(path))
			WriteJson(fileName);
		return	ReadJson();
	}

	static void WriteJson(string fileName)
	{
		TextAsset level = (TextAsset)Resources.Load(fileName);
		File.WriteAllText(path, level.text);
	}
	
	static PuzzleData ReadJson()
	{
		puzzleData = new PuzzleData();
		content = File.ReadAllText(path);
		Debug.Log(content);
		puzzleData = JsonConvert.DeserializeObject<PuzzleData>(content);
		puzzleData.FillGridList();
		puzzleData.solvedGridList = new List<int[][]>();
		return puzzleData;
	}

	public static void EditJson(int[][] solvedPuzle, int index)
	{
		string fieldName = "SolvedGrid_" + index;
		
		JObject jsonObject = JObject.Parse(File.ReadAllText(path));
		JArray incomingEvents = jsonObject[fieldName].Value<JArray>();
		for (int i=0;i<solvedPuzle.Length;i++)
		{
			JArray newEventJsonItem = new JArray(solvedPuzle[i].ToList());//Convert newEvent to JArray.
			incomingEvents.Insert(i,newEventJsonItem);//Insert new JArray object.
		}
		jsonObject[fieldName] = incomingEvents;
		
		string output =  JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
		File.WriteAllText(path,output);
	}
}
