using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RoomSpawner : MonoBehaviour
{

    public GameObject player;
    public GameObject roomToSpawn;
    public Vector3 spawnLocation;
    private BoxCollider _box;

    private void Awake()
    {
        _box = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!_box.bounds.Contains(player.transform.position)) return;
        
        Instantiate(roomToSpawn, player.transform.position + spawnLocation, Quaternion.identity);
        Destroy(this);
    }

}