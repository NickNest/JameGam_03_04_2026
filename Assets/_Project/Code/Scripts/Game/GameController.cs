using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Scripts.Game
{
    public class GameController : MonoBehaviour {
        
        private List<IManualUpdate> _manualUpdates = new();

        public void ManualAwake(List<IManualUpdate> manualUpdates) {
            _manualUpdates = manualUpdates;

            ManualStart();
        }

        private void ManualStart()
        {
        }

        public void Update()
        {
            foreach (var manualUpdate in _manualUpdates)
            {
                manualUpdate.ManualUpdate(Time.deltaTime);
            }
        }
    }
}