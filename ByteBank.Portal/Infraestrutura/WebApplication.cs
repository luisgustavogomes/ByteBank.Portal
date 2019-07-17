using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Portal.Infraestrutura
{
    public class WebApplication
    {
        private readonly string[] _prefixos;
        public WebApplication(string[] prefixos)
        {
            _prefixos = prefixos ?? throw new ArgumentNullException(nameof(prefixos));
        }

        public void Iniciar()
        {
            var httpListener = new HttpListener();
            foreach (var item in _prefixos)
                httpListener.Prefixes.Add(item);

            httpListener.Start();

            var contexto = httpListener.GetContext();
            var request = contexto.Request;
            var response = contexto.Response;

            var respostaConteudo = "Hello World";
            var respostaConteudoBytes = Encoding.UTF8.GetBytes(respostaConteudo);

            response.ContentType = "text/html; charset=utf=8";
            response.StatusCode = 200;
            response.ContentLength64 = respostaConteudoBytes.Length;
            response.OutputStream.Write(respostaConteudoBytes, 0, respostaConteudoBytes.Length);
            response.OutputStream.Close();

            httpListener.Stop();
        }
    }
}
