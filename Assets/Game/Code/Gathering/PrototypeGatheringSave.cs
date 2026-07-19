using System.Collections.Generic;
using System.IO;
using Wayroot.Inventory;
using UnityEngine;

namespace Wayroot.Gathering
{
    [System.Serializable]
    public sealed class PrototypeGatheringSave
    {
        public int version = 1;
        public int petals;
        public int timber;
        public int stone;
        public List<string> depletedNodeIds = new();
    }

    public static class PrototypeGatheringSaveService
    {
        private const string FileName = "wayroot-phase2-gathering.json";
        private static string PathName => Path.Combine(Application.persistentDataPath, FileName);
        public static PrototypeGatheringSave Load()
        {
            try { return File.Exists(PathName) ? JsonUtility.FromJson<PrototypeGatheringSave>(File.ReadAllText(PathName)) ?? new PrototypeGatheringSave() : new PrototypeGatheringSave(); }
            catch { return new PrototypeGatheringSave(); }
        }
        public static void Save(PrototypeGatheringSave value)
        {
            string temporary = PathName + ".tmp";
            File.WriteAllText(temporary, JsonUtility.ToJson(value));
            File.Copy(temporary, PathName, true);
            File.Delete(temporary);
        }
    }
}
