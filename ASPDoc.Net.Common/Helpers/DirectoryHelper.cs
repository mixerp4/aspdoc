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
using System;
using System.IO;
using System.Web.Hosting;

namespace ASPDoc.Net.Common.Helpers
{
    public static class DirectoryHelper
    {
        private static bool IsSelf(string directory, string subDirectory)
        {
            Uri directoryUri = new Uri(Path.Combine(directory, "fakepath.html"), UriKind.RelativeOrAbsolute);
            Uri subDirectoryUri = new Uri(Path.Combine(subDirectory, "fakepath.html"), UriKind.RelativeOrAbsolute);

            return directoryUri.Equals(subDirectoryUri);
        }

        public static bool IsChildOrSelf(string mediaDirectory, string subDirectory)
        {
            bool isSelf = IsSelf(mediaDirectory, subDirectory);

            if (isSelf)
            {
                return true;
            }

            return IsChildOf(mediaDirectory, subDirectory);
        }

        public static string GetParentDirectory(string directory)
        {
            if (directory != null)
            {
                var directoryInfo = new DirectoryInfo(HostingEnvironment.MapPath(directory)).Parent;
                if (directoryInfo != null)
                {
                    return directoryInfo.FullName;
                }
            }

            return string.Empty;
        }

        public static bool IsChildOf(string directory, string subDirectory)
        {
            var isChild = false;

            var directoryInfo = new DirectoryInfo(directory);
            var subDirInfo = new DirectoryInfo(subDirectory);

            while (subDirInfo.Parent != null)
            {
                if (subDirInfo.Parent.FullName == directoryInfo.FullName)
                {
                    isChild = true;
                    break;
                }

                subDirInfo = subDirInfo.Parent;
            }

            return isChild;
        }
    }
}
