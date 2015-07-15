using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TabManager : MonoBehaviour {

	public void SelectStartingTab (int index)
	{
		Toggle[] toggles = GetComponentsInChildren<Toggle> ();
		toggles [index].Select();
		toggles [index].isOn = true;
	}
}
