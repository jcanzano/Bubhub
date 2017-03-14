using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class PlayerColorHook : LobbyHook {

	public override void OnLobbyServerSceneLoadedForPlayer(
								NetworkManager manager, 
								GameObject lobbyPlayer,
								GameObject gamePlayer) {
		LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer> ();
		BoyColor boy = gamePlayer.GetComponent<BoyColor> ();

		boy.color = lobby.playerColor;
	}
}
