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

$(document).ready(function () {
    // ReSharper disable once AssignToImplicitGlobalInFunctionScope
    jQueryLayout = $("body").layout({
        applyDefaultStyles: true,
        autoResize: true,
        spacing_open: 3,
        stateManagement__enabled: true,
        north: {
            enableCursorHotkey: false,
            closable: false,
            resizable: false,
            spacing_open: 0,
            spacing_closed: 0,
            size: "40"
        },
        west:
        {
            enableCursorHotkey: false,
            size: "50%",
            minSize: "200",
            onresize_start: function () {
            },
            cssReq: {
                overflow: "auto",
                height: "auto"
            }
        },
    });
});