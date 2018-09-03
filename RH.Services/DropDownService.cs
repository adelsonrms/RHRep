using System;
using System.Web.Mvc;

namespace RH.Services
{
   public class DropDownService
    {
        public SelectList GerarComboSelect(dynamic fonte, string campoValor, string campoTexto)
        {
            try
            {
                SelectList lista = new SelectList(fonte, campoValor, campoTexto);
                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
