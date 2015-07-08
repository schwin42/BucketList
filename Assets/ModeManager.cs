using UnityEngine;
using System.Collections;

public class ModeManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		PanelManager panelManager = GetComponentInChildren<PanelManager> ();
		TabManager tabManager = GetComponentInChildren<TabManager> ();

		panelManager.DeactivatePanels ();
		tabManager.SelectFirstTab ();
	}
}
