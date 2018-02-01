using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    private Vector3 startPosition;
    public float bgScrollSpeed;
    public float tileSizeZ;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
        float newposition = Mathf.Repeat(Time.time * bgScrollSpeed,tileSizeZ);
        transform.position = startPosition + newposition * Vector3.forward;
	}
}
