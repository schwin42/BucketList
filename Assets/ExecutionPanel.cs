using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExecutionPanel : MonoBehaviour {

	public GameObject prefab_Child;
	public Transform hierarchy_TargetSelection;
	public Transform hierarchy_ActivitySelection;
	public Transform hierarchy_StyleSelection;

	// Use this for initialization
	void Start () {
	
		foreach (Character character in UiController.instance.GetTargetChoices()) {
			GameObject gameObject = Instantiate(prefab_Child);
			gameObject.transform.SetParent(hierarchy_TargetSelection);
			MonoCharacter monoCharacter = gameObject.AddComponent<MonoCharacter>();
			monoCharacter.character = character;
			gameObject.GetComponentInChildren<Text>().text = character.characterName;
		}

		foreach (Idea idea in UiController.instance.GetActivityChoices()) {
			GameObject gameObject = Instantiate(prefab_Child);
			gameObject.transform.SetParent(hierarchy_ActivitySelection);
			MonoIdea monoIdea = gameObject.AddComponent<MonoIdea>();
			monoIdea.idea = idea;
			gameObject.GetComponentInChildren<Text>().text = idea.template_Text;
		}

		foreach (Idea idea in UiController.instance.GetStyleChoices()) {
			GameObject gameObject = Instantiate(prefab_Child);
			gameObject.transform.SetParent(hierarchy_StyleSelection);
			MonoIdea monoIdea = gameObject.AddComponent<MonoIdea>();
			monoIdea.idea = idea;
			gameObject.GetComponentInChildren<Text>().text = idea.template_Text;
		}


	}

}
