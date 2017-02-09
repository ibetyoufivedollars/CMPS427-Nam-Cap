using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

	private const float MIN_Y_ANGLE = 0.0f;
	private const float MAX_Y_ANGLE = 50.0f;

	public Transform lookAt;
	public Transform cameraTransform;
	private Camera camera;
	public float distance = 10.0f;
	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float sensitivityX = 4.0f;
	private float sensitivityY = 1.0f;


	private void Start(){
		cameraTransform = transform;
		camera = Camera.main;

	}

	private void Update(){
		currentX += Input.GetAxis ("Mouse X");
		currentY += Input.GetAxis ("Mouse Y");

		currentY = Mathf.Clamp (currentY, MIN_Y_ANGLE, MAX_Y_ANGLE);
	}
		


	private void LateUpdate(){
		Vector3 dir = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
		cameraTransform.position = lookAt.position + rotation * dir;
		cameraTransform.LookAt (lookAt.position);

	}


}