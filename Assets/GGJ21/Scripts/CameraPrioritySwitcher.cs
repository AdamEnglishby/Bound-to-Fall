using Cinemachine;
using UnityEngine;

namespace GGJ21.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class CameraPrioritySwitcher : MonoBehaviour
    {

        public GameObject player;
        public CinemachineTargetGroup targetGroup;
        
        private BoxCollider _box;
        
        private void Awake()
        {
            _box = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            if (_box.bounds.Contains(player.transform.position))
            {
                // set the target group to only the player
                targetGroup.m_Targets[1].weight = 0;
            }
        }

    }
}