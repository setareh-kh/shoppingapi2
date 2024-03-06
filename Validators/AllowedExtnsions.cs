using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Validators
{
    public class AllowedExtnsions : ValidationAttribute
    {
        private readonly string[] _extensions;
        private readonly bool _isList;
        public AllowedExtnsions(string[] extensions, bool isList=false)
        {
            _extensions = extensions;
            _isList=isList;

        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (_isList)
            {
                var files = value as List<IFormFile>;
                for (int i = 0; i < files!.Count; i++)
                {
                    var ext = Path.GetExtension(files[i].FileName).ToLower();
                    if (!_extensions.Contains(ext))
                        return new ValidationResult($":(((((((((file{i} format is not valid ");
                }
                //if all of the images are valid return
                return ValidationResult.Success;
            }
            else
            {
                var file = value as IFormFile;
                var ext = Path.GetExtension(file!.FileName).ToLower();
                return _extensions.Contains(ext)? ValidationResult.Success:new ValidationResult($":(((((((((file format is not valid ");

            }
        }
    }
}