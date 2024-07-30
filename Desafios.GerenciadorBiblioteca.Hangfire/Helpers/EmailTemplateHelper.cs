namespace Desafios.GerenciadorBiblioteca.Hangfire.Helpers
{
    public static class EmailTemplateHelper
    {
        public static string GetVerificationEmailTemplate(string verificationCode)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Verificação de Email</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f9;
                            margin: 0;
                            padding: 0;
                            text-align: center;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            background-color: #ffffff;
                            border-radius: 10px;
                            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
                        }}
                        .header {{
                            background-color: #4CAF50;
                            color: white;
                            padding: 10px;
                            border-radius: 10px 10px 0 0;
                        }}
                        .content {{
                            padding: 20px;
                        }}
                        .code {{
                            font-size: 24px;
                            color: #4CAF50;
                            font-weight: bold;
                        }}
                        .footer {{
                            margin-top: 20px;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Verificação de Email</h1>
                        </div>
                        <div class='content'>
                            <p>Olá,</p>
                            <p>Você solicitou a recuperação de senha. Use o código abaixo para continuar o processo:</p>
                            <p class='code'>{verificationCode}</p>
                            <p>Se você não solicitou esta recuperação, por favor, ignore este email.</p>
                        </div>
                        <div class='footer'>
                            <p>Obrigado,</p>
                            <p>Equipe da Biblioteca</p>
                        </div>
                    </div>
                </body>
                </html>
            ";
        }

        public static string GetOverdueLoanEmailTemplate(string userName, string bookTitle, DateTime loanDate, DateTime dueDate)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Empréstimo Atrasado</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f9;
                            margin: 0;
                            padding: 0;
                            text-align: center;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            background-color: #f5f5f5;
                            border-radius: 10px;
                            box-shadow: 0 2px 4px rgba(0,0,0,0.2);
                        }}
                        .header {{
                            background-color: #FF6347;
                            color: white;
                            padding: 10px;
                            border-radius: 10px 10px 0 0;
                        }}
                        .content {{
                            padding: 20px;
                        }}
                        .book-title {{
                            font-size: 18px;
                            color: #FF6347;
                            font-weight: bold;
                        }}
                        .footer {{
                            margin-top: 20px;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Empréstimo Atrasado</h1>
                        </div>
                        <div class='content'>
                            <p>Olá {userName},</p>
                            <p>Este é um lembrete de que o empréstimo do seguinte livro está atrasado:</p>
                            <p class='book-title'>{bookTitle}</p>
                            <p>Data do empréstimo: {loanDate.ToShortDateString()}</p>
                            <p>Data de devolução: {dueDate.ToShortDateString()}</p>
                            <p>Por favor, devolva o livro o mais rápido possível para evitar multas adicionais.</p>
                        </div>
                        <div class='footer'>
                            <p>Obrigado,</p>
                            <p>Equipe da Biblioteca</p>
                        </div>
                    </div>
                </body>
                </html>
            ";
        }
    }
}
