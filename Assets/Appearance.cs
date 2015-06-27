using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DesireColor {
	Red = 0,
	Orange = 1,
	Yellow = 2,
	Green = 3,
	Cyan = 4,
	Blue = 5,
	Purple = 6,
	Magenta = 7
}

public class Appearance : MonoBehaviour {

	private static Appearance _instance;
	public static Appearance instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<Appearance>();
			}
			return _instance;
		}
	}

	void Awake () {
		_instance = this;
	}

	public List<Color> inspector_reasonColors = new List<Color>(7);

}
