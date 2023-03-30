using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
public class Player_Score : MonoBehaviour
{
    public static Player_Score Instance { get; private set; }

    public float timeLeft = 120;
    public int playerScore = 0;
    public int coins = 0;
    public int enemyDamage = 0;

    [Header("UI Inventaire")]
    public TextMeshProUGUI timeLeftUI;
    public TextMeshProUGUI playerScoreUI;
    public TextMeshProUGUI coinsUI;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
}

     public void AddPoints(int points)
    {
        playerScore += points;
        playerScoreUI.text = "Score : " + playerScore;
    }

    public void AddCoin()
    {
        coins++;
        coinsUI.text = "Dechets : " + coins;

        if (coins == 10000)
        {
            coins = 0;
        }
        playerScore += 100;
        playerScoreUI.text = "Score : " + playerScore;
    }
 
  void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLeftUI.text = "Temps : " + Mathf.RoundToInt(timeLeft);
         // Mettre à jour le score et le nombre de pièces collectées
    playerScoreUI.text = "Score : " + playerScore;
    coinsUI.text = "Déchets : " + coins;
       if (timeLeft < 0.1f )
       {
           GameManager.Instance.ResetLevel();
       }
    }

   public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("dechets"))
        {
            AddCoin();
        }else if (other.gameObject.CompareTag("ennemy"))
        {
            playerScore -= enemyDamage;
            playerScoreUI.text = "Score : " + playerScore;
        }
        
    }
}

