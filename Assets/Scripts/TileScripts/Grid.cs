using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public LayerMask unwalkableMask;
	public float mapSizeX, mapSizeY;
	int gridSizeX, gridSizeY;

	void DrawTile(Vector3 tilePos) {
		Instantiate (Resources.Load("Grass_Block"),tilePos,Quaternion.identity);
	}

	void Start() {

	}











/*	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	void Start() {
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid ();
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
		if (grid != null) {
			foreach(Node n in grid) {
				Gizmos.color = (n.walkable)?Color.white: Color.red;
				Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - 0.1f));

			}
		}
	}

	void CreateGrid() {
		grid = new Node[gridSizeX, gridSizeY];

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = transform.position + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
				grid[x,y] = new Node(walkable,worldPoint);
			}
		}
	} */
}
