using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Cam : MonoBehaviour
{
	public Transform target;
	public float smoothFactor = 4;

	private void FixedUpdate()
	{
		Vector3 destination = new Vector3(target.position.x, target.position.y, transform.position.z);

		transform.position = Vector3.Lerp(
			transform.position,
			destination,
			smoothFactor * Time.deltaTime
		);
	}
}
