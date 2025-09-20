using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{
    public GameObject[] hairStyles;
    public GameObject[] glassesTypes;
    public GameObject[] clothesColors;
    public void ChangeHairStyle(int index)
    {
        foreach (GameObject hair in hairStyles)
        {
            hair.SetActive(false);
        }
        hairStyles[index].SetActive(true);
    }
    public void ChangeGlasses(int index)
    {
        foreach (GameObject glasses in glassesTypes)
        {
            glasses.SetActive(false);
        }
        glassesTypes[index].SetActive(true);
    }
    public void ChangeClothesColor(int index)
    {
        foreach (GameObject clothes in clothesColors)
        {
            clothes.SetActive(false);
        }
        clothesColors[index].SetActive(true);
    }
}
