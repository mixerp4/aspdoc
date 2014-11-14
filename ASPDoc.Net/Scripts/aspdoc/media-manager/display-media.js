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

function InitializeMedia(directory, relativeParent) {
    if (relativeParent) {
        currentDirectory = relativeParent;
    };

    if (IsNullOrWhiteSpace(directory)) {
        currentDirectory = mediaDirectory;
    } else {
        currentDirectory = Path.combine(currentDirectory, directory);
    };

    AjaxLoad(currentDirectory);
};

function AjaxGetMedia(currentDirectory) {
    var url = "~/Services/MediaService.asmx/GetMedia";
    url = ResolveUrl(url);
    var data = AppendParameter("", "currentDirectory", currentDirectory);
    data = GetData(data);

    return GetAjax(url, data);
};

function AjaxLoad(directory) {
    var ajaxGetMedia = AjaxGetMedia(directory);

    ajaxGetMedia.success(function (msg) {
        LoadMedia(msg.d);
    });

    ajaxGetMedia.fail(function (xhr) {
        alert(xhr.responseText);
    });
};

function LoadMedia(model) {
    currentDirectory = model.CurrentDirectory;

    LoadBreadCrumb(currentDirectory);
    LoadImages(model.Files);

    $(".checkbox").checkbox();
};

function LoadImages(files) {
    var mediaManagerFiles = $("#MediaManagerFiles");
    mediaManagerFiles.addClass("loading");

    mediaManagerFiles.html("");

    var connectedItems = "";

    var mod;

    for (var i = 0; i < files.length; i++) {
        mod = (i + 1) % 3;

        if (mod === 1) {
            connectedItems += '<div class="ui three connected items segment">';
        };

        connectedItems += LoadImageGroup(files[i]);

        if (mod === 0 || i === files.length - 1) {
            connectedItems += "</div>";
            mediaManagerFiles.append(connectedItems);

            connectedItems = "";
        };

        mediaManagerFiles.removeClass("loading");
    };
};

function LoadImageGroup(file) {
    var handlerPath = ResolveUrl("~/Handlers/ImageHandler.ashx");

    var html = '<div class="item"><div class="content"><div class="ui toggle checkbox"><input type="checkbox"><label>' + file.FileName + '</label></div></div>';
    html += '<div class="image"><img onclick="toggleCheck(this)" src="' + handlerPath + '?H=150&W=250&Path=' + file.RelativePath + '"></div></div>';

    if (file.IsDirectory) {
        html = '<div class="item" ondblclick="ReloadMedia(this)"><div class="content"><div class="ui teal header"><div class="circular ui icon mini teal button"><i class="folder icon"></i></div>&nbsp;&nbsp;' + file.FileName + '</div></div><div class="ui inverted teal segment"><i class="massive open folder icon"></i></div></div>';
    };

    return html;
};

function ReloadMedia(element) {
    var target = $(element).text().trim();

    InitializeMedia(target);
};
