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
namespace ASPDoc.Net.Configuration.Models
{
    public class ConfigurationModel
    {
        public ConfigurationModel()
        {
            this.DefaultPath = AppConfiguration.GetDefaultPath();
            this.DefaultDirectory = AppConfiguration.GetDefaultDirectory();
            this.MediaDirectory = AppConfiguration.GetMediaDirectory();
            this.AllowedExtensions = AppConfiguration.GetAllowedExtensions();
            this.DisqusName = AppConfiguration.GetDisqusName();
            this.DisplayDiscusDuringEdit = AppConfiguration.DisplayDisqusDuringEdit();
            this.EnableVersionControl = AppConfiguration.EnableVersionControl();
            this.SiteName = AppConfiguration.GetSiteName();
        }

        public string DefaultPath { get; set; }
        public string DefaultDirectory { get; set; }
        public string MediaDirectory { get; set; }
        public string AllowedExtensions { get; set; }
        public string DisqusName { get; set; }
        public string DisplayDiscusDuringEdit { get; set; }
        public string EnableVersionControl { get; set; }
        public string SiteName { get; set; }
    }
}