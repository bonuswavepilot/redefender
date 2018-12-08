 using UnityEngine;
 using System.Collections;

 public class particleSystemSelfDestructScript : MonoBehaviour
 {
     private ParticleSystem explosionParticles;

     public void Start()
     {
         explosionParticles = GetComponent<ParticleSystem>();
     }

     public void Update()
     {
         if(explosionParticles)
         {
             if(!explosionParticles.IsAlive())
             {
                 Destroy(gameObject);
             }
         }
     }
 }
