//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public class BettingOdds : MonoBehaviour
//{
//    [SerializeField] private Clickable clickable;
//    [SerializeField] private float highlightAlphaFactor = 0.5f;
//    [Space(20)]
//    [SerializeField] List<Transform> betTransforms;
//    [SerializeField] List<SpriteList> spriteLists;

//    #region Optional alternative implementations
//    // Imlementation by creating an betName to spriteList index map.
//    //private Dictionary<string, int> nameToIndexMap = new Dictionary<string, int>()
//    //{
//    //    { "", 0},
//    //    { "", 1},
//    //    { "", 2},
//    //    { "", 3},
//    //    { "", 4},
//    //    { "", 5},
//    //    { "", 6},
//    //    { "", 7},
//    //    { "", 8},
//    //    { "", 9},
//    //    { "", 10},
//    //    { "", 11},
//    //    { "", 12},
//    //    { "", 13},
//    //    { "", 14},
//    //    { "", 15},
//    //    { "", 16},
//    //    { "", 17},
//    //    { "", 18},
//    //    { "", 19},
//    //    { "", 20},
//    //    { "", 21},
//    //    { "", 22},
//    //    { "", 23},
//    //    { "", 24},
//    //    { "", 25},
//    //    { "", 26},
//    //    { "", 27},
//    //    { "", 28},
//    //    { "", 29},
//    //    { "", 30},
//    //    { "", 31},
//    //    { "", 32},
//    //    { "", 33},
//    //    { "", 34},
//    //    { "", 35},
//    //    { "", 36},
//    //    { "", 37},
//    //    { "", 38},
//    //    { "", 39},
//    //    { "", 40},
//    //    { "", 41},
//    //    { "", 42},
//    //    { "", 43},
//    //    { "", 44},
//    //    { "", 45},
//    //    { "", 46},
//    //    { "", 47},
//    //    { "", 48},
//    //    { "", 49},
//    //    { "", 50},
//    //    { "", 51},
//    //    { "", 52},
//    //    { "", 53},
//    //    { "", 54},
//    //    { "", 55},
//    //    { "", 56},
//    //    { "", 57},
//    //    { "", 58},
//    //    { "", 59},
//    //    { "", 60},
//    //    { "", 61},
//    //    { "", 62},
//    //    { "", 63},
//    //    { "", 64},
//    //    { "", 65},
//    //    { "", 66},
//    //    { "", 67},
//    //    { "", 68},
//    //    { "", 69},
//    //    { "", 70},
//    //    { "", 71},
//    //    { "", 72},
//    //    { "", 73},
//    //    { "", 74},
//    //    { "", 75},
//    //    { "", 76},
//    //    { "", 77},
//    //    { "", 78},
//    //    { "", 79},
//    //    { "", 80},
//    //    { "", 81},
//    //    { "", 82},
//    //    { "", 83},
//    //    { "", 84},
//    //    { "", 85},
//    //    { "", 86},
//    //    { "", 87},
//    //    { "", 88},
//    //    { "", 89},
//    //    { "", 90},
//    //    { "", 91},
//    //    { "", 92},
//    //    { "", 93},
//    //    { "", 94},
//    //    { "", 95},
//    //    { "", 96},
//    //    { "", 97},
//    //    { "", 98},
//    //    { "", 99},
//    //    { "", 100},
//    //    { "", 101},
//    //    { "", 102},
//    //    { "", 103},
//    //    { "", 104},
//    //    { "", 105},
//    //    { "", 106},
//    //    { "", 107},
//    //    { "", 108},
//    //    { "", 109},
//    //    { "", 110},
//    //    { "", 111},
//    //    { "", 112},
//    //    { "", 113},
//    //    { "", 114},
//    //    { "", 115},
//    //    { "", 116},
//    //    { "", 117},
//    //    { "", 118},
//    //    { "", 119},
//    //    { "", 120},
//    //    { "", 121},
//    //    { "", 122},
//    //    { "", 123},
//    //    { "", 124},
//    //    { "", 125},
//    //    { "", 126},
//    //    { "", 127},
//    //    { "", 128},
//    //    { "", 129},
//    //    { "", 130},
//    //    { "", 131},
//    //    { "", 132},
//    //    { "", 133}
//    //};

//    //Implementation with serializable dictionary
//    //[SerializeField] private List<BetCombination> betCombinations;

//    //Impleentation with Serializable class structure
//    //public SerializableCombination betMappings;

//    //Implementation with lists
//    //[SerializeField] List<Transform> betTransforms;
//    //[SerializeField] List<SpriteList> spriteLists;
//    #endregion
    
//    private void Awake()
//    {
//        clickable.betSelected += OnBetClicked;
//    }

//    private void OnBetClicked(Transform go)
//    {
//        SpriteRenderer sRenderer = go.GetComponent<SpriteRenderer>();
//        Highlight.HighlightSprite(sRenderer, new Color(sRenderer.color.r, sRenderer.color.g, sRenderer.color.b, highlightAlphaFactor));
//    }
//}

//[Serializable]
//public class BetCombination
//{
//    public string betName;
//    public Transform betTransform;
//    public SpriteList possibleWinningNumbers;
//}

//[Serializable]
//public class SerializableCombination : Dictionary<Transform, SpriteList>, ISerializationCallbackReceiver
//{
//    [SerializeField]
//    private List<Transform> keys = new List<Transform>();

//    [SerializeField]
//    private List<SpriteList> values = new List<SpriteList>();

//    // save the dictionary to lists
//    public void OnBeforeSerialize()
//    {
//        keys.Clear();
//        values.Clear();
//        foreach (KeyValuePair<Transform, SpriteList> pair in this)
//        {
//            keys.Add(pair.Key);
//            values.Add(pair.Value);
//        }
//    }

//    // load dictionary from lists
//    public void OnAfterDeserialize()
//    {
//        this.Clear();

//        if (keys.Count != values.Count)
//            throw new Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

//        for (int i = 0; i < keys.Count; i++)
//            this.Add(keys[i], values[i]);
//    }
//}

//[Serializable]
//public class SpriteList
//{
//    public List<SpriteRenderer> sprites;
//}