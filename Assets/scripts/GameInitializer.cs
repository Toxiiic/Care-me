using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer {

	public static GameObject sceneFacilitiesGO;
	public static GameObject axiceGO;

	[RuntimeInitializeOnLoadMethod]
	public static void InitializeGame() {
		sceneFacilitiesGO = Resources.Load("Scene Facilities") as GameObject;
		axiceGO = Resources.Load("Axice") as GameObject;
		
		sceneFacilitiesGO = Object.Instantiate(sceneFacilitiesGO);
		axiceGO = Object.Instantiate(axiceGO);

		Object.DontDestroyOnLoad(sceneFacilitiesGO);
		Object.DontDestroyOnLoad(axiceGO);
	}


}
