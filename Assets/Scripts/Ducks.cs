using UnityEngine;
using UnityEngine.AI;

public class DuckFollow : MonoBehaviour
{
    public GameObject[] targets; // ������� ���� �������� ����
    private NavMeshAgent navAgent;
    private Animator animator;   // ���� ���������
    public float rotationSpeed = 5f; // ���� ������� ������ ��������

    void Start()
    {
        // ������ ��� ���� NavMeshAgent �Animator
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ������ ��� ��� ����� ����� ��������
        if (targets.Length > 0)
        {
            // ����� ��� ��� (�� ����� ����� ���������� ���� ����� �����)
            GameObject target = targets[0];

            // ��� ���� ���� ����� ��� ���NavMesh
            navAgent.SetDestination(target.transform.position);

            // ����� ������� ����� ����� ����� ����
            if (navAgent.velocity.magnitude > 0.1f)  // ��� ��� �����
            {
                animator.SetBool("isWalking", true);
            }
            else  // ��� ��� �� �����
            {
                animator.SetBool("isWalking", false);
            }

            // ��� ���� ����� �������� (�� ������� ���� ����� ��� ��������)
            Vector3 directionToCamera = Camera.main.transform.position - transform.position;
            directionToCamera.y = 0;  // ����� ������� �� ������ Y
            Quaternion targetRotation = Quaternion.LookRotation(-directionToCamera);  // ����� ���� ������ �����
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);  // ������� ������
        }
    }
}
