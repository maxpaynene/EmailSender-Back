namespace Models;

public class EmailForm
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Rut { get; set; }
    public required IFormFile PdfFile { get; set; }
}