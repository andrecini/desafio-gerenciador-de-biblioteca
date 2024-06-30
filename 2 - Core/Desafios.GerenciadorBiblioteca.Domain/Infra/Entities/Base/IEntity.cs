namespace Desafios.GerenciadorBiblioteca.Domain.Infra.Entities.Base
{
    public interface IEntity<T>
        where T : IComparable, IEquatable<T>
    {
        T Id { get; set; }
    }
}
