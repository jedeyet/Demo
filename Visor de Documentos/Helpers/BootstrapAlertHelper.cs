using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Visor_de_Documentos.Helpers
{
    public static class BootstrapAlertHelper
    {
        /// <summary>
        /// Genera el HTML para una alerta de Bootstrap.
        /// </summary>
        /// <param name="type">El tipo de alerta (success, danger, warning, info).</param>
        /// <param name="message">El mensaje que se mostrará en la alerta.</param>
        /// <returns>HTML string con la alerta.</returns>
        public static string GetAlert(string type, string message)
        {
            return $"<div class='alert alert-{type} alert-dismissible fade show' role='alert'>" +
                   $"{message}" +
                   "<button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>" +
                   "</div>";
        }
    }

}