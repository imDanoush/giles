using UnityEngine;
using System.Collections;
using GILES;
using UnityEngine.SceneManagement;

namespace GILES.Example
{
	/**
	 * Simple example of a scene loading script.
	 */
	public class pb_SceneLoader : pb_MonoBehaviourSingleton<pb_SceneLoader>
	{
		/// Make this object persistent between scene loads.
		public override bool dontDestroyOnLoad { get { return true; } }

		/// The scene that will be opened and loaded into.
		public string sceneToLoadLevelInto = "Empty Scene";

		[HideInInspector] [SerializeField] private string json = null;

        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (SceneManager.GetActiveScene().name == sceneToLoadLevelInto && !string.IsNullOrEmpty(json))
                pb_Scene.LoadLevel(json);
            else
                Debug.Log("json is null or empty: " + string.IsNullOrEmpty(json));
        }

        /**
		 * Call this to load level.
		 */
        public static void LoadScene(string path)
		{
	 		string san = pb_FileUtility.SanitizePath(path, ".json");

			if(!pb_FileUtility.IsValidPath(san, ".json"))
			{
				Debug.LogWarning(san + " not found, or file is not a JSON scene.");
				return;
			}
			else
			{
				instance.json = pb_FileUtility.ReadFile(san);
			}

			SceneManager.LoadScene(instance.sceneToLoadLevelInto);
		}

	}
}
