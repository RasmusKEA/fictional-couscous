namespace RandomPerson;

public class Person
{
    public Person(string? name, string? surname, string? gender, string? phoneNumber, string? address, string? cpr)
    {
        Name = name;
        Surname = surname;
        Gender = gender;
        PhoneNumber = phoneNumber;
        Address = address;
        Cpr = cpr;
    }
    
    public Person(string? name, string? surname, string? gender)
    {
        Name = name;
        Surname = surname;
        Gender = gender;
    }

    public Person()
    {
        
    }


    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Cpr { get; set; }

}