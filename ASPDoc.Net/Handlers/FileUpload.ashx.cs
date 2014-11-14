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
using ASPDoc.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;

namespace ASPDoc.Net.Handlers
{
    public class FileUpload : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string uploadDirectory = context.Request.QueryString["Path"];
            string mediaDirectory = AppConfiguration.GetMediaDirectory();

            this.ConfirmSignIn(context);

            if (!Common.Helpers.DirectoryHelper.IsChildOrSelf(HostingEnvironment.MapPath(mediaDirectory), HostingEnvironment.MapPath(uploadDirectory)))
            {
                throw new UnauthorizedAccessException("Access is denied");
            }

            Collection<string> uploadedFiles = new Collection<string>();

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string extension = Path.GetExtension(file.FileName);

                    if (this.GetAllowedExtensions().Contains(extension))
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string savePath = context.Server.MapPath(Path.Combine(uploadDirectory, fileName));

                        file.SaveAs(savePath);
                        uploadedFiles.Add(fileName);
                    }
                }
            }

            //System.Threading.Thread.Sleep(5000);

            context.Response.ContentType = "text/plain";
            context.Response.Write(string.Join(",", uploadedFiles));
        }

        private void ConfirmSignIn(HttpContext context)
        {
            if (context.Session["Username"] == null)
            {
                throw new UnauthorizedAccessException("Access is denied.");
            }
        }

        private List<string> GetAllowedExtensions()
        {
            return AppConfiguration.GetAllowedExtensions().Split(',').ToList();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}