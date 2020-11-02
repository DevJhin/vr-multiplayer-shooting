using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterGameSettings
{
    public const float ASTEROIDS_MIN_SPAWN_TIME = 5.0f;
    public const float ASTEROIDS_MAX_SPAWN_TIME = 10.0f;

    public const float PLAYER_RESPAWN_TIME = 4.0f;

    public const int PLAYER_MAX_LIVES = 3;

    public const string PLAYER_LIVES = "PlayerLives";
    public const string PLAYER_READY = "IsPlayerReady";
    public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";

    public const string GAME_LEVEL_NAME = "GameScene";

    public const int GAME_PLAYTIME_SECONDS = 500;

    public const string PLAYER_AVATAR = "PLAYER_AVATAR";


    public static Color GetPlayerColor(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0: return Color.red;
            case 1: return Color.green;
            case 2: return Color.blue;
            case 3: return Color.yellow;
            case 4: return Color.cyan;
            case 5: return Color.grey;
            case 6: return Color.magenta;
            case 7: return Color.white;
        }

        return Color.black;
    }
}
