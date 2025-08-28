
namespace LayerDomainModel
{
    public interface DMPersonaNatural
    {

        public int IdPersonaNatural { get; set; }
        public string? CedulaPerosonaNatural { get; set; }

        public string? ApellidoPersonaNatural { get; set; }

        public int Id_genero {  get; set; }

        public DMGenero? objGenero { get; set; }

    }
}
