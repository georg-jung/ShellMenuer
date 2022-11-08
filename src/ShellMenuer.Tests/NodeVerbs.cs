namespace ShellMenuer.Tests
{
    public class NodeVerbs
    {
        private readonly List<NodeVerb> _verbs = new();

        public NodeVerbs()
        {
            _verbs.Add(new("ShellMenuer.cmd3", "Open cmd here", "cmd \"%1\"")
            {
                Icon = "cmd.exe",
            });

            _verbs.Add(new("ShellMenuer.cmd2", "Open cmd here (Extended)", "cmd \"%1\"")
            {
                Icon = "cmd.exe",
                Extended = true,
            });
        }

        [Fact]
        public void ForFiles()
        {
            ShellMenu.EnsureCreated(ShellMenuScope.FileItems, _verbs, false);

            ShellMenu.EnsureDeleted(ShellMenuScope.FileItems, _verbs, false);
        }

        [Fact]
        public void ForDirectories()
        {
            ShellMenu.EnsureCreated(ShellMenuScope.DirectoryItems, _verbs, false);

            ShellMenu.EnsureDeleted(ShellMenuScope.DirectoryItems, _verbs, false);
        }
    }
}
