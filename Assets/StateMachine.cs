using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine : MonoBehaviour {
	
	private static StateMachine _instance;
	public static StateMachine instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<StateMachine>();
			}
			return _instance;
		}
	}

	public List<GameObject> uiHierarchy_tabModes;

	public GameObject selectedTabMode;

	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetActiveTabIndex(int i) {
		//Deactivate old panel
		if (selectedTabMode) {
			selectedTabMode.SetActive(false);
		}

		//Activate new panel
		selectedTabMode = uiHierarchy_tabModes [i];
		selectedTabMode.SetActive (true);

	}
}
