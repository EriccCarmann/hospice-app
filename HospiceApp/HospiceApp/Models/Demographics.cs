namespace HospiceApp.Models;

public class Demographics
{
    public string FullName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string FullAdress { get; set; }
    public string PhoneNumber { get; set; }
    public string SecondaryPhoneNumber { get; set; } = string.Empty;
    public string PrimaryInsurance { get; set; } = string.Empty;
}