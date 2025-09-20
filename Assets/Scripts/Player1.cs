using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // سرعة الحركة
    public float jumpForce = 10f; // قوة القفز

    private Rigidbody rb;
    private bool isIn2DMode = true; // تحديد الوضع الحالي (2D أو 3D)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // التبديل بين الوضعين عند الضغط على R
        if (Input.GetKeyDown(KeyCode.R))
        {
            isIn2DMode = !isIn2DMode;
        }

        // الحركة في الوضع 2D
        if (isIn2DMode)
        {
            float moveX = Input.GetAxis("Horizontal");  // الحركة على المحور X (يمين/يسار)

            // الحركة على المحور X فقط في وضع 2D (لا يتحرك على Y أو Z)
            rb.linearVelocity = new Vector3(moveX * moveSpeed, rb.linearVelocity.y, 0);

            // القفز في وضع 2D
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        else
        {
            // الحركة في وضع 3D
            float moveX = Input.GetAxis("Horizontal");  // الحركة على المحور X (يمين/يسار)
            float moveZ = Input.GetAxis("Vertical");    // الحركة على المحور Z (أمام/خلف)

            // الحركة على المحورين X و Z في وضع 3D
            rb.linearVelocity = new Vector3(moveX * moveSpeed, rb.linearVelocity.y, moveZ * moveSpeed);

            // القفز في وضع 3D
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
