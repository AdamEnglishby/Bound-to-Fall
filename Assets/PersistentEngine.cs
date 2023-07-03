using UnityEngine;

public class PersistentEngine : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
