using Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public static class EnvioCorreo
    {
        public static string EnviarCorreo(DatosCorreo _datosCorreo, ConfiguracionCorreo _configuracion)
        {
            string respuesta= string.Empty;
            var mailMessage = new MimeMessage();
            if(_datosCorreo.De.Count> 0)
            {
                foreach (var correo in _datosCorreo.De)
                {
                    mailMessage.From.Add(new MailboxAddress(_datosCorreo.NombreEmisor, correo));
                }

            }
            if (_datosCorreo.Para.Count > 0)
            {
                foreach (var correo in _datosCorreo.Para)
                {
                    mailMessage.To.Add(new MailboxAddress(_datosCorreo.NombreReceptor, correo));
                }
            }
            if (_datosCorreo.CopiaA.Count > 0)
            {
                foreach (var correo in _datosCorreo.CopiaA)
                {
                    mailMessage.Cc.Add(new MailboxAddress(_datosCorreo.NombreReceptor, correo));
                }
            }       
            mailMessage.Subject =_datosCorreo.TituloCorreo;
            mailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = _datosCorreo.CuerpoCorreo
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_configuracion.Host, _configuracion.Puerto, SecureSocketOptions.StartTls);
                smtpClient.Authenticate(_configuracion.Correo, _configuracion.Contrasena);
                respuesta = smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);

            }

            return respuesta;
        }
    }
}
