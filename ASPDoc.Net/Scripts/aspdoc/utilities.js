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

function RefreshPage(skipQueryString) {
    if (skipQueryString) {
        window.location = window.location.toString().replace(location.search, "");
        return;
    };

    window.location = window.location;
};

function ResolveUrl(path, rootPath) {
    if (!rootPath) {
        if (typeof window.virtualDirectory == "string") {
            rootPath = window.virtualDirectory;
        };
    };

    if (rootPath === "/" && path.beginsWith("~/")) {
        return path.replace("~", "");
    };

    return path.replace("~", rootPath);
};

function GetRelativePath(path, rootPath) {
    if (!rootPath) {
        if (typeof window.virtualDirectory == "string") {
            rootPath = window.virtualDirectory;
        };
    };

    return path.replace(rootPath, "~");
};

function GoHome() {
    if (window.virtualDirectory) {
        window.location = window.virtualDirectory;
    };
};

function GetConfirmation() {
    return confirm("Are you sure?");
};

var Path = {
    combine: function () {
        var url = "";

        for (var i = 0; i < arguments.length; i++) {
            if (!arguments[i].endsWith("/")) {
                arguments[i] = arguments[i] + "/";
            };
            url += arguments[i];
        };

        return url.replace("//", "/");
    }
};

if (typeof String.prototype.beginsWith !== 'function') {
    String.prototype.beginsWith = function (s) {
        return this.slice(0, s.length) == s;
    };
};

if (typeof String.prototype.endsWith !== 'function') {
    String.prototype.endsWith = function (s) {
        return this.slice(-s.length) == s;
    };
};

function beginsWith(str, s) {
    return $(str).slice(0, s.length) == s;
};

$(document).ready(function () {
    $(document.links).filter(function () {
        if (String(this).substring(0, 10).toLowerCase() !== "javascript") {
            return this.hostname != window.location.hostname;
        };
    }).attr('target', '_blank');
});