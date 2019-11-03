using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TicketMachineConsole;

namespace UnitTestTicketMachine
{
    /// <summary>
    /// Test for the TicketMachine class.
    /// </summary>
    [TestClass]
    public class TicketMachineTest
    {
        [TestMethod]
        public void ValidInsertMoneyTest()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(30);
            machine.InsertMoney(60);

            // Assert
            Assert.AreEqual(90, machine.CurrentTotal);
        }
        [TestMethod]
        public void InvalidInsertMoneyTest()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(-30);

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
        }

        [TestMethod]
        public void BuyTicket30MinTest()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(10);
            string ticketText = machine.BuyTicket();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
            Assert.AreEqual(10, machine.Total);
            Assert.AreEqual(TimeToTicketText(days: 0, hours: 0, minutes: 30), ticketText);
        }
        [TestMethod]
        public void BuyTicket3HourTest()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(60);
            string ticketText = machine.BuyTicket();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
            Assert.AreEqual(60, machine.Total);
            Assert.AreEqual(TimeToTicketText(days: 0, hours: 3, minutes: 0), ticketText);
        }
        [TestMethod]
        public void BuyTicket4DayTest()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(20 * 24 * 4);
            string ticketText = machine.BuyTicket();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
            Assert.AreEqual(20 * 24 * 4, machine.Total);
            Assert.AreEqual(TimeToTicketText(days: 4, hours: 0, minutes: 0), ticketText);
        }
        [TestMethod]
        public void BuyTicket2Day3Hour15MinTest()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();
            int money = 2 * 24 * 20 + 3 * 20 + 5;

            // Act
            machine.InsertMoney(money);
            string ticketText = machine.BuyTicket();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
            Assert.AreEqual(money, machine.Total);
            Assert.AreEqual(TimeToTicketText(days: 2, hours: 3, minutes: 15), ticketText);
        }
        [TestMethod]
        public void MultipleBuyTicketTest()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();
            int money = 2 * 24 * 20 + 3 * 20 + 5;

            // Act
            machine.InsertMoney(money);
            machine.BuyTicket();
            machine.InsertMoney(money);
            machine.BuyTicket();
            machine.InsertMoney(money);
            machine.BuyTicket();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
            Assert.AreEqual(3 * money, machine.Total);
        }
        [TestMethod]
        public void CancelTest()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(100);
            int refund = machine.Cancel();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
            Assert.AreEqual(100, refund);

        }
        private string TimeToTicketText(int days, int hours, int minutes)
        {
            return "Parking ticket valid for:" + Environment.NewLine +
                days + " days" + Environment.NewLine +
                hours + " hours" + Environment.NewLine +
                minutes + " minutes";
        }
    }
}
