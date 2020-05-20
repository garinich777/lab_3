using System;
using System.IO;

namespace lab_3.Model
{
    public static class FileModel
    {
        public static void WriteFile(double[] settings, string path)
        {
            string content = string.Empty;
            foreach (double set in settings)
                content += set.ToString() + Environment.NewLine;            

            using (StreamWriter writer = new StreamWriter(path))
                writer.Write(content);
        }

        public static bool TryReadArray(out double[] settings, string path)
        {
            string content;
            settings = new double[4];
            using (StreamReader reader = new StreamReader(path))
                content = reader.ReadToEnd();

            string[] settings_string = content.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (settings_string.Length != 4)
                return false;            

            for (int i = 0; i < 4; i++)
                if (!double.TryParse(settings_string[i], out settings[i]))
                    return false;

            return true;
        }
    }
}
