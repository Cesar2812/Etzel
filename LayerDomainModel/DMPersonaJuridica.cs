

namespace LayerDomainModel
{
    public interface DMPersonaJuridica
    {
        public int IdPersonaJuridica { get; set; }

        public string? NumeroRuc { get; set; }

        public string? RazonSocial { get; set; }

        public int idSectorEconomico { get; set; }

        public DMTipoSectorEconomico? objSector { get; set; }
    }
}
