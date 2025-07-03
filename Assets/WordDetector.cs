using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordDetector : MonoBehaviour
{
    public GameObject anchor;  // Typically the 'c' image
    public GameObject letterC;
    public GameObject letterA;
    public GameObject letterT;
    public float maxLetterSpacing = 0.3f; // Max X distance between letters in meters
    public TextMeshProUGUI uiText;

    private bool wordDetected = false;
    private List<string> recognizedWords = new List<string>();

    void Update()
    {
        // All letters must be tracked
        if (!letterC.activeInHierarchy || !letterA.activeInHierarchy || !letterT.activeInHierarchy)
        {
            uiText.text = "Waiting for all letters...";
            wordDetected = false; // Reset if any disappear
            return;
        }

        // Get positions relative to anchor
        Vector3 cPos = anchor.transform.InverseTransformPoint(letterC.transform.position);
        Vector3 aPos = anchor.transform.InverseTransformPoint(letterA.transform.position);
        Vector3 tPos = anchor.transform.InverseTransformPoint(letterT.transform.position);

        // Sort letters by X position
        List<(char letter, float x)> letters = new List<(char, float)>
        {
            ('c', cPos.x),
            ('a', aPos.x),
            ('t', tPos.x)
        };
        letters.Sort((a, b) => a.x.CompareTo(b.x)); // Sort by X axis

        string sequence = $"{letters[0].letter}{letters[1].letter}{letters[2].letter}";

        float spacing1 = Mathf.Abs(letters[1].x - letters[0].x);
        float spacing2 = Mathf.Abs(letters[2].x - letters[1].x);

        uiText.text = $"[Cat Detector]\n" +
                      $"Letter Order: {sequence}\n" +
                      $"Spacing: {spacing1:F3}, {spacing2:F3}\n" +
                      $"Word Detector: ";

        // Detect left-to-right "cat" within spacing
        if (sequence == "cat" && spacing1 <= maxLetterSpacing && spacing2 <= maxLetterSpacing)
        {
            if (!wordDetected)
            {
                recognizedWords.Add("cat");
                wordDetected = true;
                uiText.text += "Detected: cat!";
                Debug.Log("Word 'cat' detected!");
            }
        }
        else
        {
            wordDetected = false;
        }
    }
}
