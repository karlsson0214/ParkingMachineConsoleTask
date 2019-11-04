using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ParkingMachineConsole;

namespace UnitTestParkingMachine
{
    /// <summary>
    /// Test for the TicketMachine class.
    /// </summary>
    [TestClass]
    public class ParkingMachineTest
    {
        [TestMethod]
        public void ValidInsertMoney()
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
        public void InvalidInsertMoney()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(-30);

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
        }
        [TestMethod]
        public void Cancel()
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
        [TestMethod]
        public void BuyTicket30Min_CurrentTotal_Zeroed()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(10);
            machine.BuyTicket();

            // Assert
            Assert.AreEqual(0, machine.CurrentTotal);
        }
        [TestMethod]
        public void BuyTicketTwice_Total_Summed()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(10);
            machine.BuyTicket();
            machine.InsertMoney(40);
            machine.BuyTicket();

            // Assert
            Assert.AreEqual(50, machine.Total);
        }
        [TestMethod]
        public void BuyTicket30Min_TicketText()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(10);
            string ticketText = machine.BuyTicket();

            // Assert
            Assert.AreEqual(TimeToTicketText(days: 0, hours: 0, minutes: 30), ticketText);
        }
        [TestMethod]
        public void BuyTicket3Hour_TicketText()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(60);
            string ticketText = machine.BuyTicket();

            // Assert
            Assert.AreEqual(TimeToTicketText(days: 0, hours: 3, minutes: 0), ticketText);
        }
        [TestMethod]
        public void BuyTicket4Day_TicketText()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();

            // Act
            machine.InsertMoney(20 * 24 * 4);
            string ticketText = machine.BuyTicket();

            // Assert
            Assert.AreEqual(TimeToTicketText(days: 4, hours: 0, minutes: 0), ticketText);
        }
        [TestMethod]
        public void BuyTicket2Day3Hour15Min_TicketText()
        {
            // Arrange
            ParkingMachine machine = new ParkingMachine();
            int money = 2 * 24 * 20 + 3 * 20 + 5;

            // Act
            machine.InsertMoney(money);
            string ticketText = machine.BuyTicket();

            // Assert
            Assert.AreEqual(TimeToTicketText(days: 2, hours: 3, minutes: 15), ticketText);
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
