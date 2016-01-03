using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class MapEditorScript : MonoBehaviour {

	int mapArraySizeX;
	int mapArraySizeY;
	StreamWriter mapInfo;

	public static int[,] editorTileField = new int[,] {

	};


	void Start() {
		editorTileField = new int[10,10];
		for (int x = 0; x < 10; x++) {
			for (int y = 0; y < 10; y++) {
				Vector3 tilePos = new Vector3(x,y,0);
				GameObject emptyTile = Resources.Load("Empty_Tile") as GameObject;
				Instantiate(emptyTile, tilePos, Quaternion.identity);
				editorTileField[x,y] = 0;
			}
		}
	}

	void ConvertMapToString() {
		for (int x = 0; x < 10; x++) {
			for(int y = 0; y < 10; y++) {
				mapInfo.WriteLine(editorTileField[x,y].ToString());
			}
		}
	}

	public void ExportAsTextFile() {
		mapInfo = new StreamWriter ("Assets/testMap.txt");
		ConvertMapToString ();
		mapInfo.Close ();
	}
}
