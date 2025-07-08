using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vuforia;


public class AutoARCrosswordScanner : MonoBehaviour
{
    public TMP_Text debugText;
    public float groupThreshold = 0.1f; // for both rowHeight and columnWidth

    private List<TrackedLetter> trackedLetters = new List<TrackedLetter>();

    class TrackedLetter
    {
        public char letter;
        public Vector3 position;
    }

    void Update()
    {
        trackedLetters.Clear();

        // Find all ImageTargetA to Z
        foreach (char c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            string name = "ImageTarget" + c;
            GameObject obj = GameObject.Find(name);

            if (obj == null) continue;

            TrackableBehaviour tb = obj.GetComponent<TrackableBehaviour>();
            if (tb != null && tb.CurrentStatus == TrackableBehaviour.Status.TRACKED)
            {
                trackedLetters.Add(new TrackedLetter { letter = c, position = obj.transform.position });
            }
        }

        if (trackedLetters.Count == 0) return;

        string horizontal = ScanRows(trackedLetters);
        string vertical = ScanColumns(trackedLetters);

        string output = $"Horizontal:\n{horizontal}\nVertical:\n{vertical}";

        Debug.Log(output);
        if (debugText != null)
            debugText.text = "[ARCrosswordScanner]\n" + output;
    }

    string ScanRows(List<TrackedLetter> letters)
    {
        // The List Structure is --> "rows" of "row" of "TrackedLetter".
        List<List<TrackedLetter>> rows = new List<List<TrackedLetter>>();

        foreach (var letter in letters)
        {
            bool added = false;
            foreach (var row in rows)
            {
                // Check if this letter has the same height as an existing row.
                if (Mathf.Abs(letter.position.y - row[0].position.y) < groupThreshold)
                {
                    row.Add(letter);
                    added = true;
                    break;
                }
            }

            // Create a new row, if all existing rows have no letters being added.
            if (!added)
                rows.Add(new List<TrackedLetter> { letter });
        }

        // Sort rows top to bottom
        rows.Sort((a, b) => b[0].position.y.CompareTo(a[0].position.y));
        // Sort row left to right
        foreach (var row in rows)
            row.Sort((a, b) => a.position.x.CompareTo(b.position.x));

        // Create row UI Text string
        string result = "";
        foreach (var row in rows)
        {
            foreach (var l in row)
                result += l.letter;
            result += ", ";
        }

        return result.Trim();
    }

    string ScanColumns(List<TrackedLetter> letters)
    {
        // The List Structure is --> "rows" of "row" of "TrackedLetter".
        List<List<TrackedLetter>> columns = new List<List<TrackedLetter>>();

        foreach (var letter in letters)
        {
            bool added = false;
            foreach (var col in columns)
            {
                // Check if this letter has the same height as an existing row.
                if (Mathf.Abs(letter.position.x - col[0].position.x) < groupThreshold)
                {
                    col.Add(letter);
                    added = true;
                    break;
                }
            }

            // Create a new row, if all existing rows have no letters being added.
            if (!added)
                columns.Add(new List<TrackedLetter> { letter });
        }

        // Sort columns left to right
        columns.Sort((a, b) => a[0].position.x.CompareTo(b[0].position.x));
        // Sort column top to bottom
        foreach (var col in columns)
            col.Sort((a, b) => b.position.y.CompareTo(a.position.y)); // top to bottom

        // Create row UI Text string
        string result = "";
        foreach (var col in columns)
        {
            foreach (var l in col)
                result += l.letter;
            result += ", ";
        }

        return result.Trim();
    }
}