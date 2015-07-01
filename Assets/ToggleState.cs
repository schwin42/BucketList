using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleState : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		GetComponent<Toggle> ().onValueChanged.AddListener (OnValueChanged);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnValueChanged (bool isOn) {
		if (isOn) {
			UiController.instance.SelectTab(gameObject);
		}
	}
}
