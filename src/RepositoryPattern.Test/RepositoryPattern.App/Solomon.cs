using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApp
{
    internal class Solomon
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? Folder { get; set; }
        public int Olaf { get; set; }
        public Casualty? Casualty { get; set; }
        public List<int>? Hellos { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public int[]? Consolated { get; set; }
    }
    internal class Casualty
    {
        public string? Value { get; set; }
        public string? Folder { get; set; }
    }
}
