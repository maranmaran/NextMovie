using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserMovie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public bool Liked { get; set; }
    }
}
