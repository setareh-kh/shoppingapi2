using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Validators
{
    public class AllowedExtnsions:ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtnsions(string[] extensions)
        {
            _extensions=extensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            var file = value as IFormFile;
            Console.WriteLine(file!.FileName);
            var ext=Path.GetExtension(file!.FileName).ToLower();
            Console.WriteLine(ext);
            if(_extensions.Contains(ext))
            {
                Console.WriteLine("true contins");
                return ValidationResult.Success;
            }
            else return new ValidationResult(":(((((((((file format is not valid " );
        }


    }
}