using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Highlighter : MonoBehaviour
{

	public float ACTIVE_ALPHA = .3F;
	private Image uiHierarchy_SelectedBorder;
	private MonoAttribute monoAttribute;

	void Awake () {
		uiHierarchy_SelectedBorder = transform.FindChild ("SelectedBorder").GetComponent<Image> ();
		monoAttribute = GetComponent<MonoAttribute> ();
		//print (monoAttribute);
		enableHighlight = false;
	}

	void Start ()
	{

	}

	private bool _highlightEnabled;

	public bool enableHighlight {
		get {
			return _highlightEnabled;
		}
		set {
			//print ("enable highlight");

			if (value == true) { //On enable, set alpha and Attribute color
				//print (transform.parent.GetComponent<MonoAttribute>());
				Color attributeColor = monoAttribute.attribute.type_DesireColor.ToColor ();
				uiHierarchy_SelectedBorder.color = new Color (attributeColor.r, 
				                                              attributeColor.g, 
				                                              attributeColor.b, 
				                                             ACTIVE_ALPHA);
			} else { //On disable, zero out alpha
				uiHierarchy_SelectedBorder.color = new Color (0, 0, 0, 0);
			}
			//uiHierarchy_SelectedBorder.SetActive(value);
			_highlightEnabled = value;
		}
	}
}
