using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageShader : MonoBehaviour {
	
	public Color activeColor;
	private Color normalColor;
	private Image image;

	void Awake () {
		GetComponent<Toggle> ().onValueChanged.AddListener (OnValueChanged);
	}

	void Start () {
		image = GetComponent<Image> ();
		normalColor = image.color;
	}

	void OnValueChanged (bool isOn)
	{
		if (isOn) {
			image.color = activeColor;
		} else {
			image.color = normalColor;
		}
	}
}
