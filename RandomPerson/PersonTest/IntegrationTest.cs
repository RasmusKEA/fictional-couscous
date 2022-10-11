namespace PersonTest;
using NUnit.Framework;
using RandomPerson;

public class IntegrationTest
{
    [Test]
    public void FileSystemTest()
    {
        //Arrange
        var randomPerson = new RandomPerson();
        
        //Act
        var person = randomPerson.NameAndGender();

        //Assert
        Assert.IsInstanceOf(typeof(Person), person);
    }

    [Test]
    public void DbTest()
    {
        //Arrange
        var randomPerson = new RandomPerson();
        
        //Act
        var address = randomPerson.Address();
        
        //Assert
        Assert.That(address, Is.TypeOf(typeof(string)));
    }
}