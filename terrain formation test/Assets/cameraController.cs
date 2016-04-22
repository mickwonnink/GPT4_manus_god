using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour
{
    private static cameraController instance;

    public static cameraController getInstance() { return instance; }

    private void Awake()
    {
        instance = this;

        offset = transform.position;
        zoom = new Vector3();
        setBounds(-5000, 5000, -5000, 5000, -5000, 5000);
    }

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float minZ;
    private float maxZ;

    private Vector3 offset;
    private Vector3 zoom;

    private void LateUpdate()
    {
        float dx = Input.GetAxis("Horizontal") / 2;
        if ((dx > 0 && zoom.x + dx <= maxX) || (dx < 0 && zoom.x + dx >= minX))
        {
            zoom.x += dx*2;
        }

        float dy = Input.GetAxis("Mouse ScrollWheel") * -1;
        if ((dy > 0 && zoom.y + dy <= maxY) || (dy < 0 && zoom.y + dy >= minY))
        {
            zoom.y += (dy * 10);
        }

        float dz = Input.GetAxis("Vertical") / 2;
        if ((dz > 0 && zoom.z + dz <= maxZ) || (dz < 0 && zoom.z + dz >= minZ))
        {
            zoom.z += dz;
        }

        transform.position = offset + zoom;
    }

    public void setPosition(Vector3 vec)
    {
        zoom = vec;
    }

    public void setBounds(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
        this.minZ = minZ;
        this.maxZ = maxZ;
    }
}
