namespace Desafios.GerenciadorBiblioteca.Service.Services.Base
{
    public class ServiceBase
    {
        protected static bool ValidateResult(int result, string exceptionMessage)
        {
            if (result > 0)
                return true;
            else
                throw new Exception(exceptionMessage);
        }

        protected static T ValidateReturnedDada<T>(T data, string exceptionMessage)
        {
            return data is not null ? data : throw new Exception(exceptionMessage);
        }
    }
}
