using UnityEngine;

public class RigidActive : MonoBehaviour
{
    public GameObject targetObject;  

    void Start()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TT"))
        {
            if (targetObject != null)
            {
                targetObject.SetActive(true);  
            }
        }
    }
}
