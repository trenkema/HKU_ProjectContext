using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BlinkingLightPuzzle", menuName = "Puzzles/BlinkingLightData/PuzzleData")]
public class BlinkingLightsData : ScriptableObject
{
    public Color[] playerColors;
    public char[] colorFirstChar;
    public Sprite[] colorBlindShapes;
    public List<PatternData> patternList = new List<PatternData>();
}

[System.Serializable]
public class PatternData
{
    public int colorCodeSingleINT;
}
