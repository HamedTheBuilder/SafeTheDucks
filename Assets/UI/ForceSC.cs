using UnityEngine;

public class TrapEffect : MonoBehaviour
{
    public float minForce = 20f; 
    public float maxForce = 40f; 
    public AudioClip trapSound;  
    private AudioSource audioSource;  

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                float randomXForce = Random.Range(-maxForce, maxForce); 
                float randomYForce = Random.Range(minForce, maxForce);  

                // تطبيق القوة العشوائية على اللاعب
                playerRb.AddForce(new Vector2(randomXForce, randomYForce), ForceMode2D.Impulse);  
            }

            // تشغيل الصوت عندما يحدث التصادم
            if (audioSource != null && trapSound != null)
            {
                audioSource.PlayOneShot(trapSound);  // تشغيل الصوت عند التصادم
            }
        }
    }
}