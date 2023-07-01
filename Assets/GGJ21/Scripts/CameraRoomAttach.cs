using System.Linq;
using Cinemachine;
using UnityEngine;

namespace GGJ21.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class CameraRoomAttach : MonoBehaviour
    {

        public GameObject player;
        public GameObject room;
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
                targetGroup.m_Targets[1].target = room.transform;
                targetGroup.m_Targets[1].weight = 3;
            }
        }

    }
}