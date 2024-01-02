using System.ComponentModel.DataAnnotations;

namespace ExpensesApi.Model
{
    public class User
    {
        public string Password { get; set; }
        public string UserName { get; set; }

        [Key]
        public int Id { get; set; }
    }
}
