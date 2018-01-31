using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour {

	public Text textBonus;
	public Text textBonusBombs;
	public Text textBonusBunnies;

	public GameObject bomb, bunny;

	ArrayList bombs, bunnies;

	int bonusBombs;
	int bonusBunnies;

	void Start(){
		bombs = new ArrayList ();
		bunnies = new ArrayList ();
	}

	public void ShowScore(){
		textBonus.gameObject.SetActive(true);
		textBonusBombs.gameObject.SetActive(true);
		textBonusBunnies.gameObject.SetActive(true);
	}

	public void HideScore(){
		textBonus.gameObject.SetActive(false);
		textBonusBombs.gameObject.SetActive(false);
		textBonusBunnies.gameObject.SetActive(false);
		bonusBombs = 0;
		textBonusBombs.text = bonusBombs.ToString ();
		bonusBunnies = 0;
		textBonusBunnies.text = bonusBunnies.ToString ();
		foreach(GameObject go in bombs)
			Destroy(go);
		bombs.Clear ();
		foreach(GameObject go in bunnies)
			Destroy(go);
		bunnies.Clear ();
	}

	public void UpdateBonusBombs(int bonus){
		bonusBombs += bonus;
		textBonusBombs.text = bonusBombs.ToString ();
		Vector3 position = textBonusBombs.gameObject.transform.position;
		position.x += bombs.Count * 0.3f;
		GameObject go = Instantiate (bomb, position, transform.rotation) as GameObject;
		go.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		bombs.Add (go);
	}

	public void UpdateBonusBunnies(int bonus){
		bonusBunnies += bonus;
		textBonusBunnies.text = bonusBunnies.ToString ();
		Vector3 position = textBonusBunnies.gameObject.transform.position;
		position.x += bunnies.Count * 0.3f;
		GameObject go = Instantiate (bunny, position, transform.rotation) as GameObject;
		go.transform.localScale -= new Vector3 (0.5f, 0.5f, 0.5f);
		bunnies.Add (go);
	}
}
