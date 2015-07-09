using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IPointerHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		UiController.instance.HandlePointerDownOnTile (gameObject);
	}

	#endregion
}
