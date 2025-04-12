namespace Backend.Dtos
{
    public class ResponseGeneralDto
    {
        public int Respuesta { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string Mensaje2 { get; set; } = "Hola desde mensaje 2";
    }
}
