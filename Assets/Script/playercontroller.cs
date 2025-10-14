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
            Input.mousePosition.x - offsetX, // �۹��m
            Input.mousePosition.y,
            Camera.main.WorldToScreenPoint(transform.position).z // ���⪺Z�y��
        );

        //�N�ù��y���ഫ���@�ɮy��
        Vector3 newWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // �]�wX�y�Э���d��
        float clampedX = Mathf.Clamp(newWorldPosition.x, minX, maxX);

        // Y �M Z �y�Ф���
        transform.position = new Vector3(clampedX, fixedY, fixedZ);
    }
}