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

	public GameObject uiPrefab_Goal;
	public GridLayoutGroup uiHierarchy_AttributeBankPanel;
	public GameObject UiHierarchy_AttributeInfoPanel;
	public GameObject selectedButton;


	// Use this for initialization
	void Awake () {
		_instance = this;
	}

	public void PopulateGoalPanel (List<Idea> unorderedGoalsToDisplay)
	{
		this.displayedPossibleGoals = unorderedGoalsToDisplay;
		Slot[] slots = uiHierarchy_AttributeBankPanel.GetComponentsInChildren<Slot> ();
		for (int i = 0; i < slots.Length; i++) {
			Slot slot = slots [i];
			GameObject uiGoal = Instantiate<GameObject> (uiPrefab_Goal);
			Vector3 cachedScale = uiGoal.transform.localScale;
			uiGoal.transform.SetParent (slot.gameObject.transform);
			uiGoal.transform.localPosition = Vector3.zero;
			uiGoal.transform.localScale = cachedScale;
			MonoIdea monoIdea = uiGoal.GetComponent<MonoIdea> ();
			
			//Select and remove goal at random
			int randomIndex = Random.Range(0, unorderedGoalsToDisplay.Count);
			//print ("monoAttribute = " + monoAttribute);
			print ("index, goal list count: " + randomIndex + ", " + unorderedGoalsToDisplay.Count);
			monoIdea.idea = unorderedGoalsToDisplay[randomIndex];
			unorderedGoalsToDisplay.RemoveAt(randomIndex);

//			Goal goal = monoIdea.idea as Goal;
			uiGoal.GetComponentInChildren<Text> ().text = monoIdea.idea.template_Text;
			uiGoal.GetComponent<DragHandler> ().Init ();
			uiGoal.GetComponent<Image>().color = monoIdea.idea.template_DesireColor.ToColor();
		}
	}

	public void SelectButton (GameObject buttonGo)
	{
		DeselectCurrentButton ();
		this.selectedButton = buttonGo;
		buttonGo.GetComponent<Highlighter> ().enableHighlight = true;
		//TODO Handling for multiple ui modes

		//Display info for Attribute
		string output = buttonGo.GetComponent<MonoIdea> ().idea.template_Text;
		UiHierarchy_AttributeInfoPanel.GetComponentInChildren<Text> ().text = output;
	}

	public void DeselectCurrentButton () {
		if (this.selectedButton) {
			this.selectedButton.GetComponent<Highlighter> ().enableHighlight = false;
			this.selectedButton = null;
		}
	}
}
