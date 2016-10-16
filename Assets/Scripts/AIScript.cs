using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AIScript : MonoBehaviour {

	Animator anim;

	NavMeshAgent agent;
	public GameObject gameManager;

	int communism = Random.Range (0, 10);
	int honesty = Random.Range (0, 10);
	int violence = Random.Range (0, 10);

	public TraitDataClass hat, glasses, hair, coat; 
	public GameObject hatGO, glassesGO, hairGO, coatGO;
	public ParticleSystem ps;

	//state variables
	bool isSelected = false;
	bool isAttacking = false;
	bool isFleeing = false;
	bool commitingCrime = false;
	bool inBuilding = false;

	CrimeSceneScript currentCrimeScript;

	//player unit
	public GameObject detective;

	public float walkSpeed;
	public float runSpeed;

	List <int> crimesCommitted = new List<int>();
	List <int> crimesWitnessed; 
	//this is a temporary solution; the witness list above should work better later on
	public bool isWitness;

	MeshRenderer civHeadRenderer;
	MeshRenderer civBodyRenderer;
	BoxCollider civCollider;

	float cooldownRemaining = 0f;

	private bool guiActive;

	private Ray ray;
	private RaycastHit hit;

	void Start() {
		isWitness = false;
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		//instantiating and initializing 4 traits
		hat = ScriptableObject.CreateInstance<TraitDataClass>();
		coat = ScriptableObject.CreateInstance<TraitDataClass>();
		hair = ScriptableObject.CreateInstance<TraitDataClass>();
		glasses = ScriptableObject.CreateInstance<TraitDataClass>();
		hat.Init ("wears a hat", GlobalDataScript.GetRandomBool(), hatGO);
		glasses.Init ("wears glasses", GlobalDataScript.GetRandomBool(), glassesGO);
		hair.Init ("has a beard", GlobalDataScript.GetRandomBool(), hairGO);
		coat.Init ("wears a thick coat", GlobalDataScript.GetRandomBool(), coatGO);

		ps = GetComponent<ParticleSystem> ();

		guiActive = false;
		gameObject.name = GlobalDataScript.GenerateName ();

		civHeadRenderer = gameObject.transform.GetChild (0).GetComponent<MeshRenderer>();
		civBodyRenderer = gameObject.transform.GetChild (1).GetComponent<MeshRenderer>();
		civCollider = gameObject.GetComponent<BoxCollider> ();
		
		anim = GetComponent<Animator> ();
		gameObject.tag = "Citizen";
		ps.maxParticles = communism - 5;
		ps.startLifetime = .1f;
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = walkSpeed;

		GlobalDataScript.MoveToWayPoint (gameObject);
	}

	void Update() {

		cooldownRemaining -= Time.deltaTime;

		 if (commitingCrime) {
			if (agent.remainingDistance <= 1f) {
				commitingCrime = false;
				currentCrimeScript = null;
			}
		} else {
			if (agent.hasPath != true) {
				GlobalDataScript.MoveToWayPoint (gameObject);
			}
		}
	}

	void OnGUI(){
		if (guiActive == false) {
			if (Input.GetMouseButtonDown (1) == true) {
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Physics.Raycast (ray, out hit);
				if (hit.collider.gameObject == gameObject) {
					guiActive = true;
				}
			}
		}
		if (guiActive == true){
			agent.Stop ();
			GUIStyle whiteBackground = new GUIStyle();

			Texture2D texmex = MakeTex (100, 100, Color.white);
			whiteBackground.normal.background = texmex;

			//Vector3 pos = gameObject.transform.position;

			Vector3 boxLocation = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			//this inverts the y of the ui box, which is necesary to make it track the npc
			float boxY = (Screen.height - boxLocation.y) -  50;
			float boxX = boxLocation.x + 10;

			GUILayout.BeginArea (new Rect(boxX, boxY, 100, 100),gameObject.name,whiteBackground );

			GUILayout.BeginVertical ();

			GUILayout.FlexibleSpace ();

			GUILayout.BeginHorizontal ();

			if(GUILayout.Button("Interrogate",GUILayout.Width(40))){
				AnswerQuestion ();
			}

			if(GUILayout.Button("Arrest",GUILayout.Width(40))){
				Arrest ();
			}
				
			GUILayout.EndHorizontal ();


			if(GUILayout.Button("Close")){
				guiActive = false;
				agent.Resume ();
			}

			GUILayout.EndVertical ();
			GUILayout.EndArea();
			//RectTransform g = new RectTransform ();
		}
	}

	public int GetCommunism(){
		return communism;
	}

	void Attack() {
		isAttacking = true;
		agent.SetDestination (detective.transform.position);
	}

	void Confess() {
		Debug.Log (gameObject.name + " has confessed his Communist nature");
	}

	void Escape() {
		Debug.Log (gameObject.name + " is attempting to fleeeeeeee");
		agent.Resume ();
		agent.speed = walkSpeed;
		isFleeing = true;
	}

	public void CommitCrime(Vector3 location, CrimeSceneScript crimeScript){
		agent.SetDestination (location);

		commitingCrime = true;
		currentCrimeScript = crimeScript;
		//crimesCommitted.Add (crimeScript.GetCrimeNumber ());
	}

	void TurnVisible() {
		civCollider.enabled = true;
		civHeadRenderer.enabled = true;
		civBodyRenderer.enabled = true;
	}

	private Texture2D MakeTex(int width, int height, Color col) {
		Color[] pix = new Color[width*height];

		for(int i = 0; i < pix.Length; i++)
			pix[i] = col;

		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();

		return result;
	}

	void Arrest() {
		if (GameManager.currentPerpetrator = gameObject) {
			Debug.Log ("You have arrested the perpetrator");
			GameManager.communistPower -= 20;
		} else if (communism > 7) {
			Debug.Log ("You have arrested a communist!");
			GameManager.communistPower -= 10;
		} else {
			Debug.Log ("You have arrested an innocent citizen!");
			GameManager.communistPower += 5;
			}
		GameManager.communists.Remove (gameObject);
		Destroy (gameObject);
	}

	void AnswerQuestion() {
		int answerChoice = honesty * communism - communism - 45;
		if (isWitness) {
		}
		if (isWitness) {
			if (answerChoice > 20) {
				ClueDataClass witnessClue = ScriptableObject.CreateInstance ("ClueDataClass") as ClueDataClass;
				witnessClue.Init (gameObject, GlobalDataScript.PickRandomTrait (GameManager.currentPerpetrator.GetComponent<AIScript> ()));
				GameManager.currentPerpetrator.GetComponent<AIScript> ().IncreaseEmission (10);
				witnessClue.clueDossierEntry = name + " accuses " + GameManager.currentPerpetrator.name + " of committing the " + GameManager.currentCrime.crimeName;
				GameManager.currentCrime.crimeClues.Add (witnessClue);

			} else {
				Debug.Log (gameObject.name + " was interrogated");
				ClueDataClass witnessClue = ScriptableObject.CreateInstance ("ClueDataClass") as ClueDataClass;
				witnessClue.Init (gameObject, GlobalDataScript.PickRandomTrait (GameManager.currentPerpetrator.GetComponent<AIScript> ()));
				GameManager.currentPerpetrator.GetComponent<AIScript> ().IncreaseEmission (1);
				GameManager.currentCrime.crimeClues.Add (witnessClue);
				Debug.Log (witnessClue.clueDossierEntry);
				Debug.Log (GameManager.currentCrime.crimeName);
			}
		} else {
			Debug.Log (gameObject.name + " was interrogated");
			ClueDataClass witnessClue = ScriptableObject.CreateInstance ("ClueDataClass") as ClueDataClass;
			witnessClue.Init (gameObject, GlobalDataScript.PickRandomTrait (GameManager.currentPerpetrator.GetComponent<AIScript> ()));
			GameManager.currentPerpetrator.GetComponent<AIScript> ().IncreaseEmission (0);
			witnessClue.clueDossierEntry = name + " doesn't appear to know anything at all";
			GameManager.currentCrime.crimeClues.Add (witnessClue);
			Debug.Log (witnessClue.clueDossierEntry);
			Debug.Log (GameManager.currentCrime.crimeName);
		}

	}

	public void IncreaseEmission(int emissionLvl) {
		ps.startLifetime += .25f*emissionLvl;
		ps.maxParticles += 10*emissionLvl;
		communism += 1*emissionLvl;
	}
}
