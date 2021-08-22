using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityEngine;

partial class RouletteInput
{
    public string Number { set; get; }
    public int position { set; get; }
    public int[] Probability { set; get; }
}
public partial class BettingRequest
{
    public string betName { get; set; }
    public double amount { get; set; }
}

public class RouletteBettings
{
    public string Memberid { get; set; }

    public string Authentication { get; set; }

    public List<BettingType> TypeBet { set; get; }

    public List<CustomType> CustomBet { set; get; }
}

public partial class BettingType
{
    public int Type { set; get; }
    public double Amount { set; get; }
}

public partial class CustomType
{
    public int Number { set; get; }
    public double Amount { set; get; }
}

public class BettingDetails
{
    public string Member { set; get; }//player name
    public double Amount { set; get; }//bet amount
    public int Gameid { set; get; }//game id
    public string Type { set; get; }//1 to 18 - bet type
}

public class Roulettemanger : MonoBehaviour
{
    List<RouletteInput> nametoindex = new List<RouletteInput>()
        {
            new RouletteInput() { Number = "Number (0)", position = 0, Probability = new int[] { 0 } },
            new RouletteInput() { Number = "Number (1)", position = 1, Probability = new int[] { 1 } },
            new RouletteInput() { Number = "Number (2)", position = 2, Probability = new int[] { 2 } },
            new RouletteInput() { Number = "Number (3)", position = 3, Probability = new int[] { 3 } },
            new RouletteInput() { Number = "Number (4)", position = 4, Probability = new int[] { 4 } },
            new RouletteInput() { Number = "Number (5)", position = 5, Probability = new int[] { 5 } },
            new RouletteInput() { Number = "Number (6)", position = 6, Probability = new int[] { 6 } },
            new RouletteInput() { Number = "Number (7)", position = 7, Probability = new int[] { 7 } },
            new RouletteInput() { Number = "Number (8)", position = 8, Probability = new int[] { 8 } },
            new RouletteInput() { Number = "Number (9)", position = 9, Probability = new int[] { 9 } },
            new RouletteInput() { Number = "Number (10)", position = 10, Probability = new int[] { 10 } },
            new RouletteInput() { Number = "Number (11)", position = 11, Probability = new int[] { 11 } },
            new RouletteInput() { Number = "Number (12)", position = 12, Probability = new int[] { 12 } },
            new RouletteInput() { Number = "Number (13)", position = 13, Probability = new int[] { 13 } },
            new RouletteInput() { Number = "Number (14)", position = 14, Probability = new int[] { 14 } },
            new RouletteInput() { Number = "Number (15)", position = 15, Probability = new int[] { 15 } },
            new RouletteInput() { Number = "Number (16)", position = 16, Probability = new int[] { 16 } },
            new RouletteInput() { Number = "Number (17)", position = 17, Probability = new int[] { 17 } },
            new RouletteInput() { Number = "Number (18)", position = 18, Probability = new int[] { 18 } },
            new RouletteInput() { Number = "Number (19)", position = 19, Probability = new int[] { 19 } },
            new RouletteInput() { Number = "Number (20)", position = 20, Probability = new int[] { 20 } },
            new RouletteInput() { Number = "Number (21)", position = 21, Probability = new int[] { 21 } },
            new RouletteInput() { Number = "Number (22)", position = 22, Probability = new int[] { 22 } },
            new RouletteInput() { Number = "Number (23)", position = 23, Probability = new int[] { 23 } },
            new RouletteInput() { Number = "Number (24)", position = 24, Probability = new int[] { 24 } },
            new RouletteInput() { Number = "Number (25)", position = 25, Probability = new int[] { 25 } },
            new RouletteInput() { Number = "Number (26)", position = 26, Probability = new int[] { 26 } },
            new RouletteInput() { Number = "Number (27)", position = 27, Probability = new int[] { 27 } },
            new RouletteInput() { Number = "Number (28)", position = 28, Probability = new int[] { 28 } },
            new RouletteInput() { Number = "Number (29)", position = 29, Probability = new int[] { 29 } },
            new RouletteInput() { Number = "Number (30)", position = 30, Probability = new int[] { 30 } },
            new RouletteInput() { Number = "Number (31)", position = 31, Probability = new int[] { 31 } },
            new RouletteInput() { Number = "Number (32)", position = 32, Probability = new int[] { 32 } },
            new RouletteInput() { Number = "Number (33)", position = 33, Probability = new int[] { 33 } },
            new RouletteInput() { Number = "Number (34)", position = 34, Probability = new int[] { 34 } },
            new RouletteInput() { Number = "Number (35)", position = 35, Probability = new int[] { 35 } },
            new RouletteInput() { Number = "Number (36)", position = 36, Probability = new int[] { 36 } },
            new RouletteInput() { Number = "Combination (01)", position = 37, Probability = new int[] { 0, 1 } },
            new RouletteInput() { Number = "Combination (012)", position = 38, Probability = new int[] { 0, 1, 2 } },
            new RouletteInput() { Number = "Combination (02)", position = 39, Probability = new int[] { 0, 2 } },
            new RouletteInput() { Number = "Combination (023)", position = 40, Probability = new int[] { 0, 2, 3 }},
            new RouletteInput() { Number = "Combination (03)", position = 41, Probability = new int[] { 0, 3 } },
            new RouletteInput() { Number = "Combination (12)", position = 42, Probability = new int[] { 1, 2 } },
            new RouletteInput() { Number = "Combination (23)", position = 43, Probability = new int[] { 2, 3 } },
            new RouletteInput() { Number = "Combination (14)", position = 44, Probability = new int[] { 1, 4 } },
            new RouletteInput() { Number = "Combination (1245)", position = 45, Probability = new int[] { 1, 2, 4, 5 } },
            new RouletteInput() { Number = "Combination (25)", position = 46, Probability = new int[] { 2, 5 } },
            new RouletteInput() { Number = "Combination (2356)", position = 47, Probability = new int[] { 2, 3, 5, 6 } },
            new RouletteInput() { Number = "Combination (36)", position = 48, Probability = new int[] { 3, 6 } },
            new RouletteInput() { Number = "Combination (45)", position = 49, Probability = new int[] { 4, 5 } },
            new RouletteInput() { Number = "Combination (56)", position = 50, Probability = new int[] { 5, 6 } },
            new RouletteInput() { Number = "Combination (47)", position = 51, Probability = new int[] { 4, 7 } },
            new RouletteInput() { Number = "Combination (4578)", position = 52, Probability = new int[] { 4, 5, 7, 8 } },
            new RouletteInput() { Number = "Combination (58)", position = 53, Probability = new int[] { 5, 8 } },
            new RouletteInput() { Number = "Combination (5689)", position = 54, Probability = new int[] { 5, 6, 8, 9 } },
            new RouletteInput() { Number = "Combination (69)", position = 55, Probability = new int[] { 6, 9 } },
            new RouletteInput() { Number = "Combination (78)", position = 56, Probability = new int[] { 7, 8 } },
            new RouletteInput() { Number = "Combination (89)", position = 57, Probability = new int[] { 8, 9 } },
            new RouletteInput() { Number = "Combination (7 10)", position = 58, Probability = new int[] { 7, 10 } },
            new RouletteInput() { Number = "Combination (7 8 10 11)", position = 59, Probability = new int[] { 7, 8, 10, 11 } },
            new RouletteInput() { Number = "Combination (8 11)", position = 60, Probability = new int[] { 8, 11 } },
            new RouletteInput() { Number = "Combination (8 9 11 12)", position = 61, Probability = new int[] { 8, 9, 11, 12 } },
            new RouletteInput() { Number = "Combination (9 12)", position = 62, Probability = new int[] { 9, 12 } },
            new RouletteInput() { Number = "Combination (10 11)", position = 63, Probability = new int[] { 10, 11 } },
            new RouletteInput() { Number = "Combination (11 12)", position = 64, Probability = new int[] { 11, 12 } },
            new RouletteInput() { Number = "Combination (10 13)", position = 65, Probability = new int[] { 10, 13 } },
            new RouletteInput() { Number = "Combination (10 11 13 14)", position = 66, Probability = new int[] { 10, 11, 13, 14 } },
            new RouletteInput() { Number = "Combination (11 14)", position = 67, Probability = new int[] { 11, 14 } },
            new RouletteInput() { Number = "Combination (11 12 14 15)", position = 68, Probability = new int[] { 11, 12, 14, 15 } },
            new RouletteInput() { Number = "Combination (12 15)", position = 69, Probability = new int[] { 12, 15 } },
            new RouletteInput() { Number = "Combination (13 14)", position = 70, Probability = new int[] { 13, 14 } },
            new RouletteInput() { Number = "Combination (14 15)", position = 71, Probability = new int[] { 14, 15 } },
            new RouletteInput() { Number = "Combination (13 16)", position = 72, Probability = new int[] { 13, 16 } },
            new RouletteInput() { Number = "Combination (13 14 16 17)", position = 73, Probability = new int[] { 13, 14, 16, 17 } },
            new RouletteInput() { Number = "Combination (14 17)", position = 74, Probability = new int[] { 14, 17 } },
            new RouletteInput() { Number = "Combination (14 15 17 18)", position = 75, Probability = new int[] { 14, 15, 17, 18 } },
            new RouletteInput() { Number = "Combination (15 18)", position = 76, Probability = new int[] { 15, 18 } },
            new RouletteInput() { Number = "Combination (16 17)", position = 77, Probability = new int[] { 16, 17 } },
            new RouletteInput() { Number = "Combination (17 18)", position = 78, Probability = new int[] { 17, 18 } },
            new RouletteInput() { Number = "Combination (16 19)", position = 79, Probability = new int[] { 16, 19 } },
            new RouletteInput() { Number = "Combination (16 17 19 20)", position = 80, Probability = new int[] { 16, 17, 19, 20 } },
            new RouletteInput() { Number = "Combination (17 20)", position = 81, Probability = new int[] { 17, 20 } },
            new RouletteInput() { Number = "Combination (17 18 20 21)", position = 82, Probability = new int[] { 17, 18, 20, 21 } },
            new RouletteInput() { Number = "Combination (18 21)", position = 83, Probability = new int[] { 18, 21 } },
            new RouletteInput() { Number = "Combination (19 20)", position = 84, Probability = new int[] { 19, 20 } },
            new RouletteInput() { Number = "Combination (20 21)", position = 85, Probability = new int[] { 20, 21 } },
            new RouletteInput() { Number = "Combination (19 22)", position = 86, Probability = new int[] { 19, 22 } },
            new RouletteInput() { Number = "Combination (19 20 22 23)", position = 87, Probability = new int[] { 19, 20, 22, 23 }},
            new RouletteInput() { Number = "Combination (20 23)", position = 88, Probability = new int[] { 20, 23 } },
            new RouletteInput() { Number = "Combination (20 21 23 24)", position = 89, Probability = new int[] { 20, 21, 23, 24 } },
            new RouletteInput() { Number = "Combination (21 24)", position = 90, Probability = new int[] { 21, 24 } },
            new RouletteInput() { Number = "Combination (22 23)", position = 91, Probability = new int[] { 22, 23 } },
            new RouletteInput() { Number = "Combination (23 24)", position = 92, Probability = new int[] { 23, 24 } },
            new RouletteInput() { Number = "Combination (22 25)", position = 93, Probability = new int[] { 22, 25 } },
            new RouletteInput() { Number = "Combination (22 23 25 26)", position = 94, Probability = new int[] { 22, 23, 25, 26 } },
            new RouletteInput() { Number = "Combination (23 26)", position = 95, Probability = new int[] { 23, 26 } },
            new RouletteInput() { Number = "Combination (23 24 26 27)", position = 96, Probability = new int[] { 23, 24, 26, 27 } },
            new RouletteInput() { Number = "Combination (24 27)", position = 97, Probability = new int[] { 24, 27 } },
            new RouletteInput() { Number = "Combination (25 26)", position = 98, Probability = new int[] { 25, 26 } },
            new RouletteInput() { Number = "Combination (26 27)", position = 99, Probability = new int[] { 26, 27 } },
            new RouletteInput() { Number = "Combination (25 28)", position = 100, Probability = new int[] { 25, 28 } },
            new RouletteInput() { Number = "Combination (25 26 28 29)", position = 101, Probability = new int[] { 25, 26, 28, 29 } },
            new RouletteInput() { Number = "Combination (26 29)", position = 102, Probability = new int[] { 26, 29 } },
            new RouletteInput() { Number = "Combination (26 27 29 30)", position = 103, Probability = new int[] { 26, 27, 29, 30 } },
            new RouletteInput() { Number = "Combination (27 30)", position = 104, Probability = new int[] { 27, 30 } },
            new RouletteInput() { Number = "Combination (28 29)", position = 105, Probability = new int[] { 28, 29 } },
            new RouletteInput() { Number = "Combination (29 30)", position = 106, Probability = new int[] { 29, 30 } },
            new RouletteInput() { Number = "Combination (28 31)", position = 107, Probability = new int[] { 28, 31 } },
            new RouletteInput() { Number = "Combination (28 29 31 32)", position = 108, Probability = new int[] { 28, 29, 31, 32 } },
            new RouletteInput() { Number = "Combination (29 32)", position = 109, Probability = new int[] { 29, 32 } },
            new RouletteInput() { Number = "Combination (29 30 32 33)", position = 110, Probability = new int[] { 29, 30, 32, 33 } },
            new RouletteInput() { Number = "Combination (30 33)", position = 111, Probability = new int[] { 30, 33 } },
            new RouletteInput() { Number = "Combination (31 32)", position = 112, Probability = new int[] { 31, 32 } },
            new RouletteInput() { Number = "Combination (32 33)", position = 113, Probability = new int[] { 32, 33 } },
            new RouletteInput() { Number = "Combination (31 34)", position = 114, Probability = new int[] { 31, 34 } },
            new RouletteInput() { Number = "Combination (31 32 34 35)", position = 115, Probability = new int[] { 31, 32, 34, 35 } },
            new RouletteInput() { Number = "Combination (32 35)", position = 116, Probability = new int[] { 32, 35 } },
            new RouletteInput() { Number = "Combination (32 33 35 36)", position = 117, Probability = new int[] { 32, 33, 35, 36 } },
            new RouletteInput() { Number = "Combination (33 36)", position = 118, Probability = new int[] { 33, 36 } },
            new RouletteInput() { Number = "Combination (34 35)", position = 119, Probability = new int[] { 34, 35 } },
            new RouletteInput() { Number = "Combination (35 36)", position = 120, Probability = new int[] { 35, 36 } },
            new RouletteInput() { Number = "1st 12", position = 121, Probability = new int[] { 121 } },
            new RouletteInput() { Number = "2nd 12", position = 122, Probability = new int[] { 122 } },
            new RouletteInput() { Number = "3rd 12", position = 123, Probability = new int[] { 123 } },
            new RouletteInput() { Number = "1st Row (2 to 1)", position = 124, Probability = new int[] { 124 } },
            new RouletteInput() { Number = "2nd Row (2 to 1)", position = 125, Probability = new int[] { 125 } },
            new RouletteInput() { Number = "3rd Row (2 to 1)", position = 126, Probability = new int[] { 126 } },
            new RouletteInput() { Number = "Small (1 - 18)", position = 127, Probability = new int[] { 127 } },
            new RouletteInput() { Number = "Big (19 - 36)", position = 128, Probability = new int[] { 128 } },
            new RouletteInput() { Number = "Even", position = 129, Probability = new int[] { 129 } },
            new RouletteInput() { Number = "Odd", position = 130, Probability = new int[] { 130 } },
            new RouletteInput() { Number = "Red", position = 131, Probability = new int[] { 131 } },
            new RouletteInput() { Number = "Black", position = 132, Probability = new int[] { 132 } },
            new RouletteInput() { Number = "Column (0 1)", position = 134, Probability = new int[] { 0, 1, 2, 3 } },
            new RouletteInput() { Number = "Column 1", position = 135, Probability = new int[] { 1, 2, 3 } },
            new RouletteInput() { Number = "Column (1 4)", position = 136, Probability = new int[] { 1, 2, 3, 4, 5, 6 } },
            new RouletteInput() { Number = "Column 4", position = 137, Probability = new int[] { 4, 5, 6 } },
            new RouletteInput() { Number = "Column (4 7)", position = 138, Probability = new int[] { 4, 5, 6, 7, 8, 9 } },
            new RouletteInput() { Number = "Column 7", position = 139, Probability = new int[] { 7, 8, 9 } },
            new RouletteInput() { Number = "Column (7 10)", position = 140, Probability = new int[] { 7, 8, 9, 10, 11, 12 } },
            new RouletteInput() { Number = "Column 10", position = 141, Probability = new int[] { 10, 11, 12 } },
            new RouletteInput() { Number = "Column (10 13)", position = 142, Probability = new int[] { 10, 11, 12, 13, 14, 15 } },
            new RouletteInput() { Number = "Column 13", position = 143, Probability = new int[] { 13, 14, 15 } },
            new RouletteInput() { Number = "Column (13 16)", position = 144, Probability = new int[] { 13, 14, 15, 16, 17, 18 } },
            new RouletteInput() { Number = "Column 16", position = 145, Probability = new int[] { 16, 17, 18 } },
            new RouletteInput() { Number = "Column (16 19)", position = 146, Probability = new int[] { 16, 17, 18, 19, 20, 21 }},
            new RouletteInput() { Number = "Column 19", position = 147, Probability = new int[] { 19, 20, 21 } },
            new RouletteInput() { Number = "Column (19 22)", position = 148, Probability = new int[] { 19, 20, 21, 22, 23, 24 } },
            new RouletteInput() { Number = "Column 22", position = 149, Probability = new int[] { 22, 23, 24 } },
            new RouletteInput() { Number = "Column (22 25)", position = 150, Probability = new int[] { 22, 23, 24, 25, 26, 27 } },
            new RouletteInput() { Number = "Column 25", position = 151, Probability = new int[] { 25, 26, 27 } },
            new RouletteInput() { Number = "Column (25 28)", position = 152, Probability = new int[] { 25, 26, 27, 28, 29, 30 } },
            new RouletteInput() { Number = "Column 28", position = 153, Probability = new int[] { 28, 29, 30 } },
            new RouletteInput() { Number = "Column (28 31)", position = 154, Probability = new int[] { 28, 29, 30, 31, 32, 33 } },
            new RouletteInput() { Number = "Column 31", position = 155, Probability = new int[] { 31, 32, 33 } },
            new RouletteInput() { Number = "Column (31 34)", position = 156, Probability = new int[] { 31, 32, 33, 34, 35, 36 } },
            new RouletteInput() { Number = "Column 34", position = 157, Probability = new int[] { 34, 35, 36 } }
        };

