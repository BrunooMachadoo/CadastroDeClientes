namespace CadastroDeClientesBackEnd.Models
{
    public class ServiceResponse<T>
    {
        public T? Dados { get; set; }
        public string Msg { get; set; }
        public string Status { get; set; }
    }
}
