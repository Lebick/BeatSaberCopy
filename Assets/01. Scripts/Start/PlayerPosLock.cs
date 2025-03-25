using UnityEngine;

public class PlayerPosLock : MonoBehaviour
{
    public Transform xrOrigin;
    private Vector3 xrPos;

    private void Start()
    {
        xrPos = xrOrigin.position;
    }

    private void Update()
    {
        xrOrigin.position = xrPos;
    }
}
