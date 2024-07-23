namespace Desafios.GerenciadorBiblioteca.Service.Helpers
{
    public static class PaginationHelper
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int page, int size)
        {
            if (page == 0 && size == 0) return source;

            return source.Skip((page - 1) * size).Take(size);
        }
    }
}
