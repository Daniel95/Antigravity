using UnityEngine;
 using System.Collections;
 
 public class ParticleSystemAutoDestroy : MonoBehaviour 
 {
     private ParticleSystem particle;
 
 
     public void Start() 
     {
         particle = GetComponent<ParticleSystem>();
     }
 
     public void Update() 
     {
        if (!particle.IsAlive())
        {
            Destroy(gameObject);
        }
     }
 }