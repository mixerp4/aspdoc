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

using ASPDoc.Net.Common.Helpers;
using ASPDoc.Net.Configuration;
using ASPDoc.Net.DocumentManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;

namespace ASPDoc.Net.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class MediaService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public MediaModel GetMedia(string currentDirectory)
        {
            this.ConfirmSignIn(); ;

            if (string.IsNullOrWhiteSpace(currentDirectory))
            {
                currentDirectory = this.GetMediaDirectory();
            }

            if (!Directory.Exists(Server.MapPath(currentDirectory)))
            {
                throw new DirectoryNotFoundException("The directory " + currentDirectory + " could not be located.");
            }

            return this.GetModel(currentDirectory);
        }

        [WebMethod(EnableSession = true)]
        public void DeleteFiles(Collection<string> files)
        {
            this.ConfirmSignIn(); ;

            foreach (var file in files)
            {
                if (!File.Exists(Server.MapPath(file)))
                {
                    throw new FileNotFoundException("The file " + file + " could not be located.");
                }

                if (!DirectoryHelper.IsChildOrSelf(Server.MapPath(this.GetMediaDirectory()), new FileInfo(Server.MapPath(file)).DirectoryName))
                {
                    throw new UnauthorizedAccessException("Access is denied.");
                }
            }

            foreach (var file in files)
            {
                File.Delete(Server.MapPath(file));
            }
        }

        [WebMethod(EnableSession = true)]
        public void CreateDirectory(string directoryName, string currentDirectory)
        {
            if (!Directory.Exists(Server.MapPath(currentDirectory)))
            {
                throw new UnauthorizedAccessException("Access is denied");
            }

            if (!DirectoryHelper.IsChildOrSelf(Server.MapPath(this.GetMediaDirectory()), Server.MapPath(currentDirectory)))
            {
                throw new UnauthorizedAccessException("Access is denied");
            }

            Directory.CreateDirectory(Server.MapPath(Path.Combine(currentDirectory, directoryName)));
        }

        private void ConfirmSignIn()
        {
            if (Session["Username"] == null)
            {
                throw new UnauthorizedAccessException("Access is denied.");
            }
        }

        private string GetMediaDirectory()
        {
            string mediaDirectory = AppConfiguration.GetMediaDirectory();

            if (mediaDirectory == null)
            {
                throw new ConfigurationErrorsException("The AppSetting key \"MediaDirectory\" is not present in the configuration file.");
            }

            return mediaDirectory;
        }

        private MediaModel GetModel(string currentDirectory)
        {
            Collection<MediaModel.File> files = new Collection<MediaModel.File>();

            DirectoryInfo info = new DirectoryInfo(Server.MapPath(currentDirectory));

            var directories = info.GetDirectories();

            foreach (DirectoryInfo directory in directories)
            {
                MediaModel.File file = new MediaModel.File();

                file.FileName = Path.GetFileName(directory.FullName);
                file.RelativePath = this.ToRelative(directory.FullName);
                file.IsDirectory = true;

                files.Add(file);
            }

            var contents = this.GetFilesByExtensions(info, AppConfiguration.GetAllowedExtensions().Split(','));

            foreach (FileInfo content in contents)
            {
                MediaModel.File file = new MediaModel.File();

                file.FileName = Path.GetFileName(content.FullName);
                file.RelativePath = this.ToRelative(content.FullName);
                file.IsDirectory = false;

                files.Add(file);
            }

            MediaModel model = new MediaModel(files);
            model.CurrentDirectory = currentDirectory;

            return model;
        }

        //SLaks answer on stackoverflow
        //
        public IEnumerable<FileInfo> GetFilesByExtensions(DirectoryInfo dirInfo, string[] extensions)
        {
            var allowedExtensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase);

            return dirInfo.EnumerateFiles().Where(f => allowedExtensions.Contains(f.Extension));
        }

        private string ToRelative(string filePath)
        {
            string rootPath = Server.MapPath("~");
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(rootPath);

            return "/" + referenceUri.MakeRelativeUri(fileUri).ToString();
        }
    }
}
