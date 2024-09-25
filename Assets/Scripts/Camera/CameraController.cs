using UnityEngine;
using Cinemachine;

public class CameraZoomController : MonoBehaviour
{
    public Transform player;
    public CinemachineVirtualCamera vCam;
    public float zoomInSize = 4f;
    public float zoomOutSize = 6f;
    public float zoomSpeed = 2f;

    private Rigidbody2D playerRb;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float playerSpeed = playerRb.velocity.magnitude;

        if (playerSpeed > 0.1f)
        {
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(vCam.m_Lens.OrthographicSize, zoomInSize, Time.deltaTime * zoomSpeed);
        }
        else
        {
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(vCam.m_Lens.OrthographicSize, zoomOutSize, Time.deltaTime * zoomSpeed);
        }
    }
}
