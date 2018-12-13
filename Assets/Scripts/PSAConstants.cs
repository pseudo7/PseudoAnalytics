using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PseudoAnalytics
{
    public static class PSAConstants
    {
        public static string CSV_EXPORT_PATH = System.IO.Path.Combine(Application.persistentDataPath, "Analytics_CSV");
        internal static float AUTO_SAVE_TIME = 30f;
        public const string MAIN_SCENE_MODULE = "MAIN_SCENE_MODULE";
        public const string SHOPPING_SCENE_MODULE = "SHOPPING_SCENE_MODULE";
        public const string SHOWCASE_SCENE_MODULE = "SHOWCASE_SCENE_MODULE";
        public const string PSA_MAP_KEY = "PSA_MAP_KEY";
    }
}