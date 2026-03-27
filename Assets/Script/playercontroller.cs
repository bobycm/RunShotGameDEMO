using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public Camera mainCamera;
    public float minX = -3f;
    public float maxX = 3f;

    private float offsetX;
    private float fixedY;
    private float fixedZ;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.30f);

        transform.position = mainCamera.ViewportToWorldPoint(viewPos);
    }
    void OnMouseDown()
    {
        if (!enabled) return;
        fixedZ = transform.position.z;
        fixedY = transform.position.y;

        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        offsetX = Input.mousePosition.x - playerScreenPoint.x;
    }
    void OnMouseDrag()
    {
        if (!enabled) return;
        Vector3 mouseScreenPosition = new Vector3(
            Input.mousePosition.x - offsetX, // 相對位置
            Input.mousePosition.y,
            Camera.main.WorldToScreenPoint(transform.position).z // 角色的Z座標
        );

        Vector3 newWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        float clampedX = Mathf.Clamp(newWorldPosition.x, minX, maxX);

        transform.position = new Vector3(clampedX, fixedY, fixedZ);
    }
}