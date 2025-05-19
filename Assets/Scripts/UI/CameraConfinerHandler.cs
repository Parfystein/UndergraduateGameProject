using UnityEngine;
using Cinemachine;

public class CameraConfinerHandler : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
    }

    public void SetConfinerBounds(Collider2D newRoomCollider)
    {
        if (confiner == null)
        {
            Debug.LogWarning("No CinemachineConfiner2D found!");
            return;
        }

        confiner.m_BoundingShape2D = newRoomCollider;
        confiner.InvalidateCache();
    }
}