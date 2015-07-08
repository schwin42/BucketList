using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TabManager : MonoBehaviour {

	public void SelectFirstTab ()
	{
		Toggle[] toggles = GetComponentsInChildren<Toggle> ();
		toggles [0].Select();
		toggles [0].isOn = true;
	}
}
