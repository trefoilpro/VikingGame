using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _camera;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.forward);
    }

    public void Initialize(Camera camera)
    {
        _camera = camera;
    }
}
