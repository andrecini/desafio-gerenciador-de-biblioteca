using MudBlazor;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Website.Services
{
    public class AlertService(ISnackbar snackbar)
    {
        private readonly ISnackbar _snackbar = snackbar;

        public void ShowSuccess(string message, HttpStatusCode statusCode)
        {
            _snackbar.Clear();
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopEnd;
            _snackbar.Add(message, Severity.Success);
        }

        public void ShowErrors(string[] errorDetails, HttpStatusCode statusCode)
        {
            _snackbar.Clear();
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopEnd;

            foreach (var error in errorDetails)
            {
                var severity = (int)statusCode > 499 ? Severity.Error : Severity.Warning;
                _snackbar.Add(error, severity);
            }
        }
    }
}
