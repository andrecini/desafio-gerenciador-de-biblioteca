namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public interface IEntity<T> 
        where T : IComparable, IEquatable<T>
    {
        T Id { get; set; }
    }
}
