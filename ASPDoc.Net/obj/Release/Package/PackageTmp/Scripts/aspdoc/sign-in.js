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

function ShowSignInMessage(html) {
    var message = $("#Message");
    message.html(html);
    message.removeClass("hidden");
};

function HideSignInMessage() {
    var message = $("#Message");
    message.html("");
    message.addClass("hidden");
};

function SignIn(element) {
    HideSignInMessage();
    var form = $(element).closest(".form");
    var username = $("#Username");
    var password = $("#Password");
    var rememberMe = $("#RememberMe");
    var valid = true;

    username.closest(".field").removeClass("error");
    password.closest(".field").removeClass("error");

    if (IsNullOrWhiteSpace(username.val())) {
        username.closest(".field").addClass("error");
        valid = false;
    };

    if (IsNullOrWhiteSpace(password.val())) {
        password.closest(".field").addClass("error");
        valid = false;
    };

    if (!valid) {
        ShowSignInMessage("Access is denied.");
        return false;
    };

    form.addClass("loading");

    var signInModel = new Object();

    signInModel.UserName = username.val();
    signInModel.password = password.val();
    signInModel.RememberMe = rememberMe.is(":checked");

    var ajaxSignIn = AjaxSignIn(signInModel);

    ajaxSignIn.success(function (msg) {
        if (msg.d === "OK") {
            RefreshPage();
            return;
        };

        username.closest(".field").addClass("error");
        password.closest(".field").addClass("error");
        ShowSignInMessage("Access is denied.");
        form.removeClass("loading");
    });

    ajaxSignIn.fail(function (xhr) {
        form.removeClass("loading");
        ShowSignInMessage(xhr.responseText);
    });

    return false;
};

function AjaxSignIn(model) {
    var url = ResolveUrl("~/home/signin");

    var data = "";
    data = AppendParameter(data, "model", model);
    data = GetData(data);

    return GetAjax(url, data);
};