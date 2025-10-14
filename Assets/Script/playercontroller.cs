using UnityEngine;

public class PlayerFollowMouseXOnly : MonoBehaviour
{
    public float minX = -3f;
    public float maxX = 3f;

    private float offsetX;

    private float fixedY;
    private float fixedZ;

    void OnMouseDown()
    {
        fixedZ = transform.position.z;
        fixedY = transform.position.y;

        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        offsetX = Input.mousePosition.x - playerScreenPoint.x;
    }
    void OnMouseDrag()
    {
        Vector3 mouseScreenPosition = new Vector3(
            Input.mousePosition.x - offsetX, // 相對位置
            Input.mousePosition.y,
            Camera.main.WorldToScreenPoint(transform.position).z // 角色的Z座標
        );

        //將螢幕座標轉換為世界座標
        Vector3 newWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // 設定X座標限制範圍
        float clampedX = Mathf.Clamp(newWorldPosition.x, minX, maxX);

        // Y 和 Z 座標不變
        transform.position = new Vector3(clampedX, fixedY, fixedZ);
    }
}