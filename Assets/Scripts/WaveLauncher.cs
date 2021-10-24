using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveLauncher : MonoBehaviour {

	public Text waveText, countText, amountText;
	public wave[] waves;
	public Transform parentSpawner;
	private Transform[] spawnPoints;
	private int index = 0;
	private float countdown = 0;
	private List<GameObject> enemies;

	void Awake()
	{
		InvokeRepeating ("EnemyCheck", 2, 2);
		enemies = new List<GameObject> ();
		spawnPoints = new Transform[parentSpawner.childCount];

		for (int i = 0; i < spawnPoints.Length; i++) 
		{
			spawnPoints [i] = parentSpawner.GetChild (i);
		}
	}

	void EnemyCheck()
	{
		for (int i = 0; i < enemies.Count; i++) 
		{
			if (enemies [i] == null) 
			{
				enemies.RemoveAt (i);
			}
		}
	}

	void Update()
	{
		if (index < waves.Length) 
		{
			amountText.text = waves [index].amount.ToString () + " to spawn";
			countText.text = string.Format ("{00:00.00}", Mathf.Clamp (countdown, 0, countdown));
			waveText.text = "Wave " + (index + 1);
			wave currentWave = waves [index];

			if (currentWave.amount > 0)
			{
				countdown -= Time.deltaTime;

				if (countdown <= 0) 
				{
					countdown = currentWave.spawnTime;

					GameObject g = currentWave.enemies [Random.Range (0, currentWave.enemies.Length)];
					Transform t = spawnPoints [Random.Range (0, spawnPoints.Length)];

					enemies.Add(Instantiate (g, t.position, Quaternion.identity));

					currentWave.amount--;
				}
			} else if(enemies.Count == 0)
			{
				index++;
			}
		}
	}

}
