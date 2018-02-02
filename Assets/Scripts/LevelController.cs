using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public Text scoreText;

	private PlayerController playerController;
	private AudioController audioController;
	private GameObject[] bunnyIcons;
	private GameObject[] bombIcons;
	private Level[] levels;
	private int bombsLeft, bunniesLeft, totalBombs;
	private int currentActiveBombs, enemiesKilled;
	private int score, nextLevel, numberCompletedGames;
	private bool gameActive;

	public void Start(){
		playerController = (GameObject.FindGameObjectWithTag ("Player") as GameObject).GetComponent<PlayerController>();
		audioController = Helper.LoadAudioController ();
		LoadBombIcons ();
		LoadBunnies ();
		LoadLevels ();
		StartNextLevel ();
		gameActive = true;
	}

	void LoadBombIcons(){
		bombIcons = GameObject.FindGameObjectsWithTag ("BombIcon");
		int extraBombs = numberCompletedGames * 8;
		bombsLeft = totalBombs = bombIcons.Length + bombIcons.Length*nextLevel + extraBombs;
		//Debug.Log ("Total bombs: " + totalBombs);
	}

	void LoadBunnies(){
		bunnyIcons = GameObject.FindGameObjectsWithTag ("Bunny");
		bunniesLeft = bunnyIcons.Length;
	}

	public void PlaceBomb(Vector3 position){
		if (!gameActive || bombsLeft == 0)
			return;

		if (position.y > 3.5f || position.y < -2.5)
			return;

		if (!IsThereBlockCollision(position)) {
			Instantiate (Resources.Load ("Bomb", typeof(GameObject)), position, transform.rotation);
			currentActiveBombs += 1;
			//Debug.Log ("Total bombs: " + totalBombs + ", Bombs left: " + bombsLeft+", bombsLeft-1 % 2 = "+((bombsLeft-1) % 2).ToString());
			//Debug.Log ("Placing bomb at: (" + position.x + "," + position.y + ")");
			if(totalBombs == bombIcons.Length || (totalBombs > bombIcons.Length && (bombsLeft-1) % 2 == 0))  
				//bombIcons[bombsLeft-1].SetActive(false);
				for (int i = bombIcons.Length - 1; i >= 0; i--) {
					if (bombIcons [i].activeSelf == true) {
						bombIcons [i].SetActive (false);
						break;
					}
				}
			bombsLeft -= 1;
			playerController.ThrowBomb ();
		}
	}

	public void UpdateScore(int i){
		score = score + i;
		scoreText.text = "Score: " + score.ToString();
	}

	public void PlayerGotHit(){
		audioController.Play ("damage");
		if (bunniesLeft == 0)
			Helper.LoadSceneController().GoBackToMenu();
		else {
			bunnyIcons [bunniesLeft-1].SetActive (false);
			bunniesLeft -= 1;
		}
	}

	public void BombExploded(){
		currentActiveBombs -= 1;
	}

	public void EnemyAdded(){
		enemiesKilled += 1;
	}

	public void EnemyKilled(){
		enemiesKilled += 1;
		//Debug.Log("Total enemies killed: "+enemiesKilled+", Required: "+levels[nextLevel-1].totalEnemies);
		if (enemiesKilled == levels[nextLevel-1].totalEnemies) {
			enemiesKilled = 0;
			StartCoroutine(NextLevelTransition());
		}
	}

	void StartNextLevel(){
		bunniesLeft = bunnyIcons.Length;
		int extraBombs = numberCompletedGames * 8;
		bombsLeft = totalBombs = bombIcons.Length + bombIcons.Length*nextLevel + extraBombs;

		if (nextLevel >= levels.Length) {
			nextLevel = 0;
			numberCompletedGames += 1;
		}
		Spawner spawner = GetComponent<Spawner> ();
		spawner.StartLevel (levels[nextLevel]);
		nextLevel += 1;
	}

	IEnumerator NextLevelTransition(){
		gameActive = false;
		yield return new WaitForSeconds (1);

		ScoreDisplayer scoreDisplayer = GetComponent<ScoreDisplayer> ();
		scoreDisplayer.ShowScore ();
		yield return new WaitForSeconds (0.5f);

		for (int i = bombIcons.Length - 1; i >= 0; i--) {
			if (bombIcons [i].activeSelf == true) {
				UpdateScore (100);
				scoreDisplayer.UpdateBonusBombs(100);
				bombIcons [i].SetActive (false);
				audioController.Play ("bonus");
				yield return new WaitForSeconds (0.2f);
			}
		}

		yield return new WaitForSeconds (0.5f);

		while(bunniesLeft > 0){
			bunnyIcons [bunniesLeft-1].SetActive (false);
			bunniesLeft -= 1;
			UpdateScore (100);
			scoreDisplayer.UpdateBonusBunnies (100);
			audioController.Play ("bonus");
			yield return new WaitForSeconds (1);
		}

		yield return new WaitForSeconds (1);
		scoreDisplayer.HideScore ();

		bunniesLeft = bunnyIcons.Length;
		for (int i = 0; i < bunnyIcons.Length; i++) {
			bunnyIcons [i].SetActive (true);
		}

		bombsLeft = bombIcons.Length;
		for (int i = 0; i < bombIcons.Length; i++) {
			bombIcons [i].SetActive (true);
		}

		yield return new WaitForSeconds (2);
		gameActive = true;
		StartNextLevel ();
	}

	// Initialize each level of the game in a Level object. Each level is comprised of one or more
	// SpawnLoadout objects, which will contain what enemy, how many, and how fast they will come
	void LoadLevels(){
		levels = new Level[3];
		float moveSpeed = 1 - (numberCompletedGames * 0.1f >= 0.5f ? 0.5f : numberCompletedGames * 0.1f);
		int enemiesExtra = numberCompletedGames * 4;

		Level firstLevel = new Level ();
		firstLevel.spawnLoadouts.Add (new SpawnLoadout ("Enemy1", 15 +enemiesExtra, 1.5f, moveSpeed));
		foreach (SpawnLoadout loadout in firstLevel.spawnLoadouts) 
			firstLevel.totalEnemies += loadout.totalRespawns;

		Level secondLevel = new Level ();
		secondLevel.spawnLoadouts.Add (new SpawnLoadout ("Enemy1", 10 +enemiesExtra, 1, moveSpeed));
		secondLevel.spawnLoadouts.Add (new SpawnLoadout ("Enemy2", 10 +enemiesExtra, 1.5f, moveSpeed));
		foreach (SpawnLoadout loadout in secondLevel.spawnLoadouts) 
			secondLevel.totalEnemies += loadout.totalRespawns;

		Level thirdLevel = new Level ();
		thirdLevel.spawnLoadouts.Add (new SpawnLoadout ("Enemy1", 20 +enemiesExtra, 1, moveSpeed));
		thirdLevel.spawnLoadouts.Add (new SpawnLoadout ("Enemy2", 20 +enemiesExtra, 1.5f, moveSpeed));
		foreach (SpawnLoadout loadout in thirdLevel.spawnLoadouts) 
			thirdLevel.totalEnemies += loadout.totalRespawns;

		levels [0] = firstLevel;
		levels [1] = secondLevel;
		levels [2] = thirdLevel;
	}

	bool IsThereBlockCollision(Vector3 position){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (new Vector2(position.x, position.y), 0);
		for(int i = 0; i < hitColliders.Length; i++){
			if (hitColliders [0].gameObject.tag == "Block")
				return true;
		} 
		return false;
	}
}

public class Level{
	public ArrayList spawnLoadouts;
	public int totalEnemies;

	public Level(){
		this.spawnLoadouts = new ArrayList ();
	}
}

public class SpawnLoadout{
	public string enemyName;
	public int totalRespawns;
	public float secondsBetweenRespawn;
	public float enemySpeed;

	public SpawnLoadout(string enemyName, int totalRespawns, float secondsBetweenRespawn, float enemySpeed){
		this.enemyName = enemyName;
		this.totalRespawns = totalRespawns;
		this.secondsBetweenRespawn = secondsBetweenRespawn;
		this.enemySpeed = enemySpeed;
	}
}