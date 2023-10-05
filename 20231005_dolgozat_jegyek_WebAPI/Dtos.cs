namespace _20231005_dolgozat_jegyek_WebAPI.Controllers
{
    public class Dtos
    {
        public record JegyekDto (Guid id, int jegy, string leiras, DateTimeOffset letrejottido);
        public record CreateJegyekDto(int jegy, string leiras);
        public record UpdateJegyekDto(int jegy, string leiras);
    }
}
