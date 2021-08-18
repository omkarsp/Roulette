using UnityEngine;
using UnityEngine.UI;

public class CurrentChip : MonoBehaviour
{
    public double value;
    public string valueString;
    public Color coinColor;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        value = 1;
        valueString = "1";
        coinColor = GetComponent<Image>().color;
    }
}
