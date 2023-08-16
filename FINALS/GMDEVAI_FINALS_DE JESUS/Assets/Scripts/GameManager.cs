using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //timer stuff
    float beginningTimer = 4;
    int beginTimer = 0;

    float returnToMenuTimer = 4;
    int intReturnToMenuTimer = 0;

    float cooldown = 120.0f;
    int cooldownInt = 0;

    //freeze stuff
    public float numberOfFrozenAI = 0;

    //ui stuff
    public TextMeshProUGUI numberOfFrozenAiText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;

    //reference to player and AI
    public GameObject player;
    public GameObject[] hiders;

    //menu manager
    public SceneManagement sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

        player.GetComponent<FirstPersonMovement>().enabled = false;
        
        for (int i = 0; i < hiders.Length; i++)
        {
            hiders[i].GetComponent<AIControl>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        numberOfFrozenAiText.text = numberOfFrozenAI.ToString();

        //timer commands
        timerText.text = beginTimer.ToString();
        beginningTimer -= Time.deltaTime;
        beginTimer = ((int)beginningTimer);

        StartGame();

        if (numberOfFrozenAI == 20 && cooldown <= 0)
        {
            timerText.text = intReturnToMenuTimer.ToString();
            returnToMenuTimer -= Time.deltaTime;
            intReturnToMenuTimer = ((int)returnToMenuTimer);

            winText.gameObject.SetActive(true);

            if (intReturnToMenuTimer == 0)
            {
                sceneManager.GoToStartMenuScene();
            }
        }

        if (numberOfFrozenAI < 20 && cooldown <= 0)
        {
            timerText.text = intReturnToMenuTimer.ToString();
            returnToMenuTimer -= Time.deltaTime;
            intReturnToMenuTimer = ((int)returnToMenuTimer);

            loseText.gameObject.SetActive(true);

            if (intReturnToMenuTimer == 0)
            {
                sceneManager.GoToStartMenuScene();
            }
        }
    }

    void StartGame()
    {
        if (beginningTimer <= 0)
        {
            //enable player and AI
            player.GetComponent<FirstPersonMovement>().enabled = true;

            for (int i = 0; i < hiders.Length; i++)
            {
                hiders[i].GetComponent<AIControl>().enabled = true;
            }

            //Start Game Timer
            timerText.text = cooldownInt.ToString();
            cooldown -= Time.deltaTime;
            cooldownInt = ((int)cooldown);
        }
    }
}
