using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class BetSelection : MonoBehaviour
{
    [Header("Highlight selected bet")]
    [SerializeField] private Clickable clickable;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color lowBalanceColor;
    [SerializeField] private NumberSprites numSprites;
    [Space(20)]
    //Using this list to extract gameobject names
    [SerializeField] List<Transform> betTransforms;
    [SerializeField] List<SpriteList> spriteLists;
    [Header("Putting Coin on table")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinParent;
    [SerializeField] private CurrentChip currentChip;
    [Header("Update player balance and bet amount")]
    [SerializeField] Player player;
    [SerializeField] Bet bet;
    [SerializeField] Undo undo;
    //[Header("When timer finishes")]
    public delegate Task Del();
    //public Del nmbTriggered;
    public Action nmbTriggered;
    public List<BettingRequest> bettingRequests;
    [SerializeField] Roulettemanger rouletteManager;
    [SerializeField] SignalRConnection connection;

    private Dictionary<string, int> nameToIndexMap = new Dictionary<string, int>()
    {
        { "Number (0)", 0},
        { "Number (1)", 1},
        { "Number (2)", 2},
        { "Number (3)", 3},
        { "Number (4)", 4},
        { "Number (5)", 5},
        { "Number (6)", 6},
        { "Number (7)", 7},
        { "Number (8)", 8},
        { "Number (9)", 9},
        { "Number (10)", 10},
        { "Number (11)", 11},
        { "Number (12)", 12},
        { "Number (13)", 13},
        { "Number (14)", 14},
        { "Number (15)", 15},
        { "Number (16)", 16},
        { "Number (17)", 17},
        { "Number (18)", 18},
        { "Number (19)", 19},
        { "Number (20)", 20},
        { "Number (21)", 21},
        { "Number (22)", 22},
        { "Number (23)", 23},
        { "Number (24)", 24},
        { "Number (25)", 25},
        { "Number (26)", 26},
        { "Number (27)", 27},
        { "Number (28)", 28},
        { "Number (29)", 29},
        { "Number (30)", 30},
        { "Number (31)", 31},
        { "Number (32)", 32},
        { "Number (33)", 33},
        { "Number (34)", 34},
        { "Number (35)", 35},
        { "Number (36)", 36},
        { "Combination (01)", 37},
        { "Combination (012)", 38},
        { "Combination (02)", 39},
        { "Combination (023)", 40},
        { "Combination (03)", 41},
        { "Combination (12)", 42},
        { "Combination (23)", 43},
        { "Combination (14)", 44},
        { "Combination (1245)", 45},
        { "Combination (25)", 46},
        { "Combination (2356)", 47},
        { "Combination (36)", 48},
        { "Combination (45)", 49},
        { "Combination (56)", 50},
        { "Combination (47)", 51},
        { "Combination (4578)", 52},
        { "Combination (58)", 53},
        { "Combination (5689)", 54},
        { "Combination (69)", 55},
        { "Combination (78)", 56},
        { "Combination (89)", 57},
        { "Combination (7 10)", 58},
        { "Combination (7 8 10 11)", 59},
        { "Combination (8 11)", 60},
        { "Combination (8 9 11 12)", 61},
        { "Combination (9 12)", 62},
        { "Combination (10 11)", 63},
        { "Combination (11 12)", 64},
        { "Combination (10 13)", 65},
        { "Combination (10 11 13 14)", 66},
        { "Combination (11 14)", 67},
        { "Combination (11 12 14 15)", 68},
        { "Combination (12 15)", 69},
        { "Combination (13 14)", 70},
        { "Combination (14 15)", 71},
        { "Combination (13 16)", 72},
        { "Combination (13 14 16 17)", 73},
        { "Combination (14 17)", 74},
        { "Combination (14 15 17 18)", 75},
        { "Combination (15 18)", 76},
        { "Combination (16 17)", 77},
        { "Combination (17 18)", 78},
        { "Combination (16 19)", 79},
        { "Combination (16 17 19 20)", 80},
        { "Combination (17 20)", 81},
        { "Combination (17 18 20 21)", 82},
        { "Combination (18 21)", 83},
        { "Combination (19 20)", 84},
        { "Combination (20 21)", 85},
        { "Combination (19 22)", 86},
        { "Combination (19 20 22 23)", 87},
        { "Combination (20 23)", 88},
        { "Combination (20 21 23 24)", 89},
        { "Combination (21 24)", 90},
        { "Combination (22 23)", 91},
        { "Combination (23 24)", 92},
        { "Combination (22 25)", 93},
        { "Combination (22 23 25 26)", 94},
        { "Combination (23 26)", 95},
        { "Combination (23 24 26 27)", 96},
        { "Combination (24 27)", 97},
        { "Combination (25 26)", 98},
        { "Combination (26 27)", 99},
        { "Combination (25 28)", 100},
        { "Combination (25 26 28 29)", 101},
        { "Combination (26 29)", 102},
        { "Combination (26 27 29 30)", 103},
        { "Combination (27 30)", 104},
        { "Combination (28 29)", 105},
        { "Combination (29 30)", 106},
        { "Combination (28 31)", 107},
        { "Combination (28 29 31 32)", 108},
        { "Combination (29 32)", 109},
        { "Combination (29 30 32 33)", 110},
        { "Combination (30 33)", 111},
        { "Combination (31 32)", 112},
        { "Combination (32 33)", 113},
        { "Combination (31 34)", 114},
        { "Combination (31 32 34 35)", 115},
        { "Combination (32 35)", 116},
        { "Combination (32 33 35 36)", 117},
        { "Combination (33 36)", 118},
        { "Combination (34 35)", 119},
        { "Combination (35 36)", 120},
        { "1st 12", 121},
        { "2nd 12", 122},
        { "3rd 12", 123},
        { "1st Row (2 to 1)", 124},
        { "2nd Row (2 to 1)", 125},
        { "3rd Row (2 to 1)", 126},
        { "Small (1 - 18)", 127},
        { "Big (19 - 36)", 128},
        { "Even", 129},
        { "Odd", 130},
        { "Red", 131},
        { "Black", 132},
        { "Column (0 1)",134},
        { "Column 1",135},
        { "Column (1 4)",136},
        { "Column 4",137},
        { "Column (4 7)",138},
        { "Column 7",139},
        { "Column (7 10)",140},
        { "Column 10",141},
        { "Column (10 13)",142},
        { "Column 13",143},
        { "Column (13 16)",144},
        { "Column 16",145},
        { "Column (16 19)",146},
        { "Column 19",147},
        { "Column (19 22)",148},
        { "Column 22",149},
        { "Column (22 25)",150},
        { "Column 25",151},
        { "Column (25 28)",152},
        { "Column 28",153},
        { "Column (28 31)",154},
        { "Column 31",155},
        { "Column (31 34)",156},
        { "Column 34",157}
    };

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        clickable.betSelected += OnBetClicked;
        nmbTriggered += OnNMBTriggered;
        //nmbTriggered += OnNMBTriggered;
        bettingRequests = new List<BettingRequest>();

        //Writing the names of all the objects in the list to a text file
        //for easy copy pasting of names
        //StartCoroutine(WriteToFile());
    }

    //When a bet is clicked on the table.
    private void OnBetClicked(Transform selectedBetTransform)
    {
        if (currentChip.value <= player.playerBalance)
        {
            InstantiateCoin(selectedBetTransform);
            StartCoroutine(BetClickedRoutine(selectedBetTransform, selectedColor));
        }
        else
        {
            StartCoroutine(BetClickedRoutine(selectedBetTransform, lowBalanceColor));
        }
    }

    //Set the color of every sprite on table to transparent.
    private void RevertTableSpriteColor()
    {
        foreach (SpriteRenderer sr in numSprites.spriteRenderers)
        {
            Highlight.HighlightSprite(sr, defaultColor);
        }
    }

    //Things which will happen after clicking a bet on the table.
    IEnumerator BetClickedRoutine(Transform selectedBetTransform, Color highlightColor)
    {
        RevertTableSpriteColor();

        foreach (SpriteRenderer sr in spriteLists[nameToIndexMap[selectedBetTransform.name]].sprites)
        {
            Highlight.HighlightSprite(sr, highlightColor);
        }

        yield return new WaitForSeconds(2f);

        RevertTableSpriteColor();
    }

    //Puts coin on corresponding bet on the table.
    private void InstantiateCoin(Transform selectedBetTransform)
    {
        GameObject coin = Instantiate(coinPrefab, selectedBetTransform.position, Quaternion.Euler(90, 0, 0), coinParent);
        coin.GetComponent<SpriteRenderer>().color = currentChip.coinColor;
        coin.GetComponent<CoinOnTable>().coinValueText.text = currentChip.valueString;

        //Add this coin to the stack so we can undo it when needed
        undo.UpdateCoinList(coin);

        //Update player balance
        player.BetMoney(currentChip.value);

        //Update Bet amount
        bet.UpdateBetAmount(currentChip.value);

        //Update Bet types list to be sent to server
        bettingRequests.Add(new BettingRequest() { betName = selectedBetTransform.name, amount = currentChip.value });
    }

    public /*async Task*/ void OnNMBTriggered()
    {
        connection.Betting(rouletteManager.ConvertBettingData(bettingRequests, "test_id", "test_auth"));
        Debug.Log("OnNMBTriggered");
    }

    private void OnDestroy()
    {
        clickable.betSelected -= OnBetClicked;
        nmbTriggered -= OnNMBTriggered;
    }
    #region utility
    private IEnumerator WriteToFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "MyFile");
        if (!File.Exists(path))
        {
            File.Create(path);
            TextWriter tw = new StreamWriter(path);
            for (int i = 0; i < betTransforms.Count; i++)
            {
                tw.WriteLine(betTransforms[i].name);
                yield return null;
            }
            tw.Close();
        }
        else if (File.Exists(path))
        {
            TextWriter tw = new StreamWriter(path);
            for (int i = 0; i < betTransforms.Count; i++)
            {
                tw.WriteLine(betTransforms[i].name);
                yield return null;
            }
            tw.Close();
        }
    }
    #endregion
}

[Serializable]
public class SpriteList
{
    public string betName;
    public List<SpriteRenderer> sprites;
}