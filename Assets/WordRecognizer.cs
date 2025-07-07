using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Vuforia;

public class WordRecognizer : MonoBehaviour
{
    [System.Serializable]
    public class LetterTarget
    {
        public char letter;
        public GameObject targetObject;
    }

    public List<LetterTarget> letterTargets;  // Assign all letter targets (e.g. c, a, t)
    public GameObject anchorTarget;           // Fixed anchor
    public float maxSpacing = 0.1f;           // Max X gap between letters
    public float maxYDifference = 0.02f;      // Max Y offset allowed between letters
    public Text uiText;

    private HashSet<string> validWords = new HashSet<string> { "cat", "act", "bat", "cab", "rat", "mat" };
    private HashSet<string> detectedWords = new HashSet<string>();

    void Update()
    {
        // Step 1: Gather all currently visible letters
        var visibleLetters = new List<(char letter, Vector3 localPos)>();

        foreach (var target in letterTargets)
        {
            if (target.targetObject.activeInHierarchy)
            {
                Vector3 relativePos = anchorTarget.transform.InverseTransformPoint(target.targetObject.transform.position);
                visibleLetters.Add((target.letter, relativePos));
            }
        }

        if (visibleLetters.Count != 3)
        {
            uiText.text = "Place 3 letters side-by-side.";
            return;
        }

        // Step 2: Sort by X position (left to right)
        visibleLetters.Sort((a, b) => a.localPos.x.CompareTo(b.localPos.x));

        // Step 3: Extract letter sequence
        string sequence = new string(visibleLetters.Select(l => l.letter).ToArray());

        // Step 4: Check X spacing and Y alignment
        float spacing1 = Mathf.Abs(visibleLetters[1].localPos.x - visibleLetters[0].localPos.x);
        float spacing2 = Mathf.Abs(visibleLetters[2].localPos.x - visibleLetters[1].localPos.x);
        float yDiff1 = Mathf.Abs(visibleLetters[1].localPos.y - visibleLetters[0].localPos.y);
        float yDiff2 = Mathf.Abs(visibleLetters[2].localPos.y - visibleLetters[1].localPos.y);

        bool wellSpaced = spacing1 <= maxSpacing && spacing2 <= maxSpacing;
        bool wellAligned = yDiff1 <= maxYDifference && yDiff2 <= maxYDifference;

        // Step 5: Check for valid word
        bool isValidWord = validWords.Contains(sequence);
        bool isNew = !detectedWords.Contains(sequence);

        // Step 6: Display result
        uiText.text = $"Letters: {sequence}\nSpacing: {spacing1:F3}, {spacing2:F3}\nY diff: {yDiff1:F3}, {yDiff2:F3}";

        if (wellSpaced && wellAligned && isValidWord && isNew)
        {
            detectedWords.Add(sequence);
            uiText.text += $"\n✅ Word Found: {sequence}";
            Debug.Log($"Word Detected: {sequence}");
        }
    }
}