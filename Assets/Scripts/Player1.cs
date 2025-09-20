using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // سرعة الحركة
    public float jumpForce = 10f; // قوة القفز
    public AudioClip runSound;    // صوت الجري
    public AudioClip jumpSound;   // صوت القفز

    private Rigidbody rb;
    private Animator animator;    // لتحريك الأنيميشن
    private AudioSource audioSource;  // لتشغيل الأصوات
    private bool isIn2DMode = true; // تحديد الوضع الحالي (2D أو 3D)
    private bool isGrounded; // هل اللاعب على الأرض؟

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // تعيين الروتيشن ليواجه اللاعب اليمين عند بدء اللعبة
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    void Update()
    {
        // التبديل بين الوضعين عند الضغط على R
        if (Input.GetKeyDown(KeyCode.R))
        {
            isIn2DMode = !isIn2DMode;
        }

        // التحقق إذا كان اللاعب على الأرض باستخدام Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);  // 1f هي المسافة للتحقق من الأرض

        // الحركة في الوضع 2D
        if (isIn2DMode)
        {
            float moveX = Input.GetAxis("Horizontal");  // الحركة على المحور X (يمين/يسار)

            // الحركة على المحور X فقط في وضع 2D (لا يتحرك على Y أو Z)
            rb.linearVelocity = new Vector3(moveX * moveSpeed, rb.linearVelocity.y, 0);

            // تدوير اللاعب نحو الاتجاه الذي يمشي فيه
            if (moveX > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);  // اللاعب يواجه الأمام
            }
            else if (moveX < 0)
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);  // اللاعب يواجه الوراء
            }

            // القفز في وضع 2D
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                PlayJumpSound(); // تشغيل صوت القفز
                animator.SetTrigger("Jump"); // تفعيل أنيميشن القفز
            }

            // إذا كان اللاعب يتحرك، شغّل أنيميشن الجري
            if (Mathf.Abs(moveX) > 0)
            {
                animator.SetBool("isRunning", true);
                PlayRunSound(); // تشغيل صوت الجري
            }
            else
            {
                animator.SetBool("isRunning", false);
                StopRunSound(); // إيقاف صوت الجري
            }
        }
        else
        {
            // الحركة في وضع 3D
            float moveX = Input.GetAxis("Horizontal");  // الحركة على المحور X (يمين/يسار)
            float moveZ = Input.GetAxis("Vertical");    // الحركة على المحور Z (أمام/خلف)

            // الحركة على المحورين X و Z في وضع 3D
            rb.linearVelocity = new Vector3(moveX * moveSpeed, rb.linearVelocity.y, moveZ * moveSpeed);

            // تدوير اللاعب نحو الاتجاه الذي يمشي فيه
            if (moveX != 0 || moveZ != 0)
            {
                Vector3 direction = new Vector3(moveX, 0, moveZ);
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up); // تدوير اللاعب نحو الاتجاه
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 500 * Time.deltaTime);
            }

            // القفز في وضع 3D
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                PlayJumpSound(); // تشغيل صوت القفز
                animator.SetTrigger("Jump"); // تفعيل أنيميشن القفز
            }

            // إذا كان اللاعب يتحرك، شغّل أنيميشن الجري
            if (Mathf.Abs(moveX) > 0 || Mathf.Abs(moveZ) > 0)
            {
                animator.SetBool("isRunning", true);
                PlayRunSound(); // تشغيل صوت الجري
            }
            else
            {
                animator.SetBool("isRunning", false);
                StopRunSound(); // إيقاف صوت الجري
            }
        }
    }

    // تشغيل صوت الجري
    private void PlayRunSound()
    {
        if (!audioSource.isPlaying && runSound != null)
        {
            audioSource.clip = runSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // إيقاف صوت الجري
    private void StopRunSound()
    {
        if (audioSource.isPlaying && runSound != null)
        {
            audioSource.Stop();
        }
    }

    // تشغيل صوت القفز
    private void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound); // تشغيل صوت القفز لمرة واحدة
        }
    }
}
