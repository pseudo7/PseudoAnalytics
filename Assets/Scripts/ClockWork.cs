using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PseudoAnalytics
{
    public class ClockWork : MonoBehaviour
    {
        static ClockWork Instance;

        static Dictionary<string, int> loadedMap;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                loadedMap = PSAMap;
                StartCoroutine(ClockWorkAutoSave());
            }
            else
                Destroy(gameObject);
        }

        public static string GetPSATime
        {
            get
            {
                return System.DateTime.Now.ToString("HH:mm:ss:f");
            }
        }

        public static string AppendMap(string moduleName)
        {
            if (!loadedMap.ContainsKey(moduleName))
                loadedMap.Add(moduleName, 0);
            return string.Format("{0}", ++loadedMap[moduleName]);
        }

        Dictionary<string, int> PSAMap
        {
            get
            {
                if (!PlayerPrefs.HasKey(PSAConstants.PSA_MAP_KEY))
                    return new Dictionary<string, int> { };
                return JsonUtility.FromJson<Dictionary<string, int>>(PlayerPrefs.GetString(PSAConstants.PSA_MAP_KEY));
            }
            set
            {
                if (!PlayerPrefs.HasKey(PSAConstants.PSA_MAP_KEY))
                    PlayerPrefs.SetString(PSAConstants.PSA_MAP_KEY, JsonUtility.ToJson(new Dictionary<string, int> { }));
                PlayerPrefs.SetString(PSAConstants.PSA_MAP_KEY, JsonUtility.ToJson(value));
            }
        }

        IEnumerator ClockWorkAutoSave()
        {
            while (gameObject.activeInHierarchy)
            {
                yield return new WaitForSecondsRealtime(PSAConstants.AUTO_SAVE_TIME);
                PSAMap = loadedMap;
                PlayerPrefs.Save();
            }
        }

    }
}