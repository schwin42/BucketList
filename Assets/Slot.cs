using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

	public GameObject item {
		get {
			if(transform.childCount > 0) {
				return transform.GetChild (0).gameObject;
			}
			return null;
		}

	}

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
//		print ("on drop");
		if (!item) {
//			print ("set transform to " + transform.name
			DragHandler.itemBeingDragged.GetComponent<DragHandler>().nextParent = transform;
			UiController.instance.HandleDragCompletionOnTile(DragHandler.itemBeingDragged);
		}
	}
	#endregion
}
