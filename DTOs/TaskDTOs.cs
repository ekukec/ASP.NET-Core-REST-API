using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_REST_API.DTOs
{
	public class TaskDTO 
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public bool IsCompleted { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? CompletedAt { get; set; }
	}

	public class CreateTaskDTO
	{
		[Required]
		[StringLength(200, MinimumLength = 1)]
		public string Title { get; set; } = string.Empty;

		[StringLength(1000)]
		public string Description { get; set; } = string.Empty;
	}

	public class UpdateTaskDTO
	{
		[Required]
		[StringLength(200, MinimumLength = 1)]
		public string Title { get; set; } = string.Empty;

		[StringLength(1000)]
		public string Description { get; set; } = string.Empty;

		public bool IsCompleted {get; set; }
	}
}
