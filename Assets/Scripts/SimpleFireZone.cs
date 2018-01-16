using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;


public class SimpleFireZone : MonoBehaviour,IPointerDownHandler, IPointerUpHandler {

	private bool touched;
	private int pointerId;
	private bool canfire;

	void Awake() {
		touched = false;
	}

	public void OnPointerDown(PointerEventData data){
		if (!touched) {
			touched = true;
			pointerId = data.pointerId;
			canfire = true;
		}
	}
		

	public void OnPointerUp(PointerEventData data){
		if (data.pointerId == pointerId) {
			touched = false;
			canfire = false;
		}
	}

	public bool CanFire(){
		return canfire;
	}
}
