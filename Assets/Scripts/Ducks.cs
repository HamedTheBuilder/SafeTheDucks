using UnityEngine;
using UnityEngine.AI;

public class DuckFollow : MonoBehaviour
{
    public GameObject[] targets; // ������� ���� �������� ����
    private NavMeshAgent navAgent;

    void Start()
    {
        // ������ ��� ���� NavMeshAgent
        navAgent = GetComponent<NavMeshAgent>();
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
        }
    }
}
