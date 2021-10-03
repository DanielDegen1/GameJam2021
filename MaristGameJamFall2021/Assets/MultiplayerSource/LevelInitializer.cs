using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] PlayerSpawns;

    [SerializeField]
    private GameObject player1Prefab;

    [SerializeField]
    private GameObject player2Prefab;
    // Start is called before the first frame update
    void Start()
    {
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            //var player = Instantiate(playerPrefab, PlayerSpawns[i].position, PlayerSpawns[i].rotation, gameObject.transform);
            //layer.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
        }
        Instantiate(player1Prefab);
        Instantiate(player2Prefab);
        player1Prefab.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[0]);
        player2Prefab.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[1]);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
