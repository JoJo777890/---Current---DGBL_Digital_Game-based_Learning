using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Vuforia;

public class WordRecognizerHorizontal : MonoBehaviour
{
    [System.Serializable]
    public class LetterTarget
    {
        public char letter;
        public GameObject targetObject;

        [HideInInspector]
        public TrackableBehaviour trackable;
    }

    public List<LetterTarget> letterTargets;  // All possible letter targets in scene
    public GameObject anchorTarget;           // Fixed anchor (image target)
    public float maxSpacing = 0.1f;           // Max allowed X distance between letters
    public float maxYDifference = 0.02f;      // Max allowed height difference
    public Text uiText;

    private HashSet<string> validWords = new HashSet<string>();
    private HashSet<string> detectedWords = new HashSet<string>();

    void Start()
    {
        // Assign trackables
        foreach (var target in letterTargets)
        {
            target.trackable = target.targetObject.GetComponent<TrackableBehaviour>();
        }

        // Load words from Resources/wordlist.txt
        LoadWordList("wordlist");
    }

    void LoadWordList(string filename)
    {
        TextAsset wordFile = Resources.Load<TextAsset>(filename);
        if (wordFile == null)
        {
            Debug.LogError("❌ Could not load word list!");
            return;
        }

        foreach (string line in wordFile.text.Split('\n'))
        {
            string word = line.Trim().ToLower();
            if (word.Length >= 3 && word.Length <= 5)
            {
                validWords.Add(word);
            }
        }

        Debug.Log($"✅ Loaded {validWords.Count} words.");
    }

    void Update()
    {
        // Step 1: Collect tracked letters
        var visibleLetters = new List<(char letter, Vector3 localPos)>();

        foreach (var target in letterTargets)
        {
            if (target.trackable == null) continue;

            var status = target.trackable.CurrentStatus;
            if (status == TrackableBehaviour.Status.TRACKED || status == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                Vector3 localPos = anchorTarget.transform.InverseTransformPoint(target.targetObject.transform.position);
                visibleLetters.Add((target.letter, localPos));
            }
        }

        // Step 2: Only process if 3–5 letters are tracked
        if (visibleLetters.Count < 3 || visibleLetters.Count > 5)
        {
            uiText.text = $"Track 3–5 letters to detect a word.";
            return;
        }

        // Step 3: Sort by X position (left to right)
        visibleLetters.Sort((a, b) => a.localPos.x.CompareTo(b.localPos.x));
        string sequence = new string(visibleLetters.Select(l => l.letter).ToArray()).ToLower();

        // Step 4: Check spacing and alignment
        bool wellSpaced = true;
        bool wellAligned = true;

        for (int i = 0; i < visibleLetters.Count - 1; i++)
        {
            float xDist = Mathf.Abs(visibleLetters[i + 1].localPos.x - visibleLetters[i].localPos.x);
            float yDiff = Mathf.Abs(visibleLetters[i + 1].localPos.y - visibleLetters[i].localPos.y);

            if (xDist > maxSpacing) wellSpaced = false;
            if (yDiff > maxYDifference) wellAligned = false;
        }

        // Step 5: Check for a valid word
        bool isValid = validWords.Contains(sequence);
        bool isNew = !detectedWords.Contains(sequence);

        // Step 6: UI Feedback
        uiText.text = $"Letters: {sequence}\n" +
                      $"Spacing OK: {wellSpaced}\n" +
                      $"Y Alignment OK: {wellAligned}";

        if (wellSpaced && wellAligned && isValid && isNew)
        {
            detectedWords.Add(sequence);
            uiText.text += $"\n✅ Word Detected: {sequence}";
            Debug.Log($"✅ Detected word 3: {sequence}");
        }
    }
}