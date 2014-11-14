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
using System.Configuration;

namespace ASPDoc.Net.Configuration
{
    public static class Location
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

        public static string GetConfiguration(string key)
        {
            string value = string.Empty;

            if (ConfigurationManager.AppSettings[key] == null)
            {
                return value;
            }

            return ConfigurationManager.AppSettings[key];
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
    }
}