using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UiController : MonoBehaviour {

	private static UiController _instance;
	public static UiController instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<UiController>();
			}
			return _instance;
		}
	}

	public List<Idea> displayedPossibleGoals;

	public GameObject runtime_HighlightedButton;
	public GameObject prefab_Goal;
	public List<AudioClip> prefab_Sounds;
	public GridLayoutGroup hierarchy_AttributeBankPanel;
	public GameObject hierarchy_AttributeInfoPanel;



	// Use this for initialization
	void Awake () {
		_instance = this;
	}

	public void PopulateGoalPanel ()
	{
		print ("pop");
		List<Idea> unorderedGoalsToDisplay = Main.instance.AvailableGoalsForAlex;
		this.displayedPossibleGoals = unorderedGoalsToDisplay;
		Slot[] slots = hierarchy_AttributeBankPanel.GetComponentsInChildren<Slot> ();
		for (int i = 0; i < slots.Length; i++) {
			Slot slot = slots [i];
			GameObject uiGoal = Instantiate<GameObject> (prefab_Goal);
			Vector3 cachedScale = uiGoal.transform.localScale;
			uiGoal.transform.SetParent (slot.gameObject.transform);
			uiGoal.transform.localPosition = Vector3.zero;
			uiGoal.transform.localScale = cachedScale;
			MonoIdea monoIdea = uiGoal.GetComponent<MonoIdea> ();
			
			//Select and remove goal at random
			int randomIndex = Random.Range(0, unorderedGoalsToDisplay.Count);
			//print ("monoAttribute = " + monoAttribute);
//			print ("index, goal list count: " + randomIndex + ", " + unorderedGoalsToDisplay.Count);
			monoIdea.idea = unorderedGoalsToDisplay[randomIndex];
			unorderedGoalsToDisplay.RemoveAt(randomIndex);

//			Goal goal = monoIdea.idea as Goal;
			uiGoal.GetComponentInChildren<Text> ().text = monoIdea.idea.template_Text;
			uiGoal.GetComponent<DragHandler> ().Init ();
			uiGoal.GetComponent<Image>().color = monoIdea.idea.template_DesireColor.ToColor();
		}
	}

	public void HandlePointerDownOnTile (GameObject tileGo) {
		UnhighlightCurrentButton ();
		this.runtime_HighlightedButton = tileGo;
		tileGo.GetComponent<Highlighter> ().enableHighlight = true;
	}

	public void HandleBeginDragOnTile (GameObject tileGo) {
		//Play depart sound
		PlaySound (0);
	}

	public void HandleDragCompletionOnTile (GameObject tileGo) {
		//Play arrive sound
		PlaySound (1);
	}

	public void SelectButton (GameObject buttonGo)
	{
		print (buttonGo.name);

		//Display info for Attribute
		string output = buttonGo.GetComponent<MonoIdea> ().idea.template_Text;
		hierarchy_AttributeInfoPanel.GetComponentInChildren<Text> ().text = output;


	}

	public void UnhighlightCurrentButton () {
		if (this.runtime_HighlightedButton) {
			this.runtime_HighlightedButton.GetComponent<Highlighter> ().enableHighlight = false;
			this.runtime_HighlightedButton = null;
		}
	}

	public void HandlePointerClickOnTab (GameObject tabToggle) {
		StateMachine.instance.SetActiveTabIndex (tabToggle.transform.GetSiblingIndex ());

		//Play sound
		PlaySound (0);
	}

	public void PlaySound (int index) {
		AudioSource.PlayClipAtPoint (prefab_Sounds [index], transform.position);
	}
}
