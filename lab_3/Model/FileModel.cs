using System;
using System.IO;
using System.Text.RegularExpressions;


namespace lab_3.Model
{
    public static class FileModel
    {
        public static void WriteFile(string path, string ciphertext, string sourcetext)
        {
            string content = "<ciphertext>" + ciphertext + "</ciphertext>" + Environment.NewLine + "<sourcetext>" + sourcetext + "</sourcetext>";

            using (StreamWriter writer = new StreamWriter(path))
                writer.Write(content);
        }

        public static void WriteFile(string path, string ciphertext)
        {
            string content = "<ciphertext>" + ciphertext + "</ciphertext>";

            using (StreamWriter writer = new StreamWriter(path))
                writer.Write(content);
        }

        public static bool TryReadArray(string path, out string cipher_text, out string source_text)
        {
            string file_content;
            using (StreamReader reader = new StreamReader(path))
                file_content = reader.ReadToEnd();

            Match cipher_text_match = Regex.Match(file_content, "<ciphertext>(.*?)</ciphertext>");
            Match source_text_match = Regex.Match(file_content, "<sourcetext>(.*?)</sourcetext>");

            cipher_text = cipher_text_match.Groups[1].Value;
            source_text = source_text_match.Groups[1].Value;

            return source_text != string.Empty;
        }
    }
}
