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

function ViewMedia() {
    var mediaManager = $("#MediaManager");
    var selectedItems = mediaManager.find("input[type=checkbox]:checked");
    var html = "";

    if (selectedItems.length === 0) {
        return;
    };

    selectedItems.each(function () {
        var label = $(this).siblings("label");

        var href = ResolveUrl(Path.combine(currentDirectory) + label.html());

        window.open(href, "_blank");
    });

    var editDiv = $("div.markdown");
    var editor = ace.edit(editDiv[0]);

    $("#MediaManager").modal("hide");
    editor.insert(html);
};
