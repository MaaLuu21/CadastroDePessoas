using ConstanciaDeDados.Services;

namespace ConstanciaDeDados
{
    class Program
    {
        static void Main(string[] args)
        {
            

            try
            {
                var menu = new MenuInterface();
                menu.MenuExecutar();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro inesperado: " + e.Message);
            }
        }
    }
}
