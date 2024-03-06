using System.ComponentModel.DataAnnotations;
namespace shoppingapi2.Validators
{
    public class IsValidSizeFile : ValidationAttribute
    {
        private readonly int _maxSize;
        private readonly bool _isList;
        public IsValidSizeFile(int maxSize, bool isList=false)
        {
            _maxSize = maxSize;
            _isList = isList;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (_isList)
            {
                var files = value as List<IFormFile>;
                for (int i = 0; i < files!.Count; i++)
                {
                    if (files[i]!.Length > _maxSize)
                        return new ValidationResult($"Size of file{i} exceed {_maxSize}");
                }
                //if all of the images are valid return
                return ValidationResult.Success;
            }
            else
            {
                var file = value as IFormFile;
                return file!.Length <= _maxSize? ValidationResult.Success:new ValidationResult($"Size of file exceed {_maxSize}");

            }

        }
    }
}