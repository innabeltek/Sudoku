using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActiveCells : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	readonly Color32 origin = new Color32(238,233,226,255);
	readonly Color32 active = new Color32(143,197,94,255);
	readonly Color32 crossColor = new Color32(219,229,163,255);
	
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		InputField[] cells = GameObject.Find("PuzzleBoard").GetComponentsInChildren<InputField>();
		Image[] blockCells = gameObject.transform.parent.GetComponentsInChildren<Image>();
		for (int i = 1; i < blockCells.Length; i++)
		{
			blockCells[i].color = crossColor;
		}

		foreach (var c in cells)
		{
			if (c.transform.position.y == gameObject.transform.position.y ||
			    c.transform.position.x == gameObject.transform.position.x)
				c.GetComponent<Image>().color = crossColor;
		}
		
		gameObject.GetComponent<Image>().color = active;
	}
 
	public void OnPointerExit(PointerEventData eventData)
	{
		InputField[] cells = GameObject.Find("PuzzleBoard").GetComponentsInChildren<InputField>();
		Image[] blockCells = gameObject.transform.parent.GetComponentsInChildren<Image>();
		for (int i = 1; i < blockCells.Length; i++)
		{
			blockCells[i].color = origin;
		}
		foreach (var c in cells)
		{
			if (c.transform.position.y == gameObject.transform.position.y ||
			    c.transform.position.x == gameObject.transform.position.x)
				c.GetComponent<Image>().color = origin;
		}
	}  
}
