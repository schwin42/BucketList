using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
//	Vector3 startPosition;
//	Transform startParent;
	public Transform nextParent;
	Transform window;
	Lerper lerper;

	public void Init() {
		lerper = GetComponent<Lerper> ();
		window = transform.parent.parent.parent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		lerper.TerminateLerp ();
		UiController.instance.SelectButton (gameObject);
		itemBeingDragged = gameObject;
//		startPosition = transform.position;
//		startParent = 
		nextParent = transform.parent;

		//Move draggable to window and order last to move to front. This is dumb.
		transform.SetParent (window); 
		transform.SetAsLastSibling ();

		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	void IDragHandler.OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		//print ("end drag");
		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		lerper.LerpToTransform (nextParent, () => { 
			//print("set parent");
			transform.SetParent(nextParent); 
		});
	}

}
