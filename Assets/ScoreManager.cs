using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public int score;
    public float timeRemaining = 10;
    public float minComboAmount = 10;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TileSpawner spawnManager;
    [SerializeField] private TextAnimator textAnimator;
    [SerializeField] private GameObject gameOverText;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(ReduceTime());
    }
    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
    void Update()
    {
        scoreText.text = score.ToString();
        timeText.text = timeRemaining.ToString();
        if (timeRemaining <= 0)
        {
            gameOverText.SetActive(true);
            StopAllCoroutines();
            spawnManager.StopAllCoroutines();
        }
    }

    int counter;
    int comboCounter;
    public void IncreaseScore(int amount, Vector3 pos)
    {
        counter++;
        textAnimator.CreateCombo(pos, counter);
        if (score > 100 / 5 * minComboAmount)
        {
            comboCounter++;
            if (comboCounter >= 3)
            {
                minComboAmount = minComboAmount + 1;
                comboCounter = 0;
            }
        }
        if (counter >= minComboAmount)
        {
            timeRemaining++;
            textAnimator.CreateSecond(pos);
            if (spawnManager.spawnTime > 0.035f)
            {
                spawnManager.spawnTime = spawnManager.spawnTime - 0.007f;
            }
            counter = 0;
        }
        score = score + amount;
    }

    IEnumerator ReduceTime()
    {
        yield return new WaitForSeconds(1f);
        timeRemaining--;
        StartCoroutine(ReduceTime());
    }
}
