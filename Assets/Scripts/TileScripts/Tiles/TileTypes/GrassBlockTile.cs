using UnityEngine;
using System.Collections;

public class GrassBlockTile : BaseTile {
	
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