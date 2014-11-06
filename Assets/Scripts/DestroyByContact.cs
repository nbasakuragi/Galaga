﻿using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyByContact : MonoBehaviour
    {

        public GameObject Explosion;
        public GameObject PlayerExplosion;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("WorldBoundary") || other.tag == "Enemy")
                return;

            if (other.tag.Equals("Player"))
                GameObjectController.Instantiate(PlayerExplosion, other.transform.position, other.transform.rotation);

            try
            {
                if (Explosion != null)
                    GameObjectController.Instantiate(Explosion, transform.position, transform.rotation);
                
                Debug.Log(other.name);
                GameObjectController.Destroy(gameObject);
                GameObjectController.Destroy(other.gameObject);
                Debug.Log(string.Format("Destroyed By Contact - {0} Collided with {1}", gameObject.name, other.gameObject.name));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
