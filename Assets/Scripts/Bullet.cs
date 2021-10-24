using UnityEngine;

public class Bullet : MonoBehaviour {

	public float lifeTime = 5;
	public float damage;

	[Header("Explosion")]
	public bool explode;
	public float radius;
	public LayerMask harm;

	void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag ("Bullet") && !other.CompareTag ("Player")) 
		{
			if (explode)
				Explode ();
			if (other.CompareTag ("Enemy")) 
			{
				if(!explode)
				{
					other.GetComponent<Enemy> ().ApplyDamage (damage);
				}
			}
			Destroy (gameObject);
		}
	}

	void Explode()
	{
		Collider[] cols = Physics.OverlapSphere (transform.position, radius, harm);

		foreach (Collider col in cols) 
		{
			col.GetComponent<Enemy> ().ApplyDamage (damage);
		}
	}

	void Start()
	{
		Destroy (gameObject, lifeTime);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}
