using TopTal.JoggingApp.Service.Models.Helpers;

namespace TopTal.JoggingApp.Service.Api.Helpers
{
    public interface IServiceModelValidator
    {
        Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState { get; }

        void Validate(Model model);
    }
}
