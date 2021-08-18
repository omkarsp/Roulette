using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] private BallTarget ballTarget;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor;
    [SerializeField] private NumberSprites numberSprites;
    [SerializeField] private Button spinButton;

    public void ShowResult()
    {
        StartCoroutine(ResultRoutine());
    }

    IEnumerator ResultRoutine()
    {
        foreach(SpriteRenderer sr in numberSprites.spriteRenderers)
        {
            Highlight.HighlightSprite(sr, normalColor);
        }

        for (int i = 0; i < 4; i++)
        {
            Highlight.HighlightSprite(numberSprites.spriteRenderers[ballTarget.resultNum], highlightColor);

            yield return new WaitForSeconds(0.3f);

            Highlight.HighlightSprite(numberSprites.spriteRenderers[ballTarget.resultNum], normalColor);

            yield return new WaitForSeconds(0.3f);
        }

        spinButton.interactable = true;
    }
}
