using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DossierDataClass : ScriptableObject {

	public List<CrimeDataClass> dossierCrimeEntries = new List<CrimeDataClass> ();

	private List<string> dossierLog = new List<string>();

	public void UpdateDossier(ClueDataClass clue) {
		dossierLog.Add (clue.clueDossierEntry);
		if (dossierLog.Count > 10) {
			dossierLog.Remove(dossierLog[0]);
		}
	}

	public string GetDossierText() {
		string dossierOutput = "";
		foreach (string dossierEntry in dossierLog) {
			dossierOutput += dossierEntry;
			dossierOutput += "\n";
		}
		return dossierOutput;
	}
}

