namespace ShellMenuer.Tests
{
    public class CascadingVerbs
    {
        private readonly List<Verb> _nodeVerbs = new();
        private readonly List<CascadingVerb> _cscVerbs = new();

        public CascadingVerbs()
        {
            _nodeVerbs.Add(new NodeVerb("ShellMenuer.cmd3", "Open cmd here", "cmd \"%1\"")
            {
                Icon = "cmd.exe",
            });

            _nodeVerbs.Add(new NodeVerb("ShellMenuer.cmd2", "Open cmd here (Extended)", "cmd \"%1\"")
            {
                Icon = "calc.exe",
                Extended = true,
            });

            _cscVerbs.Add(new("ShellMenuer.csc1", "Cascade", _nodeVerbs)
            {
                Icon = "cmd.exe",
                Extended = false,
            });

            _cscVerbs.Add(new("ShellMenuer.csc2", "Cascade (Extended)", _nodeVerbs)
            {
                Icon = "notepad.exe",
                Extended = true,
            });

            // This kind of works, it might not be a great idea though
            // Should we block it?
            // vrbs.Add(new CascadingVerb("cscLoop", "Infinite Loop", vrbs));
        }

        [Fact]
        public void ForFiles()
        {
            ShellMenu.EnsureCreated(ShellMenuScope.FileItems, _cscVerbs);

            //ShellMenu.EnsureDeleted(ShellMenuScope.FileItems, _cscVerbs);
        }

        [Fact]
        public void ForDirectories()
        {
            ShellMenu.EnsureCreated(ShellMenuScope.DirectoryItems, _cscVerbs);

            ShellMenu.EnsureDeleted(ShellMenuScope.DirectoryItems, _cscVerbs);
        }
    }
}
