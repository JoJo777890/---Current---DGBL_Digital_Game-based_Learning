using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Vuforia;

public class WordRecognizer : MonoBehaviour
{

    [System.Serializable]
    public class LetterTarget
    {
        public char letter;
        public GameObject targetObject;

        [HideInInspector]
        public TrackableBehaviour trackable;
    }

    public List<LetterTarget> letterTargets;  // All letter image targets
    public GameObject anchorTarget;           // Fixed image target (e.g. "c")
    public float maxSpacing = 0.3f;           // Max X gap (in meters)
    public float maxYDifference = 0.05f;      // Max height difference (in meters)
    public TextMeshProUGUI uiText;

    private HashSet<string> validWords = new HashSet<string> { "cat", "act", "bat", "cab", "rat", "mat" };
    private HashSet<string> detectedWords = new HashSet<string>();

    void Start()
    {
        // Get TrackableBehaviour components
        foreach (var target in letterTargets)
        {
            target.trackable = target.targetObject.GetComponent<TrackableBehaviour>();
        }
    }

    void Update()
    {
        // Collect tracked letter targets
        var visibleLetters = new List<(char letter, Vector3 localPos)>();

        foreach (var target in letterTargets)
        {
            if (target.trackable == null)
                continue;

            var status = target.trackable.CurrentStatus;

            // Use TRACKED and optionally EXTENDED_TRACKED
            if (status == TrackableBehaviour.Status.TRACKED || status == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                Vector3 relativePos = anchorTarget.transform.InverseTransformPoint(target.targetObject.transform.position);
                visibleLetters.Add((target.letter, relativePos));
            }
        }

        if (visibleLetters.Count != 3)
        {
            uiText.text = "Place 3 visible letters side by side.";
            return;
        }

        // Sort letters by X-axis (left to right)
        visibleLetters.Sort((a, b) => a.localPos.x.CompareTo(b.localPos.x));
        string sequence = new string(visibleLetters.Select(l => l.letter).ToArray());

        // Spacing and alignment
        float spacing1 = Mathf.Abs(visibleLetters[1].localPos.x - visibleLetters[0].localPos.x);
        float spacing2 = Mathf.Abs(visibleLetters[2].localPos.x - visibleLetters[1].localPos.x);
        float yDiff1 = Mathf.Abs(visibleLetters[1].localPos.y - visibleLetters[0].localPos.y);
        float yDiff2 = Mathf.Abs(visibleLetters[2].localPos.y - visibleLetters[1].localPos.y);

        bool wellSpaced = spacing1 <= maxSpacing && spacing2 <= maxSpacing;
        bool wellAligned = yDiff1 <= maxYDifference && yDiff2 <= maxYDifference;

        // Check if it's a valid word
        bool isValid = validWords.Contains(sequence);
        bool isNew = !detectedWords.Contains(sequence);

        // UI Output
        uiText.text = $"Letter Order: {sequence}\nSpacing: {spacing1:F3}, {spacing2:F3}\nY diff: {yDiff1:F3}, {yDiff2:F3}";

        if (wellSpaced && wellAligned && isValid && isNew)
        {
            detectedWords.Add(sequence);
            uiText.text += $"\n✅ Word Detected: {sequence}";
            Debug.Log($"Detected word 2: {sequence}");
        }
    }
}