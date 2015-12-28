using UnityEngine;
using System.Collections;

public class DirtSlabTile : BaseTile {
	
	void Update() {
		FindSelectedTroop ();
		if (selectedTroop != null) {
			CheckRange ();
			if (isWithinTravelRange) {
				SetSelectedTileColor ();
			} else {
				SetRegularTileColor ();
			}
		} else {
			SetRegularTileColor();
		}
	}
	
	void OnMouseUp() {
		FindSelectedTroop ();
		CheckRange ();
		if (isWithinTravelRange) {
			MoveSelectedTroop ();
		} 
	}
}