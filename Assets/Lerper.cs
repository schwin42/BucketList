using UnityEngine;
using System.Collections;
using System;

public class Lerper : MonoBehaviour {

	private Transform startMarker;
	private Transform endMarker;
	public float duration = 1.0F;
	private float startTime;
	//private float journeyLength;
	[SerializeField] private float percentageComplete;
	[SerializeField] private bool lerpInProgress = false;
	private Action callback;

	public void LerpToTransform(Transform targetTransform, Action callback) {
		lerpInProgress = true;
		startTime = Time.time;
		startMarker = transform;
		endMarker = targetTransform;
		this.callback = callback;
		//journeyLength = Vector3.Distance (startMarker.position, endMarker.position);
	}

	public void TerminateLerp() {
		lerpInProgress = false;
		percentageComplete = 0.0F;
		if (callback != null) {
			callback.Invoke ();
		}
	}

	public void Update() {
		if (lerpInProgress) {
			float timeElapsed = (Time.time - startTime);
			percentageComplete = timeElapsed / duration;
			transform.position = Vector3.Lerp(startMarker.position, endMarker.position, percentageComplete);
			if(percentageComplete >= 1.0F) {
				TerminateLerp();
			}
		}
	}
}
