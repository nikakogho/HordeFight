using UnityEngine;

public class CameraFolloow : MonoBehaviour {

	private Transform player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void FixedUpdate()
	{
		if (player == null)
			return;
		transform.position = new Vector3 (player.position.x, transform.position.y, player.position.z);
	}
}
