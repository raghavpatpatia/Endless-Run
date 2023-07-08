using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] PlayerController playerControllerScript;
    [SerializeField] Transform startingPoint;
    [SerializeField] float lerpSpeed;

    private int score;
    private IEnumerator playerIntro;

    void Start()
    {
        score = 0;
        playerControllerScript.isGameOver = true;
        if (playerIntro != null)
        {
            StopCoroutine(playerIntro);
        }
        playerIntro = PlayIntro();
        StartCoroutine(playerIntro);
    }

    private void Score()
    {
        score++;
        if (!playerControllerScript.isGameOver)
        {
            scoreText.text = "Score: " + score;
            //Debug.Log("Score: " + score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Score();        
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        playerControllerScript.playerAnim.SetFloat("Speed_Multiplier", 0.5f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        playerControllerScript.playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScript.isGameOver = false;
    }
}
