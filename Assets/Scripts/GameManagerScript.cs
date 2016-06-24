using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour{

	public class enemy {

		public enemy(string prefab, Vector3 spawnPoint) {
			Instantiate(Resources.Load(prefab), spawnPoint, Quaternion.identity);
		}
	}
}
