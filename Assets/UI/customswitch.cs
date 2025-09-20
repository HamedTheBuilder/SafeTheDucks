using UnityEngine;
using UnityEngine.UI;
public class customswitch : MonoBehaviour
{
    public Button hairButton;        
    public Button faceButton;       
    public GameObject[] hairObjects; 
    public GameObject[] faceObjects;

    private int currentHairIndex = 0; 
    private int currentFaceIndex = 0;

    void Start()
    {
        if (hairObjects.Length == 0 || faceObjects.Length == 0)
        {
            Debug.LogError("Error: One of the arrays is empty.");
            return;
        }
        foreach (var hairObject in hairObjects)
        {
            hairObject.SetActive(false);
        }

        foreach (var faceObject in faceObjects)
        {
            faceObject.SetActive(false);
        }
        if (hairObjects.Length > 0)
        {
            hairObjects[currentHairIndex].SetActive(true);
        }
        if (faceObjects.Length > 0)
        {
            faceObjects[currentFaceIndex].SetActive(true);
        }
        hairButton.onClick.AddListener(ToggleHair);
        faceButton.onClick.AddListener(ToggleFace);
    }
    void ToggleHair()
    {
        hairObjects[currentHairIndex].SetActive(false);
        currentHairIndex = (currentHairIndex + 1) % hairObjects.Length;
        hairObjects[currentHairIndex].SetActive(true);
    }
    void ToggleFace()
    {
        faceObjects[currentFaceIndex].SetActive(false);
        currentFaceIndex = (currentFaceIndex + 1) % faceObjects.Length;
        faceObjects[currentFaceIndex].SetActive(true);
    }
}
