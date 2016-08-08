using UnityEngine;
using System.Collections;

public class ClueDataClass : ScriptableObject {

	public GameObject clueSource;
	public TraitDataClass perpTrait;
	public string clueDossierEntry;

	public void Init (GameObject _clueSource, TraitDataClass _trait) {
		clueSource = _clueSource;
		perpTrait = _trait;
		CreateClueString ();
	}

	private void CreateClueString() {
		if (perpTrait.traitBool == true) {
			clueDossierEntry = clueSource.name + " suggests that the perpetrator " + perpTrait.traitName;
		} else {
			clueDossierEntry = clueSource.name +  " had no information on the perpetrator";
		}
	}
}
