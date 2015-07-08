using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleState : MonoBehaviour {

	void Awake () {
		GetComponent<Toggle> ().onValueChanged.AddListener (OnValueChanged);
	}

	public void OnValueChanged (bool isOn) {
		if (isOn) {
			UiController.instance.SelectTab(gameObject);
		}
	}
}
