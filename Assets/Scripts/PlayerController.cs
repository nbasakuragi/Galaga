﻿using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public Boundary Boundary;
        public float Speed;
        public float Tilt;
        public GameObject Shot;
        public Transform ShotSpawn;
        public int Lives;
        public float FireRate;
        private float _nextFire;



        /// <summary>
        /// This method is called automatically by unity at each physics step. Code executed once per physics step.
        /// </summary>
        void FixedUpdate()
        {
            if (GameObjectController.IsConnected() && !networkView.isMine) return;

            //I'm only using the horizontal component. GetAxis returns only 0-1, so movement is per unit.
            var moveHorizontal = Input.GetAxis("Horizontal");


            var movement = new Vector3(moveHorizontal, 0, 0);
            rigidbody.velocity = movement * Speed;

            rigidbody.position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, Boundary.Xmin, Boundary.Xmax),
                0,
                0
           );

            rigidbody.rotation = Quaternion.Euler(.0f, .0f, -rigidbody.velocity.x * Tilt);

        }

        /// <summary>
        /// Updates every frame our scene.
        /// </summary>
        private void Update()
        {
            if (GameObjectController.IsConnected() && !networkView.isMine)
            {
                enabled = false;
                return;
            }

            if (Input.GetKeyDown(KeyCode.T) && Time.time > _nextFire)
            {
                _nextFire = Time.time + FireRate;
                var clone = GameObjectController.Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation, ignoreServerCheck: true);
                audio.Play();
            }

        }
    }
}
