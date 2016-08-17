﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace QuickStack.Util
{
    /// <summary>
    /// Provides utility functions for serializing to/ deserializing from JSON files. 
    /// </summary>
    public static class Persistence
    {
        /// <summary>
        /// Serialize a given object to a JSON file. 
        /// </summary>
        /// <typeparam name="T">Type of the object to be serialized. </typeparam>
        /// <param name="obj">Object to be serialized. </param>
        /// <param name="path">Path of the JSON file to save. </param>
        public static void SerializeToJson<T>(T obj, string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.CreateDirectory(Path.GetDirectoryName(path)); // Create 'data' directory if not exists. 
            using (var textWriter = File.CreateText(path))
            {
                // var settings = new JsonSerializerSettings();
                var jsonSerializer = JsonSerializer.CreateDefault(); // settings); 
                jsonSerializer.Serialize(textWriter, obj);
            }

        }
    }
}
