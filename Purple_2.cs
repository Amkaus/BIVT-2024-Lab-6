using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _name, _surname;
            private int _distance;
            private int[] _marks; // 5 marks


            public string Name => _name;
            public string Surname => _surname;

            public int Distance => _distance;

            public int[] Marks
            {
                get
                {
                    if (_marks == null || _marks.Length != 5) return default(int[]);
                    int[] scores = new int[_marks.Length];
                    Array.Copy(_marks, scores, _marks.Length);
                    return scores;
                }
            }

            public int Result
            {
                get
                {
                    if (_marks == null || _distance == 0) return 0;

                    int result = 0; int imin = 0; int imax = 0; int copy = _distance;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        result += _marks[i];
                        if (_marks[i] > _marks[imax]) { imax = i; }
                        if (_marks[i] < _marks[imin]) { imin = i; }
                    }
                    result -= _marks[imin]; result -= _marks[imax];
                    int x = Math.Max(0, 60 + 2 * (copy - 120));
                    result += x;
                    return result;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name; _surname = surname; _distance = 0;
                _marks = new int[] { 0, 0, 0, 0, 0 };
            }

            public void Jump(int distance, int[] marks)
            {
                if (marks == null || _marks == null || distance < 0 || marks.Length != _marks.Length) return;
                _distance = distance;
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[i] = marks[i];
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Array.Sort(array, (a, b) => { return b.Result - a.Result; });
            }
            public void Print() { }
        }
    }
}

