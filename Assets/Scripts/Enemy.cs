using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public enum AnimType { ator, ation };
	public AnimType animType;
	public int price;
	public float health, damage, hitSpeed, nearRange, attackRange, walkSpeed, runSpeed;
	private bool dead = false;
	private float countdown = 0;
	private NavMeshAgent agent;
	private Animator anim;
	private Animation Anim;
	private Transform player;

	void Awake()
	{
		GameObject check = GameObject.FindGameObjectWithTag ("Player");
		if (check == null)
			return;
		anim = GetComponent<Animator> ();
		Anim = GetComponent<Animation> ();
		player = check.transform;
		agent = GetComponent<NavMeshAgent> ();
	}

	void FixedUpdate()
	{
		if (player == null)
			return;
		if (dead)
			return;

		if (health <= 0) 
		{
			dead = true;
			if (animType == AnimType.ator) 
			{
				anim.SetTrigger ("Die");
				anim.SetBool ("Dead", true);
			} else 
			{
				Anim.Play ("Die");
			}
			agent.Stop ();
			Collider[] cols = GetComponents<Collider> ();
			foreach (Collider col in cols) 
			{
				col.enabled = false;
			}
			Destroy (gameObject, 5);
			Player.money += price;
			return;
		}

		agent.SetDestination (player.position);

		Vector3 dir = player.position - transform.position;
		float dist = dir.magnitude;

		countdown -= Time.deltaTime;

		if (dist <= attackRange) 
		{
			agent.speed = 0;
			if (countdown <= 0)
			{
				countdown = hitSpeed;
				if (animType == AnimType.ator)
					anim.SetTrigger ("Hit");
				else 
				{
					Anim.Play ("Hit");;
				}
				Player.health -= damage;
			}
		} else if (dist <= nearRange)
		{
			agent.speed = runSpeed;
		} else 
		{
			agent.speed = walkSpeed;
		}
		if (animType == AnimType.ator)
			anim.SetBool ("near", dist <= nearRange);
		else 
		{
			if (!Anim.isPlaying) 
			{
				Anim.Play ("Walk");
			}
		}
	}

	public void ApplyDamage(float Damage)
	{
		health -= Damage;
		if (animType == AnimType.ator)
			anim.SetTrigger ("GetHit");
		else 
		{
			if (health > 0) 
			{
				Anim.Play ("GetHit");
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, nearRange);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, attackRange);
	}
}
