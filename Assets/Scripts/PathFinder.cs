using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {

	public GameObject seeker, target;
	static int count = 0;
	public static List<GameObject> path;

	public static void FindPath(GameObject startTile, GameObject endTile) {
		GameObject startNode = startTile;
		GameObject targetNode = endTile;

		List<GameObject> openSet = new List<GameObject>();
		HashSet<GameObject> closedSet = new HashSet<GameObject> ();
		openSet.Add (startTile);
		List<GameObject> neighbourNode = new List<GameObject> ();

		while (openSet.Count > 0) {
			GameObject currentNode = openSet [0];
			count++;
			if (count > 50) {
				Debug.Log (openSet.Count);
				return;
			}
			for (int i = 0; i < openSet.Count; i++) {
				if (openSet [i].GetComponent<BaseTile> ().fCost < currentNode.GetComponent<BaseTile> ().fCost || 
					openSet [i].GetComponent<BaseTile> ().fCost == currentNode.GetComponent<BaseTile> ().fCost && 
					openSet [i].GetComponent<BaseTile> ().hCost < currentNode.GetComponent<BaseTile> ().hCost	) {
					currentNode = openSet [i];
				}
			} 

			openSet.Remove (currentNode);
			closedSet.Add (currentNode);

			if (currentNode == targetNode) {
				RetracePath (startNode, targetNode);
				return;
			}

			for (int x = -1; x <= 1; x++)
				for (int z = -1; z <= 1; z++) {
					if (TileGenerator.tilesRef [(int)startTile.transform.position.x + x, (int)startTile.transform.position.z + z].tag == "walkableTile") {
						neighbourNode.Add (TileGenerator.tilesRef [(int)startTile.transform.position.x + x, (int)startTile.transform.position.z + z]); 
					}
				}

			foreach (GameObject neighbour in neighbourNode) {
				if (!closedSet.Contains (neighbour)) {
					continue;
				}

				int NewMovementCostToNeighbour = currentNode.GetComponent<BaseTile> ().gCost + GetDistance (currentNode, neighbour);
				if (NewMovementCostToNeighbour < neighbour.GetComponent<BaseTile> ().gCost || !openSet.Contains (neighbour)) {
					neighbour.GetComponent<BaseTile> ().gCost = NewMovementCostToNeighbour;
					neighbour.GetComponent<BaseTile> ().hCost = GetDistance (neighbour, targetNode);
					neighbour.GetComponent<BaseTile> ().parent = currentNode;

					if (!openSet.Contains (neighbour)) {
						openSet.Add (neighbour);
					}
				}
			}
		}
	}

	public static void RetracePath(GameObject startNode, GameObject endNode) {
		List<GameObject> path = new List<GameObject> ();
		GameObject currentNode = endNode;

			while (currentNode != startNode) {
				path.Add (currentNode);
				currentNode = currentNode.GetComponent<BaseTile> ().parent;
			}
 		
		path.Reverse ();

		foreach (GameObject tile in path) {
			tile.GetComponent<MeshRenderer> ().material.color = Color.red;
		}

	}

	public static int GetDistance(GameObject tileA, GameObject tileB) {
		int distanceX = Mathf.Abs ((int)tileA.transform.position.x - (int)tileB.transform.position.x);
		int distanceZ = Mathf.Abs ((int)tileA.transform.position.z - (int)tileB.transform.position.z);

		if (distanceX > distanceZ) {
			return 14 * distanceZ + 10 * (distanceX - distanceZ);
		} else {
			return 14 * distanceX + 10 * (distanceZ - distanceX);
		}
	}
}
