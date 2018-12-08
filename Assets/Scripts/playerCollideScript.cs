using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollideScript : MonoBehaviour {
   public ParticleSystem Splosion;
   public AudioClip explosionSound;

   private gameSetupTeardownScript gameSetup;
   private GameObject gameAudioObject;
   private AudioSource gameAudio;
   private SpriteRenderer spriteRenderer;

   void Start() {
      gameSetup = GameObject.Find("Misc").GetComponent<gameSetupTeardownScript>();
      gameAudioObject = GameObject.Find("GameAudio");
      gameAudio = gameAudioObject.GetComponent<AudioSource>();
      spriteRenderer = GetComponent<SpriteRenderer>();
   }

   void Explode() {
      Vector3 shipPosition = transform.position;
      ParticleSystem explosionParticleSystem = Instantiate(Splosion, shipPosition, transform.rotation) as ParticleSystem;

      explosionParticleSystem.Play();
      gameAudio.PlayOneShot(explosionSound);

      spriteRenderer.sprite = null;
      gameSetup.DelayedRestart();
      Destroy(gameObject);
   }

   void OnCollisionEnter2D(Collision2D coll) {
      // Debug.Log(coll.gameObject.name);

      if(!coll.gameObject.name.Contains("Laser")) {  //} != "LaserRight 2(Clone)" && coll.gameObject.name != "LaserLeft(Clone)") {
         Explode();
      }
   }

}
