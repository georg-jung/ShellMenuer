using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellMenuer.Tests
{
    public class ReadmeCode
    {
        [Fact]
        public void SimpleContextMenuItem()
        {
            var verb = new NodeVerb("MyGreatApp.OpenNotepad", "Open in notepad", "notepad \"%1\"")
            {
                Icon = "calc.exe", // use this file's icon
                Extended = false, // set to true if it should just be visible if shift is pressed while opening the menue
            };

            // user-specific "installation"
            ShellMenu.EnsureCreated(ShellMenuScope.FileItems, verb, false);

            // machine-wide "installation", requires elevation
            ShellMenu.EnsureCreated(ShellMenuScope.FileItems, verb, true);

            //// user-specific "uninstall"
            ShellMenu.EnsureDeleted(ShellMenuScope.FileItems, verb, false);

            //// machine-wide "uninstall", requires elevation
            ShellMenu.EnsureDeleted(ShellMenuScope.FileItems, verb, true);
        }

        [Fact]
        public void Cascading()
        {
            var verb1 = new NodeVerb("MyGreatApp.OpenNotepad", "Open in notepad", "notepad \"%1\"")
            {
                Icon = "calc.exe", // use this file's icon
                Extended = false, // set to true if it should just be visible if shift is pressed while opening the menue
            };

            var verb2 = new NodeVerb("MyGreatApp.OpenCmd", "Open in cmd", "cmd /k cd \"%1\"");

            var superVerb1 = new CascadingVerb("MyGreatApp.Menu", "My great app");
            superVerb1.Children.Add(verb1);

            var superVerb2 = new CascadingVerb("MyGreatApp.InnerMenu", "Further down", new[] { verb2 })
            {
                Icon = "cmd.exe",
            };
            superVerb1.Children.Add(superVerb2);

            // user-specific "installation"
            ShellMenu.EnsureCreated(ShellMenuScope.FileItems, superVerb1, false);

            // user-specific "uninstall"
            ShellMenu.EnsureDeleted(ShellMenuScope.FileItems, superVerb1, false);
        }
    }
}
