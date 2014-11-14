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
using ASPDoc.Net.Configuration.Models;
using ASPDoc.Net.DocumentManager;
using ASPDoc.Net.DocumentManager.Models;
using ASPDoc.Net.Helpers;
using ASPDoc.Net.Membership.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ASPDoc.Net.Controllers
{
    public class HomeController : Controller
    {
        #region Home

        public ActionResult Index()
        {
            ViewBag.IsAuthenticated = this.IsAuthenticated();
            ViewBag.DisqusName = AppConfiguration.GetDisqusName();
            ViewBag.DisplayDiscusDuringEdit = AppConfiguration.DisplayDisqusDuringEdit();
            ViewBag.MediaDirectory = AppConfiguration.GetMediaDirectory();
            ViewBag.AllowedExtensions = AppConfiguration.GetAllowedExtensions();
            ViewBag.EnableVersionControl = AppConfiguration.EnableVersionControl();

            ViewBag.Title = AppConfiguration.GetSiteName();
            string revision = Request.QueryString["version"];

            object url = RouteData.Values["url"];

            if (url == null || string.IsNullOrWhiteSpace(url.ToString()))
            {
                Response.Redirect(AppConfiguration.GetDefaultPath());
            }

            if (url != null)
            {
                url = Path.Combine(AppConfiguration.GetDefaultDirectory(), url.ToString());

                FileModel model = ContentManager.GetFile(url.ToString(), revision);
                ViewBag.Title = this.GetTitle(model.Document);

                return View(model);
            }

            return View(new FileModel());
        }

        private string GetTitle(string html)
        {
            //Cannot use HtmlAgilityPack due to its license.
            const string headingPattern = "<h1[^>]*?>(?<TagText>.*?)</h1>";
            MatchCollection headings = Regex.Matches(html, headingPattern, RegexOptions.Multiline);

            if (headings.Count > 0)
            {
                string heading = headings[0].Value;

                const string tagPattern = "<h1[^>]*?>";
                MatchCollection tags = Regex.Matches(html, tagPattern, RegexOptions.Multiline);

                if (tags.Count > 0)
                {
                    string headingTag = tags[0].Value;
                    return heading.Replace(headingTag, "").Replace("</h1>", "");
                }
            }

            return string.Empty;
        }

        [HttpPost]
        public ActionResult Index(FileModel model)
        {
            if (model == null)
            {
                return Index();
            }

            if (string.IsNullOrWhiteSpace(model.DocumentPath))
            {
                return Index();
            }

            if (string.IsNullOrWhiteSpace(model.Markdown))
            {
                return Index();
            }

            if (string.IsNullOrWhiteSpace(model.Document))
            {
                return Index();
            }

            this.SaveDocumet(model);

            return Index();
        }

        private void SaveDocumet(FileModel model)
        {
            string path = Server.MapPath(model.DocumentPath);
            FileHelper.EnsureDirectoryExists(path);

            string fileName = FileHelper.GetFileName(path, false);
            string directory = FileHelper.GetDirectory(path, false);

            Collection<string> fileVersions = VersionControl.GetFileVersions(fileName, directory);

            if (AppConfiguration.EnableVersionControl().ToLower() == "yes")
            {
                if (fileVersions.Count > 0)
                {
                    int currentVersion = VersionControl.GetLastVersion(fileVersions, fileName) + 1;
                    DocumentHelper.CreateFile(path + "." + currentVersion.ToString(CultureInfo.InvariantCulture), model.Markdown, string.Empty);
                }
                else
                {
                    //First Version
                    DocumentHelper.CreateFile(path + ".0", model.Markdown, string.Empty);
                }
            }

            DocumentHelper.CreateFile(path, model.Markdown, string.Empty);
            DocumentHelper.CreateFile(path, model.Document, ".md.html");
        }

        public static Collection<string> GetFileVersions(FileModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.DocumentPath))
            {
                return new Collection<string>();
            }

            string path = HostingEnvironment.MapPath(model.DocumentPath);
            FileHelper.EnsureDirectoryExists(path);

            string fileName = FileHelper.GetFileName(path, false);
            string directory = FileHelper.GetDirectory(path, false);

            Collection<string> fileVersions = VersionControl.GetFileVersions(fileName, directory);

            return fileVersions;
        }

        #endregion Home

        #region Membership

        private bool IsAuthenticated()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;

            if (isAuthenticated)
            {
                if (Session["Username"] == null)
                {
                    Session["Username"] = User.Identity.Name;
                }
            }

            return isAuthenticated;
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            Membership.User.SignOut(this.Response);
            return Content(JsonHelper.ToJson("OK"), "application/json");
        }

        public ActionResult SignIn()
        {
            return Index();
        }

        [HttpPost]
        public ActionResult SignIn(SignInModel model)
        {
            if (model == null)
            {
                return Content(JsonHelper.ToJson("Access is denied"), "application/json");
            }

            bool isValid = Membership.User.SignIn(model);

            if (isValid)
            {
                Session["Username"] = model.Username;
                return Content(JsonHelper.ToJson("OK"), "application/json");
            }

            return Content(JsonHelper.ToJson("Access is denied"), "application/json");
        }

        #endregion Membership

        #region Configuration
        [HttpPost]
        public ActionResult SaveConfiguration(ConfigurationModel model)
        {
            if (IsAuthenticated())
            {
                AppConfiguration.Save(model);
            }

            return Content(JsonHelper.ToJson("OK"), "application/json");
        }
        #endregion Configuration
    }
}