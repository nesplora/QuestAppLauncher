﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuestAppLauncher
{
    /// <summary>
    /// Root config object
    /// </summary>
    [Serializable]
    public class Config
    {
        // Supported category types
        public const string Category_None = "none";
        public const string Category_Auto = "auto";
        public const string Category_Custom = "custom";

        /// <summary>
        /// Grid size
        /// </summary>
        [Serializable]
        public class GridSize
        {
            public int rows = 3;
            public int cols = 3;
        }

        // Grid size, specified as cols x rows
        public GridSize gridSize = new GridSize();

        // Whether to show 2D apps
        public bool show2D = false;

        // Whether to only show apps that are explicitly specified in appnames.txt file.
        // If true, this will not show installed apps that are not in appnames.txt. This
        // is useful for organizing the launcher with a highly curated list of apps.
        public bool showOnlyCustom = false;

        // Category types: "none", "auto", "custom":
        //  - none: No categories - all apps are listed in a single pane
        //  - auto: Apps are automatically categorized into 3 tabs - Quest, Go/GearVr, 2D
        //  - custom: Apps are categorized according to appnames.txt file
        public string categoryType = Category_Auto;
    }

    /// <summary>
    /// Class responsible for loading / saving config into a config.json file.
    /// </summary>
    public class ConfigPersistence
    {
        // File name of app name overrides
        const string ConfigFileName = "config.json";

        /// <summary>
        /// Load config from file
        /// </summary>
        /// <param name="config">Config object that will be overwritten</param>
        static public void LoadConfig(Config config)
        {
            var configFilePath = Path.Combine(UnityEngine.Application.persistentDataPath, ConfigFileName);
            if (File.Exists(configFilePath))
            {
                Debug.Log("Found config file: " + configFilePath);
                var jsonConfig = File.ReadAllText(configFilePath);

                try
                {
                    JsonUtility.FromJsonOverwrite(jsonConfig, config);
                }
                catch (Exception e)
                {
                    Debug.Log(string.Format("Failed to read config: {0}", e.Message));
                }
            }
            else
            {
                Debug.Log("Did not find config file: " + configFilePath);
            }
        }

        /// <summary>
        /// Save config to a file
        /// </summary>
        /// <param name="config"></param>
        static public void SaveConfig(Config config)
        {
            var configFilePath = Path.Combine(UnityEngine.Application.persistentDataPath, ConfigFileName);
            Debug.Log("Saving config file: " + configFilePath);

            try
            {
                File.WriteAllText(configFilePath, JsonUtility.ToJson(config, true));
            }
            catch (Exception e)
            {
                Debug.Log(string.Format("Failed to read config: {0}", e.Message));
            }

        }
    }
}