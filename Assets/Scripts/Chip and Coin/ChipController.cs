using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChipController : MonoBehaviour
{
    Button currentChipButton;
    [SerializeField] private GameObject chipSelectionPanel;
    [SerializeField] private Transform movableChipPrefab;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Transform coinsParent;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        currentChipButton = GetComponent<Button>();
        currentChipButton.onClick.AddListener(ShowChipView);
        startPos = transform.position;
    }

    private void ShowChipView()
    {
        chipSelectionPanel.SetActive(true);
    }

    private void SetupCoinToBet()
    {
        Instantiate(movableChipPrefab, startPos, Quaternion.identity, coinsParent);
    }

    private void MoveChip(Vector3 betPos)
    {
        StartCoroutine(MoveChipRoutine(betPos));
    }

    IEnumerator MoveChipRoutine(Vector3 betPos)
    {
        SetupCoinToBet();

        while (Vector3.Distance(betPos, startPos) > 0)
        {
            movableChipPrefab.Translate(betPos * Time.deltaTime * moveSpeed);
        }

        yield return null;
    }
}
