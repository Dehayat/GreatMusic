using System;
using System.Collections;
using System.Collections.Generic;
using Rosa;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIHandler : MonoBehaviour
{

    [SerializeField] private GameData data;
    [SerializeField] private PlayerController player;
    [SerializeField] private Image healthbar;
    [SerializeField] private Canvas gameOverScreen;
    
    private Health playerHealth;
    
    private float maxHealth;
    
    public TextMeshProUGUI teethDisplayer;
    public TextMeshProUGUI multiplierDisplayer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<Health>();
        maxHealth = playerHealth.GetMaxHealth();
    }
    

    // Update is called once per frame
    void Update()
    {
        teethDisplayer.text = data.teeth.ToString();
        multiplierDisplayer.text = "x" + ((int) data.currentScoreMultiplier).ToString();
        if (playerHealth.GetHealth() <= 0)
        {
            gameOver();
        }
    }

    private void OnGUI()
    {
        healthbar.rectTransform.localScale = new Vector3(0.5f, playerHealth.GetHealth() / maxHealth, 1f);
    }

    private void gameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        TextMeshProUGUI score = gameOverScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        score.text = data.teeth.ToString();
        
    }
}

