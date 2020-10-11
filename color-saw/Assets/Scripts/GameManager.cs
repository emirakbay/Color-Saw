using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    [System.Serializable]
    public class Level
    {
        public GameObject stage;
    }

	public Level[] levels;

	public bool advancing;
	public bool gameOver;

	public Text levelText;

	public GameObject gameOverUI;

    private GameObject curLevel;
	
	public int curLevelNumber;

    #endregion

    #region Instance
    public static GameManager Instance;

    #endregion

    #region Main Methods
	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(this.gameObject);

		Instance = this;
	}

	private void Start()
	{
		NextLevel();
	}

    #endregion

    #region Helper Methods
	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Advance()
	{
		if (advancing)
			return;

		advancing = true;

		if (curLevelNumber < levels.Length)
			NextLevel();
	}

	public void NextLevel()
	{
		curLevelNumber++;
		
		NextStage();
		RefreshUI();
	}

	public void NextStage()
	{
		if (curLevel)
			Destroy(curLevel);
		StartCoroutine(SpawnNextStage());
		RefreshUI();
	}

	public void RefreshUI()
	{
		levelText.text = "LEVEL " + curLevelNumber.ToString();
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverUI.SetActive(true);
	}

	private IEnumerator SpawnNextStage()
	{
		yield return new WaitForSeconds(1f);
		curLevel = Instantiate(levels[curLevelNumber-1].stage);
		advancing = false;
	}

    #endregion
}

