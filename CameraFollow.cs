using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Masukkan objek Player ke sini via Inspector
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10f); // Offset Z biasanya -10 untuk 2D

    void LateUpdate()
    {
        if (player == null) return;

        // Hanya mengikuti pergerakan X (kiri/kanan), dan sedikit Y jika mau
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        transform.position = smoothedPosition;
    }
}
