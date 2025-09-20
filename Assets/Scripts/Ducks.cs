using UnityEngine;
using UnityEngine.AI;

public class DuckFollow : MonoBehaviour
{
    public GameObject[] targets; // «·√Âœ«› «· Ì ”Ì·«ÕﬁÂ« «·»ÿ
    private NavMeshAgent navAgent;
    private Animator animator;   // „ﬂÊ‰ «·√‰Ì„Ì‘‰
    public float rotationSpeed = 5f; // ”—⁄… «· œÊÌ— »« Ã«Â «·ﬂ«„Ì—«

    void Start()
    {
        // «·Õ’Ê· ⁄·Ï „ﬂÊ‰ NavMeshAgent ÊAnimator
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // «· Õﬁﬁ ≈–« ﬂ«‰ ·œÌ‰« √Âœ«› ·Ì·«ÕﬁÂ«
        if (targets.Length > 0)
        {
            // ‰Œ «— √Ê· Âœ› (√Ê Ì„ﬂ‰ﬂ  ÕœÌœ «·ŒÊ«—“„Ì… «· Ì  Œ «— «·Âœ›)
            GameObject target = targets[0];

            // Ã⁄· «·»ÿ Ì »⁄ «·Âœ› ⁄·Ï «·‹NavMesh
            navAgent.SetDestination(target.transform.position);

            //  ›⁄Ì· √‰Ì„Ì‘‰ «·„‘Ì ⁄‰œ„« Ì Õ—ﬂ «·»ÿ
            if (navAgent.velocity.magnitude > 0.1f)  // ≈–« ﬂ«‰ Ì Õ—ﬂ
            {
                animator.SetBool("isWalking", true);
            }
            else  // ≈–« ﬂ«‰ ·« Ì Õ—ﬂ
            {
                animator.SetBool("isWalking", false);
            }

            // Ã⁄· «·»ÿ ÌÊ«ÃÂ «·ﬂ«„Ì—« (√Ê «·« Ã«Â «·–Ì   Õ—ﬂ ›ÌÂ «·ﬂ«„Ì—«)
            Vector3 directionToCamera = Camera.main.transform.position - transform.position;
            directionToCamera.y = 0;  //  Ã«Â· «· €ÌÌ— ›Ì «·„ÕÊ— Y
            Quaternion targetRotation = Quaternion.LookRotation(-directionToCamera);  //  œÊÌ— «·»ÿ »« Ã«Â «·⁄ﬂ”
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);  // «· œÊÌ— »”·«”…
        }
    }
}
