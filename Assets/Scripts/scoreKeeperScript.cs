using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreKeeperScript : MonoBehaviour {

   public Text scoreText;
   public GameObject textObject;

   private int score;
   private int alienKillScore;

   void Start()
   {
      score = 0;
      alienKillScore = 150;
      textObject = GameObject.Find("ScoreText");
      scoreText = textObject.GetComponent<Text>();
      scoreText.text = score.ToString();
   }

   void Update()
   {
      scoreText.text = score.ToString();
   }

   public void resetScore()
   {
      score = 0;
   }

   public void addKillScore()
   {
      score += alienKillScore;
   }

}
