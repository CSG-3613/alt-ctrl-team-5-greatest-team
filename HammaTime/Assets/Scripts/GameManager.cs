using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    //public [] currState
    [SerializeField] private float startSpeed;
    [SerializeField] private float accelSpeed;
    [SerializeField] private float currSpeed;

    [SerializeField] private TextMeshProUGUI gameoverText;

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

    void FixedUpdate()
    {
        currSpeed += accelSpeed;
        //print(currSpeed);
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
