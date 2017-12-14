using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer {

	public static GameObject sceneFacilitiesGO;
	public static GameObject axiceGO;
	public static Transform sroTF;

	[RuntimeInitializeOnLoadMethod]
	public static void InitializeGame() {
		/* 注意这里的GO是prefab */
		sceneFacilitiesGO = Resources.Load("Scene Facilities") as GameObject;
		axiceGO = Resources.Load("Axice") as GameObject;

		/* 这里的GO才是scene中的GO */
		sceneFacilitiesGO = Object.Instantiate(sceneFacilitiesGO);
		axiceGO = Object.Instantiate(axiceGO);
		sroTF = sceneFacilitiesGO.transform.Find("SmoothRotatableTarget");

		Object.DontDestroyOnLoad(sceneFacilitiesGO);
		Object.DontDestroyOnLoad(axiceGO);
	}


}
