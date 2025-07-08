using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;


public class AutoARCrosswordScanner : MonoBehaviour
{
    public TMP_Text debugText;
    public float rowHeight = 0.1f;

    private List<TrackedLetter> trackedLetters = new List<TrackedLetter>();

    void Update()
    {
        // Clear the list each frame
        trackedLetters.Clear();

        // Find all objects named "ImageTargetA" to "ImageTargetZ"
        foreach (char c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            string name = "ImageTarget" + c;
            GameObject obj = GameObject.Find(name);

            if (obj == null) continue;

            // Check if it is being tracked
            TrackableBehaviour tb = obj.GetComponent<TrackableBehaviour>();
            if (tb != null && tb.CurrentStatus == TrackableBehaviour.Status.TRACKED)
            {
                trackedLetters.Add(new TrackedLetter { letter = c, position = obj.transform.position });
            }
        }

        if (trackedLetters.Count == 0) return;

        // Group into rows
        List<List<TrackedLetter>> rows = new List<List<TrackedLetter>>();

        foreach (var letter in trackedLetters)
        {
            bool added = false;

            foreach (var row in rows)
            {
                if (Mathf.Abs(letter.position.y - row[0].position.y) < rowHeight)
                {
                    row.Add(letter);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                rows.Add(new List<TrackedLetter> { letter });
            }
        }

        // Sort rows top to bottom
        rows.Sort((a, b) => b[0].position.y.CompareTo(a[0].position.y));
        foreach (var row in rows)
        {
            row.Sort((a, b) => a.position.x.CompareTo(b.position.x));
        }

        string output = "";
        foreach (var row in rows)
        {
            foreach (var letter in row)
            {
                output += letter.letter;
            }
            output += "\n";
        }

        Debug.Log("Detected letters:\n" + output);
        if (debugText != null)
        {
            debugText.text = output;
        }
    }

    class TrackedLetter
    {
        public char letter;
        public Vector3 position;
    }
}
