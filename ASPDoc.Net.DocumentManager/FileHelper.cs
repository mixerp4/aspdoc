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
using System.IO;

namespace ASPDoc.Net.DocumentManager
{
    public class FileHelper
    {
        public static void EnsureDirectoryExists(string fileName)
        {
            string directory = GetDirectory(fileName, true);

            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static string GetDirectory(string fileName, bool skipVerification)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            if (skipVerification)
            {
                if (fileInfo.Directory != null)
                {
                    return fileInfo.Directory.FullName;
                }
            }

            if (fileInfo.Exists)
            {
                if (fileInfo.Directory != null)
                {
                    return fileInfo.Directory.FullName;
                }
            }

            return string.Empty;
        }

        public static string GetFileName(string fileName, bool skipVerification)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            if (skipVerification)
            {
                return fileInfo.Name;
            }

            if (fileInfo.Exists)
            {
                return fileInfo.Name;
            }

            return string.Empty;
        }
    }
}
