using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] astroids;
	public Vector3 position;

	public Text scoreText;
//	public Text restartText;
	public Text gameOverText;
	public GameObject restartButton;
	private bool restart;
	private bool gameOver;


	private int score;
	public float astroidsPerWave;

    // Use this for initialization
    void Start () {
		restart = false;
		gameOver = false;
		gameOverText.text = "";
		restartButton.SetActive (restart);
//		restartText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine(createAstroidWaves ());
	}

/*	void Update (){
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
*/
	IEnumerator createAstroidWaves(){
		yield return new WaitForSeconds (1);
		while (true) {
            var minX = Camera.main.ScreenToWorldPoint(new Vector2(0.1f * Screen.width, 0)).x;
            var maxX = Camera.main.ScreenToWorldPoint(new Vector2(0.9f * Screen.width, 0)).x;
            for (int i = 0; i < astroidsPerWave; i++) {
                GameObject astroid = astroids[Random.Range(0, astroids.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (minX, maxX), position.y, position.z);
				Quaternion iQuaternion = Quaternion.identity;
				Instantiate (astroid, spawnPosition, iQuaternion);
				yield return new WaitForSeconds (0.5f);
			}
			yield return new WaitForSeconds (4);
			if (gameOver) {
				restart = true;
				restartButton.SetActive (restart);
				break;
			}
		}
	}

	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	public void GameOver (){
		gameOver = true;
		gameOverText.text = "Game Over";
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

	public void RestartGame(){
        Application.LoadLevel (Application.loadedLevel);
	}
}
