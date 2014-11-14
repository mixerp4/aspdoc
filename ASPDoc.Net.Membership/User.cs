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
using ASPDoc.Net.Membership.Models;
using System;
using System.Web;
using System.Web.Security;

namespace ASPDoc.Net.Membership
{
    public class User
    {
        public static void SignOut(HttpResponseBase response)
        {
            FormsAuthentication.SignOut();

            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, null, DateTime.Now, DateTime.Now.AddMinutes(-30), false, string.Empty, FormsAuthentication.FormsCookiePath);
            response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
        }

        public static bool SignIn(SignInModel model)
        {
            // ReSharper disable once CSharpWarnings::CS0618
            bool isValid = FormsAuthentication.Authenticate(model.Username, model.Password);

            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                return true;
            }

            return false;
        }
    }
}
