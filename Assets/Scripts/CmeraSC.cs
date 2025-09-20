using UnityEngine;
using UnityEngine.UI; // áÇÓÊÎÏÇã UI elements ãËá Slider

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // ÇááÇÚÈ
    public float followSpeed = 2f;
    public Vector3 cameraOffset2D = new Vector3(0, 5, -10); // ÇáßÇãíÑÇ İí æÖÚ 2D
    public Vector3 cameraOffset3D = new Vector3(0, 5, -10); // ÇáßÇãíÑÇ İí æÖÚ 3D

    public Slider shakeSlider;  // ÇáÓáÇíÏÑ ÇáĞí íãËá ãŞÏÇÑ ÊÃËíÑ ÇáÍÑßÉ ÇáÚÔæÇÆíÉ

    private bool isIn2DMode = true;
    private float originalCameraY;  // ÊÎÒíä ÇÑÊİÇÚ ÇáßÇãíÑÇ ÇáÃÕáí
    private float shakeTimer = 0f;  // ãÄŞÊ ÇáãæÌÉ
    private float randomMovementSpeed = 1f;  // ÓÑÚÉ ÇáÍÑßÉ ÇáÚÔæÇÆíÉ
    private Vector3 randomOffset = Vector3.zero;  // ÇáÍÑßÉ ÇáÚÔæÇÆíÉ
    private float randomAmount = 0f;  // ãŞÏÇÑ ÇáÊÃËíÑ ÇáÚÔæÇÆí
    private float randomIncrement = 0.01f; // ãŞÏÇÑ ÇáÒíÇÏÉ İí ÇáÊÃËíÑ ÇáÚÔæÇÆí
    private float randomDecreaseSpeed = 0.5f; // ÓÑÚÉ äŞÕÇä ÇáÊÃËíÑ İí 2D
    private float randomIncreaseSpeed = 0.3f; // ÓÑÚÉ ÒíÇÏÉ ÇáÊÃËíÑ İí 3D

    void Start()
    {
        // ÊÎÒíä ÇÑÊİÇÚ ÇáßÇãíÑÇ ÇáÃÕáí
        originalCameraY = transform.position.y;

        // ÊÚííä ÇáÓáÇíÏÑ Åáì ÇáŞíãÉ ÇáÃæáíÉ
        if (shakeSlider != null)
        {
            shakeSlider.value = 0f;  // ÇáÓáÇíÏÑ íÈÏÃ ãä ÇáÕİÑ
        }
    }

    void Update()
    {
        // ÇáÊÈÏíá Èíä ÇáßÇãíÑÇ ÇáËÇÈÊÉ æÇáÍÑÉ
        if (Input.GetKeyDown(KeyCode.R))
        {
            isIn2DMode = !isIn2DMode;
        }

        // ÒíÇÏÉ ãÄŞÊ ÇáÍÑßÉ ÇáÚÔæÇÆíÉ
        shakeTimer += Time.deltaTime * randomMovementSpeed;

        // ÒíÇÏÉ Ãæ ÊŞáíá ÇáãæÌÉ ÍÓÈ ÇáæÖÚ
        if (isIn2DMode)
        {
            // ÊŞáíá ÇáÊÃËíÑ ÇáÚÔæÇÆí ÊÏÑíÌíğÇ İí æÖÚ 2D
            randomAmount = Mathf.Lerp(randomAmount, 0, Time.deltaTime * randomDecreaseSpeed);
        }
        else
        {
            // ÒíÇÏÉ ÇáÊÃËíÑ ÇáÚÔæÇÆí ÊÏÑíÌíğÇ İí æÖÚ 3D
            randomAmount = Mathf.Lerp(randomAmount, 1, Time.deltaTime * randomIncreaseSpeed);
        }

        // ÊæáíÏ ÍÑßÉ ÚÔæÇÆíÉ ÎİíİÉ İí ßá ãÑÉ
        randomOffset = new Vector3(
            Mathf.Sin(shakeTimer * randomMovementSpeed) * randomAmount,  // ÍÑßÉ ÚÔæÇÆíÉ ÎİíİÉ İí ÇáÇÊÌÇå X
            Mathf.Cos(shakeTimer * randomMovementSpeed) * randomAmount,  // ÍÑßÉ ÚÔæÇÆíÉ ÎİíİÉ İí ÇáÇÊÌÇå Y
            0);  // áÇ äÑíÏ Ãä ÊÊÍÑß ÇáßÇãíÑÇ ßËíÑğÇ İí ÇáÇÊÌÇå Z

        // ÊÍÏíË ÇáÓáÇíÏÑ ÈäÇÁğ Úáì ãŞÏÇÑ ÇáÊÃËíÑ ÇáÚÔæÇÆí
        if (shakeSlider != null)
        {
            shakeSlider.value = randomAmount;  // ÊÍÏíË ÇáÓáÇíÏÑ áíÚßÓ ãŞÏÇÑ ÇáÊÃËíÑ ÇáÚÔæÇÆí
        }

        // ÊÍÑíß ÇáßÇãíÑÇ ÈäÇÁğ Úáì ÇáæÖÚ
        if (isIn2DMode)
        {
            // İí æÖÚ 2D¡ ÇáßÇãíÑÇ ÊÊÍÑß İŞØ ãÚ ÇááÇÚÈ Úáì ÇáãÍæÑ X
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y + cameraOffset2D.y, cameraOffset2D.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition + randomOffset, followSpeed * Time.deltaTime);

            // ÇáßÇãíÑÇ ÊÙá ãæÌåÉ ááÃãÇã
            transform.rotation = Quaternion.Euler(0, 0, 0);  // ÊËÈíÊ ÒÇæíÉ ÇáßÇãíÑÇ ÈÍíË áÇ ÊÏæÑ
        }
        else
        {
            // İí æÖÚ 3D¡ ÇáßÇãíÑÇ ÊÊÍÑß ãÚ ÇááÇÚÈ İí ÇáãÍÇæÑ X æ Y æ Z
            transform.position = Vector3.Lerp(transform.position, player.position + cameraOffset3D + randomOffset, followSpeed * Time.deltaTime);

            // ÇáßÇãíÑÇ ÊÊÇÈÚ ÇááÇÚÈ
            transform.LookAt(player);  // ÇáßÇãíÑÇ ÊÊÇÈÚ ÇááÇÚÈ
        }
    }
}
