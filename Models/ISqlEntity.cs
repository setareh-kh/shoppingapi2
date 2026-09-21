using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Models;

public interface ISqlEntity
{
    [Key] public int Id { get; set; }
}