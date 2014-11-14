/********************************************************************************
Copyright (C) Binod Nirvan, Mix Open Foundation (http://mixof.org).

This file is part of ASPDoc.Net.

ASPDoc.Net is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

ASPDoc.Net is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with ASPDoc.Net.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Web.Hosting;

namespace ASPDoc.Net.Configuration
{
    public static class AppConfiguration
    {
        public static string GetDefaultPath()
        {
            string path = GetConfiguration("DefaultPath");
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "~/";
            }

            return path;
        }

        public static string EnableVersionControl()
        {
            return GetConfiguration("EnableVersionControl");
        }

        public static string GetDefaultDirectory()
        {
            string directory = GetConfiguration("DefaultDirectory");
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = "~/";
            }

            return directory;
        }

        public static string GetMediaDirectory()
        {
            string directory = GetConfiguration("MediaDirectory");
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = "~/";
            }

            return directory;
        }

        public static string GetDisqusName()
        {
            return GetConfiguration("DisqusName");
        }

        public static string DisplayDisqusDuringEdit()
        {
            return AppConfiguration.GetConfiguration("DisplayDiscusDuringEdit");
        }

        public static string GetConfiguration(string key)
        {
            string value = string.Empty;
            string configFile = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ASPDocConfigFileLocation"]);

            if (MixERP.Net.Common.Helpers.ConfigurationHelper.GetConfigurationValues(configFile, key) == null)
            {
                return value;
            }

            return MixERP.Net.Common.Helpers.ConfigurationHelper.GetConfigurationValues(configFile, key);
        }

        public static string GetAllowedExtensions()
        {
            string allowedExtensions = GetConfiguration("AllowedExtensions");

            if (string.IsNullOrWhiteSpace(allowedExtensions))
            {
                allowedExtensions = "*.jpg,*.png,*.gif";
            }

            return allowedExtensions;
        }

        public static string GetSiteName()
        {
            return GetConfiguration("SiteName");
        }

        public static void Save(Models.ConfigurationModel model)
        {
            if (model == null)
            {
                return;
            }

            string configFile = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ASPDocConfigFileLocation"]);

            Collection<KeyValuePair<string, string>> sections = new Collection<KeyValuePair<string, string>>();
            sections.Add(new KeyValuePair<string, string>("AllowedExtensions", model.AllowedExtensions));
            sections.Add(new KeyValuePair<string, string>("DefaultDirectory", model.DefaultDirectory));
            sections.Add(new KeyValuePair<string, string>("DefaultPath", model.DefaultPath));
            sections.Add(new KeyValuePair<string, string>("DisplayDiscusDuringEdit", model.DisplayDiscusDuringEdit));
            sections.Add(new KeyValuePair<string, string>("DisqusName", model.DisqusName));
            sections.Add(new KeyValuePair<string, string>("EnableVersionControl", model.EnableVersionControl));
            sections.Add(new KeyValuePair<string, string>("MediaDirectory", model.MediaDirectory));
            sections.Add(new KeyValuePair<string, string>("SiteName", model.SiteName));

            SetConfigurationValues(configFile, sections);
        }

        public static string SetConfigurationValues(string configFileName, string sectionName, string value)
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            AppSettingsSection section = config.GetSection("appSettings") as AppSettingsSection;

            if (section != null)
            {
                if (section.Settings[sectionName] != null)
                {
                    section.Settings[sectionName].Value = value;
                }
            }
            config.Save(ConfigurationSaveMode.Modified);

            return string.Empty;
        }

        public static void SetConfigurationValues(string configFileName, Collection<KeyValuePair<string, string>> sections)
        {
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFileName };
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            AppSettingsSection appsetting = config.GetSection("appSettings") as AppSettingsSection;

            if (appsetting == null)
            {
                return;
            }

            if (sections != null && sections.Count > 0)
            {
                foreach (var section in sections)
                {
                    if (section.Key != null)
                    {
                        if (appsetting.Settings[section.Key] != null)
                        {
                            appsetting.Settings[section.Key].Value = section.Value;
                        }
                    }
                }
            }

            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}