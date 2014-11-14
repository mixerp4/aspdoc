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
namespace ASPDoc.Net.DocumentManager
{
    public static class DocumentHelper
    {
        public static void CreateFile(string fileName, string content, string extension)
        {
            if (!string.IsNullOrWhiteSpace(extension))
            {
                if (!extension.StartsWith("."))
                {
                    extension += ".";
                }

                fileName += extension;
            }

            System.IO.File.WriteAllText(fileName, content);
        }

        public static string ReadFile(string fileName)
        {
            string contents = string.Empty;

            if (System.IO.File.Exists(fileName))
            {
                contents = System.IO.File.ReadAllText(fileName);
            }

            return contents;
        }
    }
}
