using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DriversBackup.Models
{
    public class IniFiles
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",
            SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]

        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            string lpReturnString,
            int nSize,
            string lpFilename);

        /// <summary>
        /// Get information (dictionary) keys from inf files 
        /// </summary>
        /// <param name="iniFile">Path to driver ini file</param>
        /// <param name="category">Driver category</param>
        /// <returns>List of keys</returns>
        public List<string> GetKeys(string iniFile, string category)
        {
            var returnString = new string(' ', 32768);
            GetPrivateProfileString(category, null, null, returnString, 32768, iniFile);

            //return data followed by null terminator                                                                                                                                                                                            ŁŁŁŁŁŁŁŁŁ<
            var result = new List<string>(returnString.Split('\0'));
            result.RemoveRange(result.Count - 2, 2);
            return result;
        }

        /// <summary>
        /// Get value from inf file by given Key
        /// </summary>
        /// <param name="iniFile">Path to inf file</param>
        /// <param name="category">Category</param>
        /// <param name="key">Dictionary key - can be obtaine by GetKeys method</param>
        /// <param name="defaultValue">Default value (for error handling)</param>
        /// <returns>Inf file key value</returns>
        public string GetIniFileString(string iniFile, string category, string key, string defaultValue)
        {
            var returnString = new string(' ', 1024);
            GetPrivateProfileString(category, key, defaultValue, returnString, 1024, iniFile);
            //return data followed by null terminator
            return returnString.Split('\0')[0];
        }
    }
}
