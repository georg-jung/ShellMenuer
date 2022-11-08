namespace ShellMenuer.Tests
{
    public class NodeVerbs
    {
        private readonly List<NodeVerb> _verbs = new();

        public NodeVerbs()
        {
            _verbs.Add(new("cmd3", "Open cmd here", "cmd \"%1\"")
            {
                Icon = "cmd.exe",
            });

            _verbs.Add(new("cmd2", "Open cmd here (Extended)", "cmd \"%1\"")
            {
                Icon = "cmd.exe",
                Extended = true,
            });
        }

        [Fact]
        public void CreateForDirectories()
        {
            var cls = new Class("Directory");
            _verbs.ForEach(x => cls.Verbs.Add(x));
            ShellMenu.EnsureCreated(cls, false);
        }

        [Fact]
        public void DeleteForDirectories()
        {
            var cls = new Class("Directory");
            _verbs.ForEach(x => cls.Verbs.Add(x));
            ShellMenu.EnsureDeleted(cls, false);
        }
    }
}
