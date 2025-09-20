using UnityEngine;

public class DragModeToggle : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.R;  // المفتاح الذي يتم الضغط عليه للتبديل بين الوضعين
    private bool isDraggingEnabled = false;  // حالة الوضع الحالي
    private Rigidbody[] allRigidbodies;  // مصفوفة تحتوي على جميع الـ Rigidbody في المشهد

    void Start()
    {
        // الحصول على جميع الـ Rigidbody في المشهد
        allRigidbodies = FindObjectsOfType<Rigidbody>();
    }

    void Update()
    {
        // تحقق من الضغط على المفتاح المحدد
        if (Input.GetKeyDown(toggleKey))
        {
            isDraggingEnabled = !isDraggingEnabled;  // التبديل بين الوضعين
            ToggleDragMode(isDraggingEnabled);
        }
    }

    // تفعيل أو إيقاف تفعيل حركة العناصر بناءً على التاج أو السكربت
    private void ToggleDragMode(bool enable)
    {
        // إذا تم تفعيل الوضع
        if (enable)
        {
            // تفعيل السكربتات لجميع الكائنات مع تاق "Drag"
            GameObject[] draggableObjects = GameObject.FindGameObjectsWithTag("Drag");

            foreach (GameObject obj in draggableObjects)
            {
                if (obj.GetComponent<Draggable>() != null)
                {
                    obj.GetComponent<Draggable>().enabled = true;  // تفعيل السكربت
                }
            }

            // تعطيل حركة باقي الأجسام (بدون تاق "Drag" أو السكربت "Draggable")
            foreach (Rigidbody rb in allRigidbodies)
            {
                if (rb.GetComponent<Draggable>() == null)  // إذا لم يحتوي على السكربت
                {
                    rb.isKinematic = true;  // تعطيل الحركة
                }
            }
        }
        else
        {
            // إيقاف الحركة لجميع الكائنات وتفعيل الحركة فقط للكائنات مع السكربت
            foreach (Rigidbody rb in allRigidbodies)
            {
                rb.isKinematic = false;  // إعادة تفعيل الحركة لجميع الأجسام
            }

            // إيقاف السكربت للكائنات التي تحتوي على تاق "Drag"
            GameObject[] draggableObjects = GameObject.FindGameObjectsWithTag("Drag");

            foreach (GameObject obj in draggableObjects)
            {
                if (obj.GetComponent<Draggable>() != null)
                {
                    obj.GetComponent<Draggable>().enabled = false;  // إيقاف السكربت
                }
            }

            // تجميد الكائنات بعد العودة إلى الوضع العادي
            foreach (GameObject obj in draggableObjects)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // تجميد الحركة بعد العودة
                    rb.isKinematic = true;
                    rb.constraints = RigidbodyConstraints.FreezeAll;  // تجميد جميع المحاور
                }
            }
        }
    }
}
