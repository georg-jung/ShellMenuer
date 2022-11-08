# ShellMenuer

Easily create Windows Explorer Context Menu items using .Net. Does not require elevation, installation or COM server registration. Supports cascading menus.

See [Microsoft's Documentation](https://learn.microsoft.com/en-us/windows/win32/shell/context-menu-handlers#creating-cascading-menus-with-the-extendedsubcommandskey-registry-entry) on how this works.

## Classes to use

* `*`: All kinds of files, no folders, Placeholder `%1`
* `Directory`: Folders (if one folder is selected inside another one), Placeholder `%1`
* `Directory\Background`: Folders (no file selected, right click on empty space), Placeholder `%V`

## Further reading

* <https://learn.microsoft.com/en-us/windows/win32/shell/fa-file-types>
* <https://learn.microsoft.com/en-us/windows/win32/shell/app-registration>
* <https://answers.microsoft.com/en-us/windows/forum/all/how-to-add-my-own-program-to-the-list-of-default/cd1d1305-9870-4156-9b99-fc03b40c1fc9>
* <https://learn.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shchangenotify>
