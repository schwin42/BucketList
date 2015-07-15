using UnityEngine;
using System.Collections;

public class ModeManager : MonoBehaviour {

	public int startingTabIndex;

	// Use this for initialization
	void Start () {
	
		PanelManager panelManager = GetComponentInChildren<PanelManager> ();
		TabManager tabManager = GetComponentInChildren<TabManager> ();

		panelManager.DeactivatePanels ();
		tabManager.SelectStartingTab (startingTabIndex);
	}
}
