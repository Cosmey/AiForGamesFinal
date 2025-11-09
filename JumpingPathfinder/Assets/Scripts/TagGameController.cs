using System.Collections.Generic;
using UnityEngine;

public class TagGameController : MonoBehaviour
{
    static bool initTaggedPlayer = false;
    static List<TagPlayer> tagPlayers;


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

    public static void AddTagPlayer(TagPlayer player)
    {
        tagPlayers.Add(player);
    }
    public static List<TagPlayer> GetTagPlayers()
    {
        return tagPlayers;
    }
}
