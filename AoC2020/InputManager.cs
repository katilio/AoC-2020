using System.IO;

namespace AoC2020
{
    public static class InputManager
    {
        public static string[] StringArrayFromFile(string filename)
        {
            string filePath = "C:/Users/Adam/Documents/AoC2020/" + filename;
            var input = System.IO.File.ReadAllLines(filePath);
            return input;
        }
    }
}