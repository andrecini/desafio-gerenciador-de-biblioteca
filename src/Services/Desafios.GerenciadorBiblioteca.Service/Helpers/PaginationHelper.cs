namespace Desafios.GerenciadorBiblioteca.Service.Helpers
{
    public static class PaginationHelper
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int page, int size)
        {
            if (page < 1) page = 1;

            if (size < 1) size = 10;

            return source.Skip((page - 1) * size).Take(size);
        }
    }
}
