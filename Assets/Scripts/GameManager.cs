using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject endUI, pauseUI;
	public string shopName;
	public GameObject player;
	private bool paused = false;

	void Update()
	{
		if (player == null && !endUI.activeSelf) 
		{
			PlayerPrefs.SetInt ("Money", Player.money);
			endUI.SetActive (true);
		}

		if (!endUI.activeSelf && Input.GetKeyDown("p")) 
		{
			Toggle (!paused);
		}
	}

	void Toggle(bool state)
	{
		paused = state;
		pauseUI.SetActive (paused);

		if (paused) 
		{
			Time.timeScale = 0;
		} else 
		{
			Time.timeScale = 1;
		}
	}

	public void GoToShop()
	{
		Toggle (false);
		SceneManager.LoadScene (shopName);
	}

	public void Restart()
	{
		Toggle (false);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Exit()
	{
		Toggle (false);
		Application.Quit ();
	}
}
