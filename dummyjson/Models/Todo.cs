using System.ComponentModel.DataAnnotations;

namespace dummyjson.Models
{
	public class Todo
	{
		[Key]
        public int Id { get; set; }
        public string todo { get; set; }
        public bool completed { get; set; }
        public int userId { get; set; }
		     
	}
}
