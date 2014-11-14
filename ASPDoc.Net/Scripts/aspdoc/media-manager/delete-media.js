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

function DeleteMedia() {
    if (!GetConfirmation()) {
        return;
    };

    var mediaManager = $("#MediaManager");
    var selectedItems = mediaManager.find("input[type=checkbox]:checked");

    var files = [];

    if (selectedItems.length === 0) {
        return;
    };

    selectedItems.each(function () {
        var label = $(this).siblings("label");

        var href = ResolveUrl(Path.combine(currentDirectory) + label.html());
        files.push(GetRelativePath(href));
    });

    var ajaxDeleteFiles = AjaxDeleteFiles(files);

    ajaxDeleteFiles.success(function () {
        AjaxLoad(currentDirectory);
    });

    ajaxDeleteFiles.fail(function (xhr) {
        alert(xhr.responseText);
    });
};

function AjaxDeleteFiles(files) {
    var url = "~/Services/MediaService.asmx/DeleteFiles";
    url = ResolveUrl(url);
    var data = AppendParameter("", "files", files);
    data = GetData(data);

    return GetAjax(url, data);
};