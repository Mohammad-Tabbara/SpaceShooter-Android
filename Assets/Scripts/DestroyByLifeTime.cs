using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByLifeTime : MonoBehaviour {

	public float lifeTime;
	void Start()
	{
		Destroy (gameObject, lifeTime);
	}
}
