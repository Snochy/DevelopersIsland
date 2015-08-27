using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class FishSim : MonoBehaviour {

	public Button nextBut;
	
	public GameObject fishPrefab;

	public GameObject fishSpawner;

	private float timer;
	public float spawnTimer;

	void Start(){
		nextBut.onClick.AddListener(() => { BackButtonClick(); });
		timer = spawnTimer;
	}

	void Update () 
	{
		timer += Time.deltaTime;
		if (timer >= spawnTimer) 
		{
			SpawnPool();
			timer = 0;
		}
	}
	
	void BackButtonClick ()
	{
		Application.LoadLevel(0);
	}

	void SpawnPool ()
	{
		int finType = Random.Range (0, 3);
		float FLengthInt = Random.Range(-100,100);
		float FWidthInt = Random.Range(-100,100);
		float BLengthInt = Random.Range(-100,100);
		float BWidthInt = Random.Range(-100,100);
		int color = Random.Range (0, 3);
		
		for (int i = 0; i < Random.Range(1,10); i++) 
		{
			GameObject clone = Instantiate (fishPrefab, new Vector3(fishSpawner.transform.position.x+ Random.Range(-3f,3f), fishSpawner.transform.position.y + Random.Range(-3f,3f), fishSpawner.transform.position.z + Random.Range(-3f,3f)), fishPrefab.transform.rotation) as GameObject;			
			clone.GetComponent<FishStats>().finInt = finType;
			clone.GetComponent<FishStats>().FLengthInt = FLengthInt;
			clone.GetComponent<FishStats>().FWidthInt = FWidthInt;
			clone.GetComponent<FishStats>().BLengthInt = BLengthInt;
			clone.GetComponent<FishStats>().BWidthInt = BWidthInt;
			clone.GetComponent<FishStats>().textureInt = color;
		}

		fishSpawner.transform.position = new Vector3(fishSpawner.transform.position.x, Random.Range(-20, 20), Random.Range(20, 40));
	}
}
