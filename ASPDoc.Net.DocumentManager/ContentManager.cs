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

using ASPDoc.Net.DocumentManager.Models;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace ASPDoc.Net.DocumentManager
{
    public class ContentManager
    {
        public static FileModel GetFile(string url, string revision)
        {
            if (url == null)
            {
                return new FileModel();
            }

            string documentPath = HttpContext.Current.Server.MapPath(url);
            string markdownPath = HttpContext.Current.Server.MapPath(url) + ".md.html";

            if (!string.IsNullOrWhiteSpace(revision))
            {
                string fileName = FileHelper.GetFileName(url, true);
                documentPath = HostingEnvironment.MapPath(url.Replace(fileName, "") + revision);
                markdownPath = string.Empty;
            }

            if (File.Exists(HttpContext.Current.Server.MapPath(url)))
            {
                return new FileModel
                {
                    DocumentPath = url,
                    Markdown = DocumentHelper.ReadFile(documentPath),
                    Document = DocumentHelper.ReadFile(markdownPath)
                };
            }

            return new FileModel
            {
                DocumentPath = url,
                Markdown = string.Empty,
                Document = string.Empty
            };
        }
    }
}
