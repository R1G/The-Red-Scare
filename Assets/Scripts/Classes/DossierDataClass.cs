using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DossierDataClass : ScriptableObject {

	public List<CrimeDataClass> dossierCrimeEntries = new List<CrimeDataClass> ();

	private List<string> dossierLog = new List<string>();

	public void UpdateDossier(ClueDataClass clue) {
		dossierLog.Add (clue.clueDossierEntry);
		if (dossierLog.Count > 25) {
			dossierLog.Remove(dossierLog[0]);
		}
	}

	public string GetDossierText() {
		string dossierOutput = "";
		foreach (CrimeDataClass crime in dossierCrimeEntries) {
			dossierOutput += crime.crimeName;
			dossierOutput += "\n";
			foreach (ClueDataClass clue in crime.crimeClues) {
				dossierOutput += clue.clueDossierEntry;
				dossierOutput += "\n";
			}
			dossierOutput += "\n";
		}
		return dossierOutput;
	}
}

