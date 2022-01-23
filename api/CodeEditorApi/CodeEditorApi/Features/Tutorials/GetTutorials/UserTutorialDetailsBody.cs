using System;
using System.ComponentModel.DataAnnotations;

namespace CodeEditorApi.Features.Tutorials.GetTutorials
{
    public class UserTutorialDetailsBody
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? DifficultyId { get; set; }
        public int? LanguageId { get; set; }
        public bool InProgress { get; set; }
        public bool IsCompleted { get; set; }

        public UserTutorialDetailsBody(int Id, int UserId, int? DifficultyId, int? LanguageId, bool InProgress, bool IsCommpleted)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.DifficultyId = DifficultyId;
            this.LanguageId = LanguageId;
            this.InProgress = InProgress;
            this.IsCompleted = IsCommpleted;
        }
    }

}
