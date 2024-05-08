using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    //public [] currState
    [SerializeField] private float startSpeed;
    [SerializeField] private float accelSpeed;
    [SerializeField] private float currSpeed;

    [SerializeField] private TextMeshProUGUI gameoverText;

    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private float score = 0;
    public float scoreMultiplier = 1;
    public float multiplierIncreaseFactor = 0.01f;

    private void Awake()
    {
        // Ensure this is the only instance. If not, destroy self.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO: prep and set game states
        
        currSpeed = startSpeed;
    }

    private void Update()
    {
        scoreUI.text= "Score: " + (Mathf.Round(score * 10) / 10.0f);
    }

    void FixedUpdate()
    {
        currSpeed += accelSpeed * Time.fixedDeltaTime;
        //print(currSpeed);

        score += scoreMultiplier * Time.fixedDeltaTime;
        scoreMultiplier += multiplierIncreaseFactor * Time.fixedDeltaTime;
    }

    public float getCurrSpeed()
    {
        return currSpeed;
    }

    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        print("Player dead");
        currSpeed = 0;
        accelSpeed = 0;
        gameoverText.gameObject.SetActive(true);
        Time.timeScale = 0;
        //resetGame();
    }
}
