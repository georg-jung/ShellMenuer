namespace ShellMenuer.Tests
{
    public class CascadingVerbs
    {
        private readonly List<NodeVerb> _nodeVerbs = new();
        private readonly List<CascadingVerb> _cscVerbs = new();

        public CascadingVerbs()
        {
            _nodeVerbs.Add(new("cmd3", "Open cmd here", "cmd \"%1\"")
            {
                Icon = "cmd.exe",
            });

            _nodeVerbs.Add(new("cmd2", "Open cmd here (Extended)", "cmd \"%1\"")
            {
                Icon = "calc.exe",
                Extended = true,
            });

            var vrbs = new VerbCollection("ContextMenus\\Cascade");
            _nodeVerbs.ForEach(x => vrbs.Add(x));
            _cscVerbs.Add(new("csc1", "Cascade", vrbs)
            {
                Icon = "cmd.exe",
                Extended = false,
            });

            _cscVerbs.Add(new("csc2", "Cascade (Extended)", vrbs)
            {
                Icon = "notepad.exe",
                Extended = true,
            });
        }

        [Fact]
        public void CreateForDirectories()
        {
            var cls = new Class("Directory");

            // _nodeVerbs.ForEach(x => cls.Verbs.Add(x));
            _cscVerbs.ForEach(x => cls.Verbs.Add(x));
            ShellMenu.EnsureCreated(cls, false);
        }

        [Fact]
        public void DeleteForDirectories()
        {
            var cls = new Class("Directory");
            _cscVerbs.ForEach(x => cls.Verbs.Add(x));
            ShellMenu.EnsureDeleted(cls, false);
        }
    }
}
