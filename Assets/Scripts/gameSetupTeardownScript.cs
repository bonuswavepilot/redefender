using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSetupTeardownScript : MonoBehaviour {
   public Rigidbody2D PlayerSprite;

   private scoreKeeperScript scoreKeeper;
   private Rigidbody2D player;

   void Start()
   {
      scoreKeeper = GameObject.Find("UICanvas").GetComponent<scoreKeeperScript>();
      player = Instantiate(PlayerSprite, new Vector3(0, 0, -10), Quaternion.identity) as Rigidbody2D;
   }

   void NewGame()
   {
      foreach(GameObject currentAlien in GameObject.FindGameObjectsWithTag("Enemy"))
      {
         Destroy(currentAlien);
      }

      if(player)
      {
         Destroy(player);
      }
      player = Instantiate(PlayerSprite, new Vector3(0, 0, -10), Quaternion.identity) as Rigidbody2D;

      scoreKeeper.resetScore();
   }

   public void DelayedRestart()
   {
      StartCoroutine(DoDelayedRestart());
   }

   IEnumerator DoDelayedRestart()
   {
      yield return new WaitForSecondsRealtime(5);
      NewGame();
   }

}
