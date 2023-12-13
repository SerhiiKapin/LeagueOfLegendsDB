using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfLegendsDB
{
    public class Character
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ClassID { get; set; }
        public Class Class { get; set; }
        public int BlueEssence { get; set; }
        public int RiotPoints { get; set; }
        public string? ImagePath { get; set; }
    }
}
