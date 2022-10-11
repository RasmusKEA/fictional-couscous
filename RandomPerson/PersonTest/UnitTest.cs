using System;

namespace PersonTest;
using NUnit.Framework;
using RandomPerson;

public class UnitTest
{
    [Test]
    public void PhoneNumberIs8Digits()
    {
        //Arrange
        var randomPerson = new RandomPerson();
        
        //Act
        var phoneNumber = randomPerson.PhoneNumber();

        //Assert
        Assert.AreEqual(8, phoneNumber.Length);
    }

    [Test]
    public void ZipCodeIsFourDigits()
    {
        //Arrange
        var randomPerson = new RandomPerson();

        //Act
        var address = randomPerson.Address();
        var arr = address.Split(" ");
        var zipCode = arr[4];
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(4, zipCode.Length);
            Assert.IsInstanceOf(typeof(int), int.Parse(zipCode));
        });
    }

    [Test]
    public void DoBIsEqualToCpr()
    {
        //Arrange
        var randomPerson = new RandomPerson();
        //Act
        var person = randomPerson.DoBCPRFullnameAndGender();
        var cpr = person.Cpr.Split("-");

        //Assert
        Assert.AreEqual(person.DoB, cpr[0]);
    }

    [Test]
    public void CprMatchesGender()
    {
        //Arrange
        var randomPerson = new RandomPerson();
        

        //Act
        var maleCpr = randomPerson.CprNumber("male").Split("-");
        var femaleCpr = randomPerson.CprNumber("female").Split("-");
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.AreEqual(1, int.Parse(maleCpr[1]) % 2);
            Assert.AreEqual(0, int.Parse(femaleCpr[1]) % 2);
        });
    }
    
    
    


}