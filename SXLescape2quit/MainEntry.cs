using System.Reflection;
using UnityEngine;
using UnityModManagerNet;
using HarmonyLib;


//this script is how to get code run in SXL via UMM
//
//this is the link I followed for this entry method
//wiki.nexusmods.com/index.php/How_to_create_mod_for_unity_game
//
//just make sure namespaces(\/) and references(/\) are setup right
//
namespace SXLescape2quit
{
	static class MainEntry
	{
		public static bool Enabled { get; private set; }
		private static Harmony Harmony { get; set; }

		public static UnityModManager.ModEntry mod;
		public static bool Load(UnityModManager.ModEntry modEntry)
		{
			var harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());
			//lol lmao

			mod = modEntry;
			modEntry.OnToggle = OnToggle;
			//modEntry.OnUpdate = OnUpdate; 

			return true;
		}

		private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
		{
			if (Enabled == value) return true;
			Enabled = value;

			if (Enabled)
			{
				Harmony = new Harmony(modEntry.Info.Id);
				Harmony.PatchAll(Assembly.GetExecutingAssembly());

				SpawnerSetup.CreateSpawnerGO();     //the only thing I technically "wrote" myself in this class
			}
			else
			{
				Harmony.UnpatchAll(Harmony.Id);
			}

			return true;
		}

		//modEntry.OnUpdate can't be combined with .OnToggle (as far as I know)
		/*public static void OnUpdate(UnityModManager.ModEntry modEntry, float dt)
		{
			if (Input.GetKeyUp(KeyCode.F1))
			{
				UnityModManager.Logger.Log("F1 pressed");
			}
		}*/
	}
}


//this script is how to create a method that gets called by UMM
//
//CreateSpawnerGO() creates an empty gameObject that can run any MonoBehaviour script
//
namespace SXLescape2quit
{
	public class SpawnerSetup : MonoBehaviour
	{
		public static GameObject SkaterPrefab;

		public static void CreateSpawnerGO()
		{
			GameObject EmptySpawner = new GameObject("E2Qspawner");	//creates empty gameObject
			EmptySpawner.AddComponent<E2Qempty>();  //adds custom MonoBehaviour script to it
			DontDestroyOnLoad(EmptySpawner);    //prevents object from being destroyed

			UnityModManager.Logger.Log("Escape2Quit Successfully Loaded");
		}
	}
}