using System.Collections;
using UnityEngine;
public class CanvasAnimation : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] GameObject[] objectsOnPanel;
    [SerializeField] TMPro.TextMeshProUGUI textPoint;
    [SerializeField] float tiempoTotal; //tiempo que se demora en hacer la animacion
    [SerializeField] float enableObjectsTime = 0.05f;
    [SerializeField] AnimationCurve animationCurve;
    bool isAnim = false;
    public IEnumerator ShowPointsCoroutine(int points)
    {
        yield return AnimPanelCoroutine(true);
        float suma = points / 10;
        int currentPoints = 0;
        for (int i = 0; i < 10; i++)
        {
            currentPoints += (int)suma;

            textPoint.text = currentPoints.ToString();

            textPoint.transform.localScale = Vector3.one * 1.3f;
            yield return new WaitForSeconds(enableObjectsTime);
            textPoint.transform.localScale = Vector3.one;
            //Agregar sonido
            yield return new WaitForSeconds(enableObjectsTime);
        }

        textPoint.text = points.ToString();
    }
    public IEnumerator AnimPanelCoroutine(bool isEnable)
    {
        isAnim = true;
        bool isOnAnim = true;
        Vector2 starPos = rectTransform.anchoredPosition;
        Vector2 endPos = Vector2.zero;

        if (!isEnable)
        {
            endPos = new Vector2(rectTransform.rect.width, rectTransform.anchoredPosition.y);
            foreach (GameObject item in objectsOnPanel)
            {
                item.SetActive(false);
                yield return new WaitForSeconds(enableObjectsTime);
            }
        }

        float lerp = 0;
        float startTime = Time.time;
        while (isOnAnim)
        {
            lerp = (Time.time - startTime) / tiempoTotal;
            rectTransform.anchoredPosition = Vector2.Lerp(starPos, endPos, lerp);
            if (lerp >= 1) isOnAnim = false;
            yield return null;
        }
        rectTransform.anchoredPosition = endPos;
        isAnim = false;

        if (isEnable)
        {
            foreach (GameObject item in objectsOnPanel)
            {
                item.SetActive(true);
                yield return new WaitForSeconds(enableObjectsTime);
            }
        }
    }
}
public enum AnimSide
{
    Left,
    Right,
    Top,
    Bottom,
}