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

function CreateDirectory() {
    var directory = $("#DirectoryName").val();

    var ajaxCreateDirectory = AjaxCreateDirectory(directory, GetRelativePath(currentDirectory));

    ajaxCreateDirectory.success(function () {
        $('#DirectoryInfo').toggle(500);
        AjaxLoad(currentDirectory);
        $("#DirectoryName").val("");
    });

    ajaxCreateDirectory.fail(function (xhr) {
        alert(xhr.responseText);
    });
};

function AjaxCreateDirectory(directory, currentDirectory) {
    var url = "~/Services/MediaService.asmx/CreateDirectory";
    url = ResolveUrl(url);
    var data = AppendParameter("", "directoryName", directory);
    data = AppendParameter(data, "currentDirectory", currentDirectory);
    data = GetData(data);

    return GetAjax(url, data);
};
