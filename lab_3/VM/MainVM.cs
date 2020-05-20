using lab_3.Model;
using System;

namespace lab_3.VM
{
    public static class MainVM
    {
        public static double GetFunc(double x, double a)
        {
            return MathModel.GetFunc(x, a);
        }

        public static bool WriteFile(double[] settings, string file_path)
        {
            if (settings.Length != 4 || settings == null)
                return false;
            FileModel.WriteFile(settings, file_path);
            return true;
        }

        public static bool ReadFile(out double[] settings, string file_path)
        {            
            return FileModel.TryReadArray(out settings, file_path);
        }

        public static string AboutMessage()
        {
            return "Lab_2" + Environment.NewLine +
            "Лабораторная работа №3" + Environment.NewLine +
            "График функции" + Environment.NewLine +
            "Написать программу для построения" + Environment.NewLine +
            "графика функции" + Environment.NewLine +
            "Студент группы 484" + Environment.NewLine +
            "Осипов Игорь Вадимович" + Environment.NewLine +
            "2020 год";
        }
    }
}
