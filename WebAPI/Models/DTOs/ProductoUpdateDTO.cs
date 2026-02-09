namespace WebAPI.Models.DTOs
{
    public class ProductoUpdateDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }
    }
}
