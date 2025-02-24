using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_1
    {
        public struct Participant
        {
            //fields
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _amount_jumps;

            //svoystva
            public string Name => _name;
            public string Surname => _surname;

            public double[] Coefs
            {
                get
                {
                    if (_coefs == null || _coefs.Length == 0) return default(double[]);
                    double[] copy_reader = new double[_coefs.Length];
                    Array.Copy(_coefs, copy_reader, _coefs.Length);
                    return copy_reader;
                }
            }

            public int[,] Marks
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) != 4 || _marks.GetLength(1) != 7) return default(int[,]);
                    int[,] copy_reader = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    Array.Copy(_marks, copy_reader, _marks.Length);
                    return copy_reader;
                }
            }
            public double TotalScore
            {
                get
                {
                    if (_marks == null || _coefs == null) return 0;

                    double score_of_jumper = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        score_of_jumper += CountJumpScore(i);
                    }
                    return score_of_jumper;
                }
            }
            private double CountJumpScore(int index_jump)
            {
                if (_marks == null || _coefs == null) return 0;

                double curr_score = 0;
                int[] marks = new int[_marks.GetLength(1)];
                for (int i = 0; i < _marks.GetLength(1); i++)
                {
                    marks[i] = _marks[index_jump, i];
                    curr_score += marks[i];
                }
                int im = 0, imx = 0;
                for (int i = 0; i < marks.Length; i++)
                {
                    if (marks[i] < marks[im]) im = i;
                    if (marks[i] > marks[imx]) imx = i;
                }
                curr_score -= marks[im]; curr_score -= marks[imx];
                curr_score *= _coefs[index_jump];
                return curr_score;
            }
            // constructor
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[,]
                {
                    {0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0},
                    {0,0,0,0,0,0,0}
                };
                _amount_jumps = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || _coefs == null || coefs.Length != 4) return;

                for (int i = 0; i < coefs.Length; i++)
                {
                    if (coefs[i] > 3.5 || coefs[i] < 2.5)
                    {
                        return;
                    }
                }
                Array.Copy(coefs, _coefs, 4);
            }

            public void Jump(int[] marks)
            {
                if (marks == null || _marks == null ||  marks.Length != 7 ||  _amount_jumps >= 4) return;
                foreach (int mark in marks)
                {
                    if (mark < 1 || mark > 6) return;
                }
                for (int i = 0; i < marks.Length; i++)
                {
                    _marks[_amount_jumps, i] = marks[i];
                }
                _amount_jumps++;
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                Array.Sort(array, (a, b) => {
                    if (b.TotalScore - a.TotalScore > 0) return 1;
                    else if (b.TotalScore - a.TotalScore < 0) return -1;
                    else return 0;
                });
            }
            public void Print() { }
        }
    }
}

