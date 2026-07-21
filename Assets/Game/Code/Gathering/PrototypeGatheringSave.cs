using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Wayroot.Gathering
{
    [System.Serializable]
    public sealed class PrototypeGatheringSave
    {
        public int version = PrototypeGatheringSaveMigration.CurrentVersion;
        public int petals;
        public int timber;
        public int stone;
        public int slimeCores;
        public int weaponLevel;
        public bool shelterBuilt;
        // Missing in Phase 0-11 saves defaults false through JsonUtility for backward compatibility.
        public bool activeShelterReturnPoint;
        public bool creatureBefriended;
        // Missing in pre-Phase 16 saves is migrated to the cozy soundscape's enabled default.
        public bool soundEnabled = true;
        // Missing in Phase 0-9 saves defaults false through JsonUtility for backward compatibility.
        public bool wayrootRestored;
        // A first Thorn Guardian victory opens the compact Phase 17 exploration pocket.
        public bool moonlitGladeUnlocked;
        // Missing in Phase 0-18 saves defaults false, preserving the one-time Phase 19 finale.
        public bool bloomwellRestored;
        // Retained only to read Phase 0-10 JSON. Normalize migrates these IDs to renewalNodes.
        public List<string> depletedNodeIds = new();
        public List<RenewalNodeSave> renewalNodes = new();
    }

    [System.Serializable]
    public sealed class RenewalNodeSave
    {
        public string nodeId = string.Empty;
        public long renewalDeadlineUtcTicks;
    }

    public static class PrototypeGatheringSaveService
    {
        private const string FileName = "wayroot-phase2-gathering.json";
        private static string PathName => Path.Combine(Application.persistentDataPath, FileName);

        public static PrototypeGatheringSave Load()
        {
            try
            {
                bool exists = File.Exists(PathName);
                PrototypeGatheringSave save = exists ? JsonUtility.FromJson<PrototypeGatheringSave>(File.ReadAllText(PathName)) ?? new PrototypeGatheringSave() : new PrototypeGatheringSave();
                bool needsMigrationWrite = exists && (save.version < PrototypeGatheringSaveMigration.CurrentVersion || save.depletedNodeIds == null || save.depletedNodeIds.Count > 0 || save.renewalNodes == null);
                PrototypeGatheringSaveMigration.Normalize(save, System.DateTime.UtcNow);
                if (needsMigrationWrite) Save(save);
                return save;
            }
            catch { return new PrototypeGatheringSave(); }
        }

        public static void Reset()
        {
            if (File.Exists(PathName)) File.Delete(PathName);
        }

        public static void Save(PrototypeGatheringSave value)
        {
            PrototypeGatheringSaveMigration.Normalize(value, System.DateTime.UtcNow);
            string temporary = PathName + ".tmp";
            File.WriteAllText(temporary, JsonUtility.ToJson(value));
            File.Copy(temporary, PathName, true);
            File.Delete(temporary);
        }
    }
}
