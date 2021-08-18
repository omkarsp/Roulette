using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chip : MonoBehaviour
{
    [SerializeField] private double value = 1;
    [SerializeField] private GameObject chipSelectionPanel;
    [SerializeField] private TextMeshProUGUI currentChipText;
    [SerializeField] private Image currentChipImage;
    [SerializeField] private CurrentChip currentChip;
    private string valueString;
    private Color thisChipColor;
    private Button thisChipButton;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        thisChipColor = GetComponent<Image>().color;
        valueString = GetComponentInChildren<TextMeshProUGUI>().text;
        thisChipButton = GetComponent<Button>();
        thisChipButton.onClick.AddListener(HideChipSelectionView);
        thisChipButton.onClick.AddListener(UpdateCurrentChip);
    }

    public void HideChipSelectionView()
    {
        //chipSelectionPanel.SetActive(false);
    }

    private void UpdateCurrentChip()
    {
        currentChipImage.color = thisChipColor;
        currentChipText.text = valueString;
        currentChip.value = value;
        currentChip.coinColor = thisChipColor;
        currentChip.valueString = valueString;
        Debug.Log(valueString);
    }
}