    public RouletteBettings ConvertBettingData(List<BettingRequest> bet = null, string memberid = null, string authentication = null)
    {
        List<BettingType> type = new List<BettingType>();
        List<CustomType> customtype = new List<CustomType>();

        int count = bet.Count();
        Debug.Log("Bet Count:" + count);

        for (int i = 0; i < count; i++)
        {
            var betj = nametoindex.FirstOrDefault(e => e.Number == bet[i].betName);

            if (betj.position >= 121 && betj.position <= 132)
            {
                type.Add(new BettingType() { Type = BettingMethod(betj.position), Amount = bet[i].amount });
            }
            else
            {
                int[] bets = betj.Probability;
                int betu = bets.Length;
                double amount = bet[i].amount / betu;
                for (int k = 0; k < betu; k++)
                {
                    customtype.Add(new CustomType() { Number = bets[k], Amount = amount });
                }
            }
        }
        return new RouletteBettings() { Memberid = memberid, Authentication = authentication, CustomBet = customtype, TypeBet = type };
    }

    private int BettingMethod(int position)
    {
        if (position == 121) { return 1; } //1st row
        else if (position == 122) { return 2; }//2st row
        else if (position == 123) { return 3; }////3st row
        else if (position == 124) { return 4; }//1st row
        else if (position == 125) { return 5; }//2nd row
        else if (position == 126) { return 6; }//3rd row
        else if (position == 127) { return 7; }//1-18
        else if (position == 128) { return 8; }//19-36
        else if (position == 129) { return 9; }//Even
        else if (position == 130) { return 10; }//ODD
        else if (position == 131) { return 11; }//Red
        else if (position == 132) { return 12; }//Black
        else { return 0; }
    }
}