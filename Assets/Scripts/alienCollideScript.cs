using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alienCollideScript : MonoBehaviour {
   public ParticleSystem Splosion;
   public AudioClip explosionSound;

   private scoreKeeperScript scoreKeeper;
   private GameObject gameAudioObject;
   private AudioSource gameAudio;

   void Start() {
      scoreKeeper = GameObject.Find("UICanvas").GetComponent<scoreKeeperScript>();
      gameAudioObject = GameObject.Find("GameAudio");
      gameAudio = gameAudioObject.GetComponent<AudioSource>();
   }

   void Explode() {
      Vector3 alienPosition = transform.position;
      ParticleSystem explosionParticleSystem = Instantiate(Splosion, alienPosition, transform.rotation) as ParticleSystem;

      explosionParticleSystem.Play();
      gameAudio.PlayOneShot(explosionSound);
      Destroy(gameObject);

      scoreKeeper.addKillScore();
   }

   void OnCollisionEnter2D(Collision2D coll) {
      // Debug.Log(coll.gameObject.name);

      if(coll.gameObject.name.Contains("Laser")) {  //} != "LaserRight 2(Clone)" && coll.gameObject.name != "LaserLeft(Clone)") {
         Explode();
      }
   }
}
