using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todolistaspnetcore.Models
{
    public class ToDoPosition : IToDoPosition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Uzupełnij zadanie do wykonania")]
        public string Description { get; set; }
    }
}