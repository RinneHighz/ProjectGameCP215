using System;
using System.IO;
using System.Collections.Generic;

namespace ProjectGameCP215
{
    public static class ScoreManager
    {
        private static string filePath = "Content/Resource/Score/scoreData.txt";

        // บันทึกคะแนนลงไฟล์
        public static void SaveScore(int score)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true)) // Append mode
                {
                    writer.WriteLine(score);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving score: {ex.Message}");
            }
        }

        // อ่านคะแนนทั้งหมดจากไฟล์
        public static List<int> GetAllScores()
        {
            if (!File.Exists(filePath))
                return new List<int>();

            try
            {
                var scores = new List<int>();
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (int.TryParse(line, out int score))
                    {
                        scores.Add(score);
                    }
                }
                return scores;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading scores: {ex.Message}");
                return new List<int>();
            }
        }
    }
}
