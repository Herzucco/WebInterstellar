using UnityEngine;
using System.Collections;

public class B2Dragger : MonoBehaviour
{
	Vector2 targetPos = Vector2.zero; // the desired position
	public float maxForce = 100f; // the max force available
	public float pGain = 20f; // the proportional gain
	public float iGain = 0.5f; // the integral gain
	public float dGain = 0.5f; // differential gain
	public float maxDistance = 10;
	public Transform player;
	private Vector2 integrator = Vector2.zero; // error accumulator
	private Vector2 lastError = Vector2.zero; 
	private Vector2 curPos = Vector2.zero; // actual Pos
	private Vector2 force = Vector2.zero; // current force
	
	private int layerMask = 1 << 8;
	private Vector3 mousePos;
	private Vector3 mousePosOnMouseDown;
	private Vector3 clickOffset;

	
	void Start(){
	}
	
	void FixedUpdate()
	{
		if(Input.GetMouseButton(0)){
			moveObject();
		}
	}
	
	void Update (){
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z=0;
		
		if (Input.GetMouseButtonDown (0)){
			calculateClickOffset();
			
		}
	}

	void calculateClickOffset(){
		mousePosOnMouseDown = new Vector3(mousePos.x,mousePos.y,-10);
		clickOffset = mousePosOnMouseDown-transform.position;//Distance from movable object to mousePos
	}
	
	void moveObject(){
		if(player != null && Vector3.Distance(player.position, transform.position) <= maxDistance){
			targetPos=mousePos-clickOffset;
			curPos = transform.position;
			Vector2 error = targetPos - curPos; // generate the error signal
			integrator += error * Time.deltaTime; // integrate error
			Vector2 diff = (error - lastError)/ Time.deltaTime; // differentiate error
			lastError = error;
			// calculate the force summing the 3 errors with respective gains:
			force = error * pGain + integrator * iGain + diff * dGain;
			// clamp the force to the max value available
			force = Vector3.ClampMagnitude(force, maxForce);
			
			// apply the force to accelerate the rigidbody:
			//movableObject.rigidbody2D.AddForce(force);
			
			rigidbody2D.AddForce(force);
			//movableObject.rigidbody2D.AddForceAtPosition(force,mousePos);
		}
	}
	
	
}