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

function LoadBreadCrumb(directory) {
    if (directory.substring(0, mediaDirectory.length) === mediaDirectory) {
        directory = directory.replace(mediaDirectory, "");
    };

    var directories = directory.split("/");

    var breadcrumb = $("#MediaManagerBreadCrumb");
    breadcrumb.html("Path : ");

    breadcrumb.append("<a href='javascript:ReloadMedia();' class='section'>Media Directory Root</a> <div class='divider'> / </div>");

    var path = "";

    $.each(directories, function () {
        if (!IsNullOrWhiteSpace(this)) {
            path = Path.combine(path, this);
            var html = "<a href='javascript:InitializeMedia(\"" + encodeURI(path.replace("'", "&apos;")) + "\", mediaDirectory);' class='section'>" + this + "</a> <div class='divider'> / </div>";
            breadcrumb.append(html);
        };
    });
};
