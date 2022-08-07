using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;

public class ScarabClimb : MonoBehaviour
{
	public Transform chController;
	bool inside = false;
	public float speedUpDown = 3.2f;
	public ThirdPersonMovement TPMInput;
	Vector3 velocity;

	void Start()
	{
		TPMInput = GetComponent<ThirdPersonMovement>();
		inside = false;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Climbing Wall")
		{
			TPMInput.enabled = false;
			inside = !inside;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Climbing Wall")
		{
			TPMInput.enabled = true;
			inside = !inside;
		}
	}

	void Update()
	{
		if (inside == true && Input.GetKey("w"))
		{
			velocity.y = Mathf.Sqrt(3f * -2f * -9.81f / 2f);
			//chController.transform.position += Vector3.up / speedUpDown;
			Debug.Log("Climbing Up");
		}

		if (inside == true && Input.GetKey("s"))
		{
			chController.transform.position += Vector3.down / speedUpDown;
			Debug.Log("Climbing Down");
		}
	}
}
