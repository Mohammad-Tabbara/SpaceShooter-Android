using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasionManeuver : MonoBehaviour {


    public Vector2 startWait;
    public float dodge;
    public float tilt;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public float smoothing;
    public Boundary boundary;

    private float targetManeuver;
    private Rigidbody rb;
    private float currentSpeed;

	void Start () {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x,startWait.y)) ;
        while (true)
        {
            targetManeuver = Random.Range(1,dodge) * - Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x,maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverTime.x,maneuverTime.y));
        }
    }
	
	void FixedUpdate () {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x,targetManeuver,Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver,0.0f,currentSpeed);
        var minX = Camera.main.ScreenToWorldPoint(new Vector2(0.1f * Screen.width, 0)).x;
        var maxX = Camera.main.ScreenToWorldPoint(new Vector2(0.9f * Screen.width, 0)).x;
        rb.position = new Vector3
        (
                Mathf.Clamp(rb.position.x, minX, maxX),
                0.0f,
                rb.position.z
        );

        rb.rotation = Quaternion.Euler(0.0f,0.0f,rb.velocity.x * - tilt);
    }
}
