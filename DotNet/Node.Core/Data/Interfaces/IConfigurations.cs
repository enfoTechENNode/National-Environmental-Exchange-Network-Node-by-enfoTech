using System;
using System.Data;
using System.Xml;

namespace Node.Core.Data.Interfaces
{
    /// <summary>
    /// Interface class for retrieving System Configuration for Node Application.
    /// </summary>
    public interface IConfigurations
    {
        /// <summary>
        /// Get the Current Status of the Node as Set Through the Configurations Settings Screen
        /// </summary>
        /// <returns>"Running" if Node is Running, "Stopped" Otherwise</returns>
        string GetNodeStatus();

        /// <summary>
        /// Get System Configuration File from Database
        /// </summary>
        /// <returns>System Configuration Xml Document</returns>
        XmlDocument GetSystemConfig();

        /// <summary>
        /// Update System Configuration File in Database
        /// </summary>
        /// <param name="xml">The new System Configuration file</param>
        /// <returns>true if successful, false otherwise</returns>
        bool UpdateSystemConfig(XmlDocument xml);

        /// <summary>
        /// Get Task Configuration File from Database
        /// </summary>
        /// <returns>Task Configuration Xml Document</returns>
        XmlDocument GetTaskConfig();

        /// <summary>
        /// Update Task Configuration File in Database
        /// </summary>
        /// <param name="xml">The new Task Configuration file</param>
        /// <returns>true if successful, false otherwise</returns>
        bool UpdateTaskConfig(XmlDocument xml);

        /// <summary>
        /// Get Service Registration File from Database
        /// </summary>
        /// <returns>Service Registration Xml Document</returns>
        XmlDocument GetServiceRegistration();

        /// <summary>
        /// Update Service Registration File in Database
        /// </summary>
        /// <param name="xml">The new Service Registration file</param>
        /// <returns>true if successful, false otherwise</returns>
        bool UpdateServiceRegistration(XmlDocument xml);

        /// <summary>
        /// Gets the list of config names by specified config type.
        /// </summary>
        /// <param name="type">The config type.</param>
        /// <returns>A DataTable contains the list of config name.</returns>
        DataTable GetConfigNames(string type);

        /// <summary>
        /// Gets the config file content by config name and config type.
        /// </summary>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config file type.</param>
        /// <returns>The  config content.</returns>
        string GetConfig(string filename, string type);

        /// <summary>
        /// Gets the config file id by config name and config type.
        /// </summary>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config file type.</param>
        /// <returns>The  config id.</returns>
        string GetConfigID(string filename, string type);

        /// <summary>
        /// Deletes a config by config id.
        /// </summary>
        /// <param name="id">The config id.</param>
        /// <returns>If successfully deleted, return true; otherwise, return false.</returns>
        bool DeleteConfig(string id);

        /// <summary>
        /// Saves the config file.
        /// </summary>
        /// <param name="id">The config id.</param>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config file type.</param>
        /// <param name="content">The config content.</param>
        /// <returns>If successfully saved, return true; otherwise, return false.</returns>
        bool SaveConfig(string id, string filename, string type, string content);

        /// <summary>
        /// Adds a new config to database.
        /// </summary>
        /// <param name="filename">The config file name.</param>
        /// <param name="type">The config type.</param>
        /// <param name="content"></param>
        /// <returns></returns>
        int AddConfig(string filename, string type, string content);

        /// <summary>
        /// Get Service Registration File from Database
        /// </summary>
        /// <returns>Service Registration Xml Document</returns>
        string GetENDSServiceRegistration();
        /// <summary>
        /// Get Service Registration File from Database
        /// </summary>
        /// <returns>Service Registration Xml Document</returns>
        bool UpdateENDSServiceRegistration(string xml);

        /// <summary>
        /// Get Service Registration File from Database
        /// </summary>
        /// <returns>Service Registration Xml Document</returns>
        string GetDEDL();
        /// <summary>
        /// Get Service Registration File from Database
        /// </summary>
        /// <returns>Service Registration Xml Document</returns>
        bool UpdateDEDL(string xml);

    }
}
