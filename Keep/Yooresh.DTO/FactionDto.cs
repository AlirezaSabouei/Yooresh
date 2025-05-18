namespace Eloy.DTO;

public class FactionDto
{
	public Guid Id { get; set; }
	private List<string> Advantages { get; set; } = new();
	private List<string> Disadvantages { get; set; } = new();
	public string Name { get; set; } = null!;
}
