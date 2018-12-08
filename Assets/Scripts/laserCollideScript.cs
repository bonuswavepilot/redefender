using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserCollideScript : MonoBehaviour {

   void Explode() {
      Destroy(gameObject);
   }

   void OnCollisionEnter2D(Collision2D coll) {
      if(!coll.gameObject.name.Contains("Laser") && !coll.gameObject.name.Contains("Player")) {  //} != "LaserRight 2(Clone)" && coll.gameObject.name != "LaserLeft(Clone)") {
         Explode();
      }
   }
}
