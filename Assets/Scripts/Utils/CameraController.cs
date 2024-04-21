using UnityEngine;

public class CameraController
{
    public Vector3 camLeftEdge, camRightEdge, camTopEdge, camBottomEdge;

    public CameraController()
    {
        camLeftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        camRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.nearClipPlane));
        camTopEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane));
        camBottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
    }
}
