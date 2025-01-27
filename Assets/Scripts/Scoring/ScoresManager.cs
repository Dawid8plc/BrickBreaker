using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class ScoresManager : MonoBehaviour
{
    static string DataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BrickBreaker");

    static List<ScoreEntry> Scores = new List<ScoreEntry>();

    static XmlSerializer serializer;

    public static void Initialize()
    {
        serializer = new XmlSerializer(typeof(List<ScoreEntry>));
        LoadScores();
    }

    public static void LoadScores()
    {
        if (!Directory.Exists(DataDir))
        {
            Directory.CreateDirectory(DataDir);
        }

        if (!File.Exists(Path.Combine(DataDir, "Scores.xml")))
        {
            FileStream stream = File.Create(Path.Combine(DataDir, "Scores.xml"));

            serializer.Serialize(stream, Scores);

            stream.Close();
        }
        else
        {
            FileStream stream = File.OpenRead(Path.Combine(DataDir, "Scores.xml"));

            Scores = (List<ScoreEntry>)serializer.Deserialize(stream);

            stream.Close();
        }
    }

    public static void SaveScores()
    {
        FileStream stream = File.Create(Path.Combine(DataDir, "Scores.xml"));

        serializer.Serialize(stream, Scores);

        stream.Close();
    }

    public static void AddScore(ScoreEntry entry)
    {
        Scores.Add(entry);

        Scores = Scores.OrderByDescending(x => x.GetScore()).ToList();

        if (Scores.Count > 7)
        {
            Scores.RemoveRange(7, Scores.Count - 7);
        }
    }

    public static void ClearScores()
    {
        Scores.Clear();
    }

    public static List<ScoreEntry> GetScores()
    {
        return Scores;
    }
}
