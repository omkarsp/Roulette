using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    private const float HIGHLIGHT_DURATION = 0.3f;
    private const int NO_OF_BLINKS = 4;
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
        foreach (SpriteRenderer sr in numberSprites.spriteRenderers)
        {
            Highlight.HighlightSprite(sr, normalColor);
        }

        for (int i = 0; i < NO_OF_BLINKS; i++)
        {
            Highlight.HighlightSprite(numberSprites.spriteRenderers[ballTarget.resultNum], highlightColor);

            yield return new WaitForSeconds(HIGHLIGHT_DURATION);

            Highlight.HighlightSprite(numberSprites.spriteRenderers[ballTarget.resultNum], normalColor);

            yield return new WaitForSeconds(HIGHLIGHT_DURATION);
        }

        spinButton.interactable = true;
    }
}
