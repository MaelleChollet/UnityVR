using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Référence UI")]
    public TextMeshProUGUI scoreText;

    [Header("Paramètres")]
    public int pointsPerHit = 1;

    private int score = 0;
    private int targetsSpawned = 0; 

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        UpdateDisplay();
    }

    public void AddPoint()
    {
        score += pointsPerHit;
        UpdateDisplay();
    }

    
    public void RegisterTargetSpawned()
    {
        targetsSpawned++;
        UpdateDisplay();
    }

    public void ResetScore()
    {
        score = 0;
        targetsSpawned = 0;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score + " / " + targetsSpawned;
        }
    }
}