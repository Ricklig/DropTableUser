// ===============================
// AUTHOR     : Guillaume Vachon Bureau
// CREATE DATE     : 2018-08-29
//==================================
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class BlackBoard
    {
        private Dictionary<string, object> entries = new Dictionary<string, object>();


        public object GetEntry(string entryName)
        {
            return entries[entryName];
        }
        public void AddEntry(string entryName, object data)
        {
            entries[entryName] = data;
        }
        public void RemoveEntry(string entryName)
        {
            entries.Remove(entryName);
        }

        public bool GetValueAsBool(string entryName)
        {
            return (bool)entries[entryName];
        }
        public void SetValueAsBool(string entryName, bool value)
        {
            entries[entryName] = value;
        }

        public int GetValueAsInt(string entryName)
        {
            return (int)entries[entryName];
        }
        public void SetValueAsInt(string entryName, int value)
        {
            entries[entryName] = value;
        }

        public void SetValueAsFloat(string entryName, float value)
        {
            entries[entryName] = value;
        }
        public float GetValueAsFloat(string entryName)
        {
            return (float)entries[entryName];
        }

        public void SetValueAsVector3(string entryName, Vector3 value)
        {
            entries[entryName] = value;
        }
        public Vector3 GetValueAsVector3(string entryName)
        {
            return (Vector3)entries[entryName];
        }

        public void SetValueAsString(string entryName, String value)
        {
            entries[entryName] = value;
        }
        public String GetValueAsString(string entryName)
        {
            return (String)entries[entryName];
        }

        public void SetValueAsTransform(string entryName, Transform value)
        {
            entries[entryName] = value;
        }
        public Transform GetValueAsTransform(string entryName)
        {
            return (Transform)entries[entryName];
        }
    }
}

    