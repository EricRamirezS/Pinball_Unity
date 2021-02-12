using UnityEngine;
using UnityEngine.UI;

public enum Phase {
    THROW,
    PLAYING
}

public class GameManager : MonoBehaviour {
    public int life = 2;

    public static Phase CurrentPhase = Phase.THROW;

    private static int lifes = 2;
    private static int Score = 0;

    private static Text LifeText = null;
    private static Text ScoreText = null;

    private void Awake() {
        Score = 0;
        LifeText = GameObject.FindGameObjectWithTag("Life").GetComponent<Text>();
        ScoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        lifes = life;
    }

    public static void addScore(int score) {
        int multiplier = MultiplierZone.GetMultiplier();

        Score += score * multiplier;

        ScoreText.text = Score.ToString();
    }

    public static bool loseLife() {
        if (lifes > 0) {
            lifes--;
            LifeText.text = lifes.ToString();
            return true;
        }

        return false;
    }

    public static void GameOver() {
        
    }
}