using UnityEngine;

[CreateAssetMenu()]
public class TutorialSO : ScriptableObject
{
    public string Heading;
    [TextArea(3,10)]
    public string Content;
}
