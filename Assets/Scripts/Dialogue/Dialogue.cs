using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public int charId;
    public int priority;
    public Subtitle[] subtitles;
}

[Serializable]
public struct Subtitle
{
    public string text;
    public AudioClip speech;
}

