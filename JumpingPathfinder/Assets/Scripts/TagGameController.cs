using System.Collections.Generic;
using UnityEngine;

public class TagGameController : MonoBehaviour
{
    static bool initTaggedPlayer = false;
    static List<TagPlayer> tagPlayers;

    private static float tagForce = 8;

    private void Awake()
    {
        tagPlayers = new List<TagPlayer>();
    }
    void Start()
    {
        int taggedIndex = Random.Range(0, tagPlayers.Count);
        tagPlayers[taggedIndex].Tag();
    }

    // Update is called once per frame
    void Update()
    {
        if (!initTaggedPlayer)
        {
            initTaggedPlayer = true;
            
        }
    }

    public static float GetTagForce()
    {
        return tagForce;
    }
    public static void AddTagPlayer(TagPlayer player)
    {
        tagPlayers.Add(player);
    }
    public static List<TagPlayer> GetTagPlayers()
    {
        return tagPlayers;
    }



    public static bool GetColorTagged(PlayerColor color)
    {
        for (int i = 0; i < tagPlayers.Count; i++)
        {
            bool colorSame = tagPlayers[i].GetColor() == color;
            if (tagPlayers[i].GetColor() == color && tagPlayers[i].GetTagged())
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public enum PlayerColor
{
    Blue,
    Red
}
