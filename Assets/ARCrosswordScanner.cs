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
        List<List<TrackedLetter>> rows = new List<List<TrackedLetter>>();

        foreach (var letter in letters)
        {
            bool added = false;
            foreach (var row in rows)
            {
                if (Mathf.Abs(letter.position.y - row[0].position.y) < groupThreshold)
                {
                    row.Add(letter);
                    added = true;
                    break;
                }
            }

            // Create a new row if the current row has no letters added.
            if (!added)
                rows.Add(new List<TrackedLetter> { letter });
        }

        // Sort rows top to bottom
        rows.Sort((a, b) => b[0].position.y.CompareTo(a[0].position.y));
        foreach (var row in rows)
            row.Sort((a, b) => a.position.x.CompareTo(b.position.x));

        string result = "";
        foreach (var row in rows)
        {
            foreach (var l in row)
                result += l.letter;
            result += ", "; // Originally "\n"
        }

        return result.Trim();
    }

    string ScanColumns(List<TrackedLetter> letters)
    {
        List<List<TrackedLetter>> columns = new List<List<TrackedLetter>>();

        foreach (var letter in letters)
        {
            bool added = false;
            foreach (var col in columns)
            {
                if (Mathf.Abs(letter.position.x - col[0].position.x) < groupThreshold)
                {
                    col.Add(letter);
                    added = true;
                    break;
                }
            }
            if (!added)
                columns.Add(new List<TrackedLetter> { letter });
        }

        // Sort columns left to right
        columns.Sort((a, b) => a[0].position.x.CompareTo(b[0].position.x));
        foreach (var col in columns)
            col.Sort((a, b) => b.position.y.CompareTo(a.position.y)); // top to bottom

        string result = "";
        foreach (var col in columns)
        {
            foreach (var l in col)
                result += l.letter;
            result += ", "; // Originally "\n"
        }

        return result.Trim();
    }

    class TrackedLetter
    {
        public char letter;
        public Vector3 position;
    }
}