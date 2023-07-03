using UnityEngine;

public class VSyncHandler : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
