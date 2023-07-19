namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaración o Matrícula de nuestras interfaces a nivel de Repositorio
        IMedicationRepository Medication { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
