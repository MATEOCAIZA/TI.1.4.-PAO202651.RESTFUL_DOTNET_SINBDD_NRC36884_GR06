using System.Web.Mvc;
using ec.edu.monster.modelo;
using ec.edu.monster.servicio;

namespace ec.edu.monster.vista
{
    public class HomeController : Controller
    {
        private const string LoginSessionKey = "monster_login";
        private const string ValidUser = "MONSTER";
        private const string ValidPassword = "MONSTER69";

        private readonly ConversionApiClient _conversionClient = new ConversionApiClient();

        [HttpGet]
        public ActionResult Index()
        {
            var model = new ConversionViewModel();
            model.IsAuthenticated = IsAuthenticated();
            model.UpdateUnitOptions();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ConversionViewModel model)
        {
            model.UpdateUnitOptions();

            if (IsValidLogin(model.Usuario, model.Contrasena))
            {
                Session[LoginSessionKey] = true;
                return RedirectToAction("Index");
            }

            model.IsAuthenticated = false;
            model.LoginError = "Usuario o contrasena incorrectos.";
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Convert(ConversionViewModel model)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Index");
            }

            model.IsAuthenticated = true;
            model.UpdateUnitOptions();
            var response = _conversionClient.Convert(model.ToRequest());
            model.Resultado = response.Resultado;
            model.Mensaje = response.Mensaje;

            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clear(ConversionViewModel model)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("Index");
            }

            var cleared = new ConversionViewModel
            {
                IsAuthenticated = true,
                Mensaje = string.Empty,
                Resultado = null,
                Categoria = "masa",
                Origen = string.Empty,
                Destino = string.Empty,
                Valor = 0
            };
            cleared.UpdateUnitOptions();
            return View("Index", cleared);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Remove(LoginSessionKey);
            return RedirectToAction("Index");
        }

        private bool IsAuthenticated()
        {
            return Session[LoginSessionKey] is bool && (bool)Session[LoginSessionKey];
        }

        private static bool IsValidLogin(string usuario, string contrasena)
        {
            return string.Equals(usuario, ValidUser, System.StringComparison.OrdinalIgnoreCase)
                && string.Equals(contrasena, ValidPassword, System.StringComparison.Ordinal);
        }
    }
}