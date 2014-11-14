using MixERP.Net.Common;
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
using System.IO;
using System.Linq;

namespace ASPDoc.Net.DocumentManager
{
    public static class VersionControl
    {
        public static int GetLastVersion(IEnumerable<string> fileVersions, string fileName)
        {
            Collection<int> versions = new Collection<int>();
            foreach (var fileVersion in fileVersions)
            {
                versions.Add(Conversion.TryCastInteger(fileVersion.Replace(fileName + ".", "")));
            }

            return versions.Max();
        }

        public static Collection<string> GetFileVersions(string fileName, string directory)
        {
            Collection<string> fileCollection = new Collection<string>();

            if (!string.IsNullOrWhiteSpace(fileName) && !string.IsNullOrWhiteSpace(directory))
            {
                DirectoryInfo info = new DirectoryInfo(directory);
                var files = info.GetFiles(fileName + "*");

                foreach (var file in files)
                {
                    if (!file.Name.EndsWith(".md.html"))
                    {
                        fileCollection.Add(file.Name);
                    }
                }
            }

            return fileCollection;
        }
    }
}
