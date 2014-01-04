js.Modernizr
=============

A common location for Modernizr.js and related script libraries for Orchard Project.

This module defines a script manifest for Modernizr Library with name "Modernizr".<br>
You can include Modernizr script inside your Razor views using:<br>
Script.Require("Modernizr")<br>

Modernizr module will automatically insert your Modernizr.js script in every page.<br>
You can disable this bevavior and include modernizr on demand (using Script.Require("Modernizr) inside your theme/view) by unchecking Auto Enable at modernizr module settings.<br>

You can also set a customized version of Modernizr to automatically load on every page.<br>
Create your own version of modernizrs, upload it into your media library under a folder named scripts and select it through modernizr module settings (/admin/setting/modernizr)

** Customized versions only work with the Auto Enable feature.