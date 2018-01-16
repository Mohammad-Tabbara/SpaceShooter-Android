using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	private Rigidbody rb;
	private AudioSource audioSource;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireDelta;
	public SimpleTouchPad touchPad;
	public SimpleFireZone fireZone;

	private float nextFire;

    void Start(){
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}
	void Update ()
	{
		if (fireZone.CanFire() && Time.time > nextFire)
		{
			nextFire = Time.time + fireDelta;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}
	}
	void FixedUpdate(){
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");

//		Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical);

		Vector2 direction = touchPad.GetDirection ();
		Vector3 movement = new Vector3 (direction.x,0.0f,direction.y);
		rb.velocity = movement*speed;
        var minX = Camera.main.ScreenToWorldPoint(new Vector2(0.1f * Screen.width, 0)).x;
        var maxX = Camera.main.ScreenToWorldPoint(new Vector2(0.9f * Screen.width, 0)).x;
        rb.position = new Vector3
		(
				Mathf.Clamp(rb.position.x,minX,maxX),
				0.0f,
				Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax)
		);
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x*-tilt);
		Debug.Log (rb.velocity.x);
	}
}
