using UnityEngine;
using UnityEngine.AI;

public class DuckFollow : MonoBehaviour
{
    public GameObject[] targets; // ÇáÃåÏÇİ ÇáÊí ÓíáÇÍŞåÇ ÇáÈØ
    private NavMeshAgent navAgent;

    void Start()
    {
        // ÇáÍÕæá Úáì ãßæä NavMeshAgent
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // ÇáÊÍŞŞ ÅĞÇ ßÇä áÏíäÇ ÃåÏÇİ áíáÇÍŞåÇ
        if (targets.Length > 0)
        {
            // äÎÊÇÑ Ãæá åÏİ (Ãæ íãßäß ÊÍÏíÏ ÇáÎæÇÑÒãíÉ ÇáÊí ÊÎÊÇÑ ÇáåÏİ)
            GameObject target = targets[0];

            // ÌÚá ÇáÈØ íÊÈÚ ÇáåÏİ Úáì ÇáÜNavMesh
            navAgent.SetDestination(target.transform.position);
        }
    }
}
