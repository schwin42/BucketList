using UnityEngine;
using System.Collections;

public class IntrospectionPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UiController.instance.PopulateGoalPanel ();
		print ("started");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
