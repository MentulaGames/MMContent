namespace Mentula.Content.Config
{
    using Core;
    using Core.Collections;
    using Reading;
    using System.Collections.Generic;
    using TypeConverter = System.ComponentModel.TypeConverter;

    /// <summary>
    /// Represents a runtime loaded config file.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public sealed class Config
    {
        private const string VALUE = "Value";
        private const string CLIENT = "Client";

        private Container raw;
        private Dictionary<string, object> values;
        private Dictionary<string, string[]> catKeys;
        private List<KeyValuePair<string, string>> clientValues;
        private string path;

        internal Config(Container cnt, string path)
        {
            raw = cnt;
            values = new Dictionary<string, object>();
            catKeys = new Dictionary<string, string[]>();
            this.path = path;
        }

        /// <summary>
        /// Gets the visual version of a config property.
        /// </summary>
        /// <param name="name"> The name of the property. </param>
        /// <returns> A string representation of the properties value. </returns>
        public string GetVisual(string name)
        {
            string val, cat;
            string res = GetInternal(name, out val, out cat);
            return string.IsNullOrEmpty(res) ? val : res;
        }

        /// <summary>
        /// Get the specified property as a specified type.
        /// </summary>
        /// <remarks> A property can only be loaded as one type or as string via <see cref="GetVisual(string)"/>. </remarks>
        /// <typeparam name="T"> The specified type of the property. </typeparam>
        /// <param name="name"> The specified name of the property. </param>
        /// <returns> The value of the property as T. </returns>
        /// <exception cref="ContainerException"> The value could not be found or the current type is not equal to its previous type. </exception>
        /// <exception cref="System.NotSupportedException"> The value could not be converted 
        /// (add a <see cref="TypeConverter"/> to the <see cref="MentulaTypeDescriptor"/> if you wish to convert a custom value). </exception>
        public T Get<T>(string name)
        {
            string category;
            T foundVal;
            string val = GetInternal(name, out foundVal, out category);

            if (val == null) return foundVal;
            if (val == string.Empty) throw new ContainerException(path, new ParameterNullException(name));

            if (!catKeys.ContainsKey(category)) catKeys.Add(category, new string[0]);
            catKeys[category] = catKeys[category].Add(name);

            RecursiveRemove(raw, name);

            TypeConverter tc = MentulaTypeDescriptor.GetFromString<T>();
            object result = tc.ConvertFromString(null, Utils.usInfo, val);

            values.Add(name, result);
            return (T)result;
        }

        /// <summary>
        /// Adds specified values to the config file.
        /// </summary>
        /// <remarks>
        /// This method is used for config properties that are adden when the main config is already loaded.
        /// These could be config properties that are stored on a server.
        /// </remarks>
        /// <param name="serverValues"> The specified properties. </param>
        public void Add(KeyValuePair<string, string>[] serverValues)
        {
            for (int i = 0; i < serverValues.Length; i++)
            {
                KeyValuePair<string, string> cur = serverValues[i];
                if (raw.Values.ContainsKey(cur.Key)) raw.Values.Remove(cur.Key);
                raw.Values.Add(cur.Key, cur.Value);
            }
        }

        /// <summary>
        /// Gets the properties specified as client required.
        /// </summary>
        /// <remarks>
        /// Client marked properties are properties that should be send to the client from the server.
        /// </remarks>
        /// <returns> The properties that should be send to the client. </returns>
        public List<KeyValuePair<string, string>> GetClient()
        {
            if (clientValues == null) clientValues = GetClientValues(raw);
            return clientValues;
        }

        /// <summary>
        /// Gets the underlying config categories.
        /// </summary>
        /// <returns> The categories specified in the config file. </returns>
        public string[] GetCategories()
        {
            string[] result = new string[raw.Childs.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = raw.Childs[i].Name;
            }

            return result;
        }

        /// <summary>
        /// Gets all the property keys from a specified category.
        /// </summary>
        /// <param name="category"> The specified category. </param>
        /// <returns> The property keys from the spevified category. </returns>
        public string[] GetKeysFromCategory(string category)
        {
            string[] result = null;

            for (int i = 0; i < raw.Childs.Length; i++)
            {
                Container cur = raw.Childs[i];
                if (cur.Name == category)
                {
                    result = new string[cur.Values.Count];
                    cur.Values.Keys.CopyTo(result, 0);

                    if (catKeys.ContainsKey(category)) result = result.Concat(catKeys[category]);
                }
            }

            return result;
        }

        private string GetInternal<T>(string name, out T cnvVal, out string category)
        {
            if (values.ContainsKey(name))
            {
                if (typeof(T) == typeof(string) && values[name].GetType() != typeof(string)) cnvVal = (T)((object)values[name].ToString());
                else cnvVal = (T)values[name];

                category = null;
                return null;
            }

            cnvVal = default(T);
            return RecursiveFind(raw, name, out category);
        }

        private List<KeyValuePair<string, string>> GetClientValues(Container cnt)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            if (cnt.Values.ContainsKey(CLIENT))
            {
                if (cnt.GetBoolValue(CLIENT)) result.Add(new KeyValuePair<string, string>(cnt.Name, cnt.GetStringValue(VALUE)));
            }
            for (int i = 0; i < cnt.Childs.Length; i++)
            {
                result.AddRange(GetClientValues(cnt.Childs[i]));
            }

            return result;
        }

        private string RecursiveFind(Container cnt, string name, out string category)
        {
            string result;
            if (cnt.TryGetValue(name, out result))
            {
                category = cnt.Name;
                return result;
            }
            else
            {
                for (int i = 0; i < cnt.Childs.Length; i++)
                {
                    if (cnt.Childs[i].Name == name)
                    {
                        category = cnt.Childs[i].Name;
                        return cnt.Childs[i].GetStringValue(VALUE);
                    }
                    result = RecursiveFind(cnt.Childs[i], name, out category);
                    if (!string.IsNullOrEmpty(result)) return result;
                }
            }

            category = null;
            return string.Empty;
        }

        private bool RecursiveRemove(Container cnt, string name)
        {
            if (cnt.Values.ContainsKey(name))
            {
                cnt.Values.Remove(name);
                return true;
            }
            else
            {
                for (int i = 0; i < cnt.Childs.Length; i++)
                {
                    if (RecursiveRemove(cnt.Childs[i], name)) return true;
                }
            }

            return false;
        }
    }
}
