using System.ComponentModel.DataAnnotations;
namespace shoppingapi2.Validators
{
    public class IsValidSizeFile:ValidationAttribute
    {
        private readonly int _maxSize;
        public IsValidSizeFile(int maxSize)
        {
            _maxSize=maxSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) 
        {
            var file = value as IFormFile;
            var length=file!.Length;
            return length<=_maxSize ? ValidationResult.Success :new ValidationResult($"Size of file exceed {_maxSize}");
        }
    }
}