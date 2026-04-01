using System.Collections.Generic;

namespace QuizApp.Models
{
    public class Question
    {
        public string Category { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string ImageFile { get; set; }
    }
}
