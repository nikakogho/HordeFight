using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float regenerationSpeed = 3;
	public Color fullHealthColor = Color.green, zeroHealthColor = Color.red;
	public Image healthBar;
	public Text moneyText;
	public static int money = 0;
	public float startHealth;
	public static float health;
	public float moveSpeed, rotateSpeed, fireRate;
	private float countdown = 0;
	public GameObject bullet;
	public float bulletForce;
	public Transform firePoint;
	private Animator anim;
	private bool dead = false;
	private Rigidbody rb;

	void Awake()
	{
		bullet.GetComponent<Bullet>().damage = PlayerPrefs.GetFloat ("Damage", Shop.Damage);
		moveSpeed = PlayerPrefs.GetFloat ("WalkSpeed", moveSpeed);
		bulletForce = PlayerPrefs.GetFloat ("BulletSpeed", bulletForce);
		fireRate = PlayerPrefs.GetFloat ("FireRate", fireRate);
		startHealth = PlayerPrefs.GetFloat ("Health", startHealth);
		regenerationSpeed = PlayerPrefs.GetFloat ("Regen", regenerationSpeed);
		bullet.GetComponent<Bullet>().radius = PlayerPrefs.GetFloat ("Radius", Shop.Radius);
		money = PlayerPrefs.GetInt ("Money", 0);
		rb = GetComponent<Rigidbody> ();
		anim = GetComponentInChildren<Animator> ();
		health = startHealth;
	}

	void Update()
	{
		if (dead)
			return;
		health += regenerationSpeed * Time.deltaTime;
		health = Mathf.Clamp (health, 0, startHealth);
		healthBar.fillAmount = health / startHealth;
		healthBar.color = Color.Lerp (zeroHealthColor, fullHealthColor, health / startHealth);
		moneyText.text = "$" + money;
		if (health <= 0)
		{
			dead = true;

			Destroy (gameObject);
			return;
		}
		
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		anim.SetBool ("walking", h != 0 && v != 0);
		rb.MovePosition (rb.position + new Vector3 (h, 0, v) * moveSpeed * Time.deltaTime);



		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) 
		{
			Vector3 dir = hit.point - transform.position;
			dir.y = 0;

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), rotateSpeed * Time.deltaTime);
		}
		countdown -= Time.deltaTime;

		if (Input.GetButton ("Fire1") && countdown <= 0) 
		{
			countdown = fireRate;
			anim.SetBool ("shooting", true);
			Instantiate (bullet, firePoint.position, transform.rotation).GetComponent<Rigidbody> ().velocity = transform.forward * bulletForce;
		} else 
		{
			anim.SetBool ("shooting", false);
		}
	}
}
