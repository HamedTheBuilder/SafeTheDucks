using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    private Rigidbody rb;

    void Start()
    {
        // الحصول على الـ Rigidbody الخاص بالكائن
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        // حفظ المسافة بين الكاميرا والكائن عند الضغط
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        // الحصول على موقع الكائن الجديد بناءً على حركة الماوس
        Vector3 newPosition = GetMouseWorldPosition() + offset;

        // منع الحركة في المحاور المجمدة في Rigidbody
        if (rb.constraints.HasFlag(RigidbodyConstraints.FreezePositionX))
        {
            newPosition.x = transform.position.x;  // تثبيت قيمة المحور X
        }
        if (rb.constraints.HasFlag(RigidbodyConstraints.FreezePositionY))
        {
            newPosition.y = transform.position.y;  // تثبيت قيمة المحور Y
        }
        if (rb.constraints.HasFlag(RigidbodyConstraints.FreezePositionZ))
        {
            newPosition.z = transform.position.z;  // تثبيت قيمة المحور Z
        }

        // تحديث الكائن للموقع الجديد
        transform.position = newPosition;
    }

    // تحويل إحداثيات الماوس من شاشة إلى عالمية
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
