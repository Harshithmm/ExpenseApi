using System.ComponentModel.DataAnnotations;

namespace ExpensesApi.Model
{
    public class Entry
    {
        [Key]
        public int Id { get; set; }
        public bool IsExpense { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }

    }
}
