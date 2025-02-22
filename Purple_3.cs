using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _name, _surname;
            private double[] _marks;
            private int[] _places_judges;
            private int _amount;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Marks
            {
                get
                {
                    if (_marks == null || _marks.Length != 7) return default(double[]);
                    double[] copy = new double[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            public int[] Places
            {
                get
                {
                    if (_places_judges == null || _places_judges.Length != 7) return default(int[]);
                    int[] copy = new int[_places_judges.Length];
                    Array.Copy(_places_judges, copy, _places_judges.Length);
                    return copy;
                }
            }

            public int Score // total_marks_judges 
            {
                get
                {
                    if (_places_judges == null) return default;
                    int copy = default;
                    for (int i = 0; i < _places_judges.Length; i++)
                    {
                        copy += _places_judges[i];
                    }
                    return copy;
                }
            }
            private int Total_place // [34,5,23,1,32,4,89] ==> 1 = the best position at all
            {
                get
                {
                    if (_places_judges == null || _places_judges.Length == 0) return default(int);
                    int unnes = 10000000; int ind_top_mesta = -1;
                    for (int i = 0; i < _places_judges.Length; i++)
                    {
                        if (_places_judges[i] < unnes)
                        {
                            {
                                unnes = _places_judges[i]; ind_top_mesta = i;
                            }
                        }
                    }
                    return _places_judges[ind_top_mesta];
                }
            }
            private double Total_Sum_Mark // last stolbetc in test
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return default;
                    double sum_copy = default;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        sum_copy += _marks[i];

                    }
                    return sum_copy;
                }
            }
            public Participant(string name, string surname)
            {
                _name = name; _surname = surname; _amount = 0;
                _places_judges = new int[] { 0, 0, 0, 0, 0, 0, 0 };
                _marks = new double[] { 0, 0, 0, 0, 0, 0, 0 };
            }

            public void Evaluate(double result)
            {
                if (result < 0 || result > 6 || _marks == null) return;
                _marks[_amount++] = result;
            }
            public static void SetPlaces(Participant[] participants)
            {
                for (int i = 0; i < 7; i++)
                {
                    Array.Sort(participants, (a, b) =>
                    {
                        if (a.Marks[i] - b.Marks[i] < 0) return 1;
                        else if (a.Marks[i] - b.Marks[i] > 0) return -1;
                        else return 0;
                    });
                    for (int j = 0; j < participants.Length; j++)
                    {
                        participants[j].Link_for_SetPlaces(i, j+1);
                    }
                }
            }
            private void Link_for_SetPlaces(int i, int j)
            {
                if (i < 0 || i > 7) return;
                if (_places_judges == null || _places_judges.Length == 0) return;
                _places_judges[i] = j;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                Array.Sort(array, (a, b) =>
                {
                    if (a.Score == b.Score)
                    {
                        if (a.Total_place == b.Total_place)
                        {
                            if (a.Total_Sum_Mark - b.Total_Sum_Mark > 0) return -1;
                            else if (a.Total_Sum_Mark - b.Total_Sum_Mark < 0) return 1;
                            else return 0;
                        }
                        return a.Total_place - b.Total_place;
                    }
                    return a.Score - b.Score;
                }
                );
            }
            public void Print() { }
        }
    }
}

