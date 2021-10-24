using UnityEngine;
using UnityEngine.UI;

public class shopItem : MonoBehaviour {

	public string name;
	public int startPrice;
	[HideInInspector]public int price;
	private Button button;
	private Text text;

	void Awake()
	{
		button = GetComponent<Button> ();
		text = GetComponentInChildren<Text> ();
		text.text = name + "\n" + price;
		price = PlayerPrefs.GetInt (name + "Price", startPrice);
	}

	void Update()
	{
		text.text = name + "\n" + price;
	}

	public void Buy()
	{
		Shop.money -= price;
		price *= 2;
		PlayerPrefs.SetInt ("Money", Shop.money);
		PlayerPrefs.SetInt (name + "Price", price);
	}
}
