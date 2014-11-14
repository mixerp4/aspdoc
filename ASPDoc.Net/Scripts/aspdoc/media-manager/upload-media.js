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

function Upload() {
    var file = $("#File");

    var url = ResolveUrl('~/Handlers/FileUpload.ashx?Path=') + encodeURI(currentDirectory);

    file.upload(url, function () {
        $("#BrowseButton").prop("disabled", true);
        file.val(""); //Clear file upload control.
        AjaxLoad(currentDirectory);
    }, function (progress, value) {
        $("#Progress").find(".bar").html("&nbsp;".repeat(value));
    });
};

$("#BrowseButton").click(function () {
    $('#File').trigger('click'); $('#Progress').find('.bar').html('');
});

$("#UploadButton").click(function () {
    Upload();
});

String.prototype.repeat = function (num) {
    return new Array(num + 1).join(this);
};
