using area_adm.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace area_adm.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManger, SignInManager<IdentityUser> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new AccountViewModel()
            {
                ReturnUrl = returnUrl
            });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountViewModel accountVM)
        {
            if (ModelState.IsValid)
            {

                // Tenta acessar o Login. 
                var result = await _signInManager.PasswordSignInAsync(accountVM.Nome, accountVM.Senha, accountVM.LembrarMe, false);

                // Se login for bem sucedido, o usuário será direcionado a página seguinte.
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(accountVM.ReturnUrl))
                    {
                        // Se houver dados na string do ReturnUrl, o usuário vai para página Home.
                        return RedirectToAction("Index", "Home");
                    }
                    // Se não o usuário vai para página seguinte.
                    return Redirect(accountVM.ReturnUrl);
                }
                // Se não o usuário vai receber uma menssagem e vai tentar denovo.
                else
                {
                    ModelState.AddModelError(string.Empty, "Nome de usuário ou senha inválidos!!");
                    return View(accountVM);
                }
            }
            // Se autenticação falhar, será feito outra tentativa de acesso ao login.
            return View(accountVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountViewModel accountVM)
        {
            if (ModelState.IsValid)
            {
                // Cria um nome de usuário é o email vinculado. 
                var user = new IdentityUser { UserName = accountVM.Nome, Email = accountVM.Email };
                // Cria uma senha
                var result = await _userManger.CreateAsync(user, accountVM.Senha);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro ao tentar criar uma conta, tente novamente.");
                    return View(accountVM);
                }
            }
            return View(accountVM);
        }

        public IActionResult Register(string returnUrl)
        {
            return View(new AccountViewModel()
            {
                ReturnUrl = returnUrl
            });

        }

    }
}
