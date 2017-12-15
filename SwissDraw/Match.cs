using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissDraw
{
    public class Match
    {
        private int person1;
        private int person2;
        // result 1:Person1の勝ち　2:Person2の勝ち　0:決着がついていない　他:エラー
        private int result;

        public int Person1 { get => person1; set => person1 = value; }
        public int Person2 { get => person2; set => person2 = value; }
        public int Result { get => result; set => result = value; }

        public Match(int i,int j)
        {
            person1 = i;
            person2 = j;
            result = 0;
        }

        public Match[] MakeMatch(Dictionary<int, Person> persons, Match[] results)
        {
            Match[] matches = new Match[1];
            matches[0] = new Match (1,10);
            return matches;
        }
        public Match[] MergeMatch(Match[] results1, Match[] results2)
        {
            return results1;
        }


    }

}
