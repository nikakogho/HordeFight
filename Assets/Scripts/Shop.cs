using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour 
{
	public string gameSceneName;
	public Text moneyText;
	public shopItem damage, walkSpeed, bulletSpeed, fireRate, health, regen, radius;
	public static float Damage, WalkSpeed, BulletSpeed, FireRate, Health, Regen, Radius;
	public float _damage, _walkSpeed, _bulletSpeed, _fireRate, _health, _regen, _radius;
	public static int money;

	void Awake()
	{
		money = PlayerPrefs.GetInt ("Money", 0);
		moneyText.text = "$" + money;

		Damage = PlayerPrefs.GetFloat ("Damage", _damage);
		WalkSpeed = PlayerPrefs.GetFloat ("WalkSpeed", _walkSpeed);
		BulletSpeed = PlayerPrefs.GetFloat ("BulletSpeed", _bulletSpeed);
		FireRate = PlayerPrefs.GetFloat ("FireRate", _fireRate);
		Health = PlayerPrefs.GetFloat ("Health", _health);
		Regen = PlayerPrefs.GetFloat ("Regen", _regen);
		Radius = PlayerPrefs.GetFloat ("Radius", _radius);
	}

	public void Choose(string chosen)
	{
	    if (chosen == health.name) 
		{
			if (money < health.price)
				return;
		    health.Buy ();
			Health *= 1.25f;
		}
		else if (chosen == regen.name) 
		{
			if (money < regen.price)
				return;
			regen.Buy ();
			Regen *= 1.2f;
		}
		else if (chosen == walkSpeed.name) 
		{
			if (money < walkSpeed.price)
				return;
			walkSpeed.Buy ();
			WalkSpeed *= 1.15f;
		}
		else if (chosen == bulletSpeed.name) 
		{
			if (money < bulletSpeed.price)
				return;
			bulletSpeed.Buy ();
			BulletSpeed *= 1.2f;
		}
		else if (chosen == fireRate.name) 
		{
			if (money < fireRate.price)
				return;
			fireRate.Buy ();
			FireRate *= 0.925f;
		}
		else if (chosen == damage.name) 
		{
			if (money < damage.price)
				return;
			damage.Buy ();
			Damage *= 1.5f;
		}
		else if (chosen == radius.name) 
		{
			if (money < radius.price)
				return;
			radius.Buy ();
			Radius *= 1.25f;
		}

		moneyText.text = "$" + money;
	}

	public void Exit()
	{
		Application.Quit ();
	}

	public void Reset()
	{
		PlayerPrefs.DeleteAll ();

		damage.price = damage.startPrice;
		health.price = health.startPrice;
		fireRate.price = fireRate.startPrice;
		walkSpeed.price = walkSpeed.startPrice;
		radius.price = radius.startPrice;
		bulletSpeed.price = bulletSpeed.startPrice;
		regen.price = regen.startPrice;

		Damage = _damage;
		Radius = _radius;
		WalkSpeed = _walkSpeed;
		Health = _health;
		BulletSpeed = _bulletSpeed;
		FireRate = _fireRate;
		Regen = _regen;

		money = 0;
		moneyText.text = "$0";
	}

	public void Save()
	{
		PlayerPrefs.SetFloat ("Damage", Damage);
		PlayerPrefs.SetFloat ("WalkSpeed", WalkSpeed);
		PlayerPrefs.SetFloat ("BulletSpeed", BulletSpeed);
		PlayerPrefs.SetFloat ("FireRate", FireRate);
		PlayerPrefs.SetFloat ("Health", Health);
		PlayerPrefs.SetFloat ("Regen", Regen);
		PlayerPrefs.SetFloat ("Radius", Radius);
	}

	public void Play()
	{ 
		SceneManager.LoadScene (gameSceneName);
	}
}
