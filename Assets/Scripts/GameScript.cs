using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	static public string turn = "PlayerTurn";
	public static int playerMoves = 1;

	public static void playerTurn() {
		playerMoves -= 1;
		if (playerMoves == 0) {
			turn = "EnemyTurn";
			playerMoves = 1;
		} 
	}
}
