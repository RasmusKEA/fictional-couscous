using NUnit.Framework;
using RandomPerson;

namespace PersonTest;

public class Tests
{

    [Test]
    public void PhoneNumberIs8Digits()
    {
        //Arrange
        var randomPerson = new RandomPerson.RandomPerson();
        
        //Act
        var phoneNumber = randomPerson.PhoneNumber();

        //Assert
        Assert.AreEqual(8, phoneNumber.Length);
    }
}