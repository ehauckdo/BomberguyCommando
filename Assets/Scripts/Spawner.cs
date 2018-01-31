using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public int totalRespawns = 10;
	public float secondsBetweenRespawns = 1.5f;
	public float initialXpos = -5.5f, initialYpos = 3.5f;
	public float tileSize = 1;

	private GameObject enemyContainer;
	private ArrayList spawnLoadouts;

	public void Start(){
		InstantiateEnemyContainer ();
	}

	public void StartLevel(Level level){
		for (int i = 0; i < level.spawnLoadouts.Count; i++) {
			StartCoroutine(SpawnLoadout((SpawnLoadout)level.spawnLoadouts [i]));
		}
	}

	IEnumerator Spawn(){
		for (int i = 0; i < totalRespawns; i++) {
			float seconds = Random.Range (secondsBetweenRespawns - 0.5f, secondsBetweenRespawns + 0.5f);
			yield return new WaitForSeconds (seconds);
			InstantiateEnemyAtRandomPosition ("Enemy1");
			//InstantiateEnemyAtFixedPosition(initialXpos + tileSize*12, initialYpos);
		}

	}

	IEnumerator SpawnLoadout(SpawnLoadout loadout){
		for (int i = 0; i < loadout.totalRespawns; i++) {
			float seconds = Random.Range (loadout.secondsBetweenRespawn - 0.5f, loadout.secondsBetweenRespawn + 0.5f);
			yield return new WaitForSeconds (seconds);
			InstantiateEnemyAtRandomPosition (loadout.enemyName, loadout.enemySpeed);
		}
		yield return new WaitForSeconds (1f);
	}


	public void InstantiateEnemyContainer(){
		enemyContainer = new GameObject ();//Instantiate (Resources.Load ("EnemyContainer", typeof(GameObject))) as GameObject;
		enemyContainer.name = "EnemyContainer";
	}

	public void InstantiateEnemyAtRandomPosition(string enemyName, float enemySpeed = 1f){
		float x_pos = initialXpos + tileSize * Random.Range (1, 13);
		Vector3 pos = new Vector3 (x_pos, initialYpos, 0f);
		//GameObject newEnemy = Instantiate(enemy, pos, transform.rotation);
		GameObject newEnemy = Instantiate (Resources.Load (enemyName, typeof(GameObject)), pos, transform.rotation) as GameObject;
		newEnemy.name = "Enemy";
		newEnemy.transform.parent = enemyContainer.transform;
		if (newEnemy.tag == "Enemy1") {
			Enemy1Controller controller = newEnemy.GetComponent < Enemy1Controller> ();
			controller.movingTime = enemySpeed;
		}
	}

	public void InstantiateEnemyAtFixedPosition(float x_pos, float y_pos){
		Vector3 pos = new Vector3 (x_pos, initialYpos, 0f);
		//GameObject newEnemy = Instantiate(enemy, pos, transform.rotation);
		GameObject newEnemy = Instantiate (Resources.Load ("Enemy1", typeof(GameObject)), pos, transform.rotation) as GameObject;
		newEnemy.name = "Enemy";
		newEnemy.transform.parent = enemyContainer.transform;
	}
		
}


