﻿
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SwarmFormation : MonoBehaviour
    {
        private Spline Spline;
        public List<GameObject> ITweenPathGameObjects;

        private GameController _gameControllerObject;
        public bool InFormation;

        void Start()
        {
            float t = 0;

            var gameControllerObject = GameObject.FindGameObjectWithTag("GameController");

            if (gameControllerObject != null)
            {
                _gameControllerObject = gameControllerObject.GetComponent<GameController>();
            }
            if (_gameControllerObject == null)
            {
                Debug.Log("Cannot find 'GameController' script");
            }

            Spline = new Spline();
            var random = new System.Random();
            var splineNumber = random.Next(1, 3);
            float offset = 0
                ;
            if (gameObject.tag == "EnemyRed")
            {
                splineNumber = 0;
                offset = 0.5f;
            }
            var path = ITweenPathGameObjects[splineNumber].GetComponent<iTweenPath>();

            Spline.AddKeyframe(-1, path.nodes[0]);

            foreach (var vector3 in path.nodes)
            {
                Spline.AddKeyframe(t++, vector3);
            }
            var vector = path.nodes[path.nodes.Count - 1];
            if (_gameControllerObject != null && _gameControllerObject.Positions != null && _gameControllerObject.Positions[splineNumber].Count > 0)
            {
                var pos = _gameControllerObject.Positions[splineNumber].Pop();
                vector.x += (pos + (pos * offset));
            }

            Spline.AddKeyframe(t++, vector);

            Spline.AddKeyframe(t, path.nodes[path.nodes.Count - 1]);


        }

        private void Update()
        {
            if (GameObjectController.IsConnected() && !Network.isServer) return;

            if (!Spline.End)
            {
                gameObject.transform.position = Spline.GetPosition();
                Spline.Update(Time.deltaTime);
            }
            else
                InFormation = true;

        }
    }
}
