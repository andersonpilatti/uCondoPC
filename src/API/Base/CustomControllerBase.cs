using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
    public class CustomControllerBase
        : ControllerBase
    {
        protected const string _msgErroGenerico = "Ocorreu um erro inexperado!";
    }
}
