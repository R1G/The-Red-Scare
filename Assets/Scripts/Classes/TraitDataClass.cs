using UnityEngine;
using System.Collections;

public class TraitDataClass : ScriptableObject {

	public bool traitBool;
	public string traitName;
	private GameObject traitModel;

	public void Init(string _traitName, bool _traitBool, GameObject _traitModel) {
		traitBool = _traitBool;
		traitName = _traitName;
		traitModel = _traitModel;

		if (traitBool == true) {
			traitModel.GetComponent<MeshRenderer> ().enabled = true;
		}
	}
}
