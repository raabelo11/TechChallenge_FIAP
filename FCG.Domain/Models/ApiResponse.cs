using System.Reflection.Metadata.Ecma335;

namespace FCG.Domain.Models
{
    public class ApiResponse
    {
        public bool Ok { get; set; }
        public object? Data { get; set; }
        public string[]? Errors { get; set; }
    }
}
