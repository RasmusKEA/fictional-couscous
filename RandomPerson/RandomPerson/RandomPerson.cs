using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace RandomPerson;

public class RandomPerson
{
    private Random rnd = new Random();
    string connStr = "server=localhost;user=root;database=addresses;port=3306;password=password";

    public Person OnePerson()
    {
        var person = NameAndGender();
        var phoneNumber = PhoneNumber();
        var address = Address();
        var cpr = CprNumber(person.Gender);
        
        var onePerson = new Person
        {
            Name = person.Name,
            Surname = person.Surname,
            Gender = person.Gender,
            PhoneNumber = phoneNumber,
            Address = address,
            Cpr = cpr
        };
        
        return onePerson;
    }

    public Person[] BulkPerson(int amount)
    {
        var persons = new Person[amount];
        if (amount < 2 || amount > 100)
        {
            throw new Exception("Invalid input");
        }
        var i = 0;
        while (i < amount)
        {
            persons[i] = OnePerson();
            i++;
        }

        return persons;
    }

    public Person NameAndGender()
    {
        var fileName = "/Users/rasmusmoller/Desktop/fictional-couscous/RandomPerson/RandomPerson/persons.json";
        if (!File.Exists(fileName)) return null;
        
        var persons = JsonConvert.DeserializeObject<List<JsonPerson>>(File.ReadAllText(fileName));
        var person = persons[rnd.Next(1, persons.Count - 1)];

        return new Person(person.name, person.surname, person.gender);
    }

    public string PhoneNumber()
    {
        int[] startingDigits =
        {
            2, 30, 31, 40, 41, 42, 50, 51, 52, 53, 60, 61, 71, 81, 91, 92, 93, 342, 344,
            345, 346, 347, 348, 349, 356, 357, 359, 362, 365, 366, 389, 398, 431, 441,
            462, 466, 468, 472, 474, 476, 478, 485, 486, 488, 489, 493, 494, 495, 496,
            498, 499, 542, 543, 545, 551, 552, 556, 571, 572, 573, 574, 577, 579, 584,
            586, 587, 589, 597, 598, 627, 629, 641, 649, 658, 662, 663, 664, 665, 667,
            692, 693, 694, 697, 771, 772, 782, 783, 785, 786, 788, 789, 826, 827, 829
        };

        var startingDigit = startingDigits[rnd.Next(1, startingDigits.Length-1)];
        var concat = "";
        return startingDigit.ToString().Length switch
        {
            1 => $"{startingDigit}{rnd.Next(1000000, 9999999)}",
            2 => $"{startingDigit}{rnd.Next(100000, 999999)}",
            _ => $"{startingDigit}{rnd.Next(10000, 99999)}"
        };
    }

    public string Address()
    {
        var zipAndCity = "";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();

            var sql = "SELECT * FROM postal_code ORDER BY RAND() LIMIT 1";
                
            var cmd = new MySqlCommand(sql, conn);
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                zipAndCity = $"{rdr.GetString(0)} {rdr.GetString(1)}";
            }
            rdr.Close();
            conn.Close();
        }
        catch (Exception err)
        {
            Console.WriteLine(err.ToString());
        }
        
        const string chars = "abcdefghijklmnopqrstuvwxyzæøå";
        var address = new string(Enumerable.Repeat(chars, rnd.Next(8, 15))
            .Select(s => s[rnd.Next(s.Length)]).ToArray());

        var houseNumber = rnd.Next(1, 999);
        string[] floor =
        {
            "st", rnd.Next(1, 100).ToString(), rnd.Next(1, 100).ToString(), rnd.Next(1, 100).ToString(), 
            rnd.Next(1, 100).ToString(), rnd.Next(1, 100).ToString(), rnd.Next(1, 100).ToString()
        };

        string[] door =
        {
            "", "tv", "mf", "th"
        };

        return $"{address} {houseNumber}, {floor[rnd.Next(0,7)]} {door[rnd.Next(0,3)]}, {zipAndCity}";
    }

    public string CprNumber(string gender)
    {
        var start = new DateTime(1900, 1, 1);
        var range = (DateTime.Today - start).Days;           
        var randomDate = start.AddDays(rnd.Next(range));
        var formatDate = randomDate.ToString("ddMMyy");
        var endDigits = rnd.Next(2000, 9000);
        
        return gender switch
        {
            "male" => $"{formatDate}-{(endDigits / 2)*2 + 1}",
            "female" => $"{formatDate}-{(endDigits / 2)*2}",
            _ => "Wrong input"
        };
    }

    public Person DoBFullnameAndGender()
    {
        var person = NameAndGender();
        var cpr = CprNumber(person.Gender);
        var doB = cpr.Split("-");

        return new Person
        {
            Name = person.Name,
            Surname = person.Surname,
            Gender = person.Gender,
            DoB = doB[0]
        };

    }
    
    public Person DoBCPRFullnameAndGender()
    {
        var person = NameAndGender();
        var cpr = CprNumber(person.Gender);
        var doB = cpr.Split("-");

        return new Person
        {
            Name = person.Name,
            Surname = person.Surname,
            Gender = person.Gender,
            DoB = doB[0],
            Cpr = cpr
        };
    }
    
    public Person CPRFullnameAndGender()
    {
        var person = NameAndGender();
        var cpr = CprNumber(person.Gender);

        return new Person
        {
            Name = person.Name,
            Surname = person.Surname,
            Gender = person.Gender,
            Cpr = cpr
        };
    }

    public static void Main()
    {
        var randomPerson = new RandomPerson();
        Console.WriteLine("Return a fake CPR: " + randomPerson.CprNumber("male"));
        Console.WriteLine("Return a fake full name and gender: " + randomPerson.NameAndGender());
        Console.WriteLine("Return a fake full name, gender and DoB: " + randomPerson.DoBFullnameAndGender());
        Console.WriteLine("Return a fake full name, gender, DoB and CPR: " + randomPerson.DoBCPRFullnameAndGender());
        Console.WriteLine("Return a fake full name, gender and CPR: " + randomPerson.CPRFullnameAndGender());
        Console.WriteLine("Return a fake mobile phone number: " + randomPerson.PhoneNumber());
        Console.WriteLine("Return a fake address: " + randomPerson.Address());
        Console.WriteLine("Return all information for a fake person: " + randomPerson.OnePerson());
        Console.WriteLine("Return fake person in bulk (5): " + randomPerson.BulkPerson(5) + " - Length of array: " + randomPerson.BulkPerson(5).Length);
    }
}