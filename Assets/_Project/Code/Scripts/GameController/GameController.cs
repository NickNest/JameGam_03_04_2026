using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Scripts.GameController
{
    public class GameController : MonoBehaviour {

        private List<IManualAwake> _manualAwakes = new();

        public void ManualAwake(List<IManualAwake> manualAwakes) {
            _manualAwakes = manualAwakes;
        }

        public void ManualStart() {

        }

        public void Update()
        {
            foreach (var manualAwake in _manualAwakes)
            {
                manualAwake.ManualAwake(Time.deltaTime);
            }
        }
    }
}