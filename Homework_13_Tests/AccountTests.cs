using Homework_13.Model;
using NUnit.Framework;
using System;

namespace Homework_13_Tests
{
    public class AccountTests
    {
        private Account senderAc;
        private Account recipientAc;

        [SetUp]
        public void setUp()
        {
            senderAc = new Account()
            {
                Id = 1,
                ClientId = 1,
                CreateDate = DateTime.Now,
                Balance = 1000
            };

            recipientAc = new Account
            {
                Id = 2,
                ClientId = 2,
                CreateDate = DateTime.Now,
                Balance = 1000
            };
        }

        [Test]
        public void SendTo_Normal_Test()
        {
            bool actualIsDone = senderAc.SendTo(recipientAc, 100);

            Assert.IsTrue(actualIsDone, $"Перевод не выполнен, ожидалось что будет выполнен");
            Assert.AreEqual(900, senderAc.Balance, $"Ожидался балансу отправителя 900, получен {senderAc.Balance}");
            Assert.AreEqual(1100, recipientAc.Balance, $"Ожидался балансу получателя 1100, получен {senderAc.Balance}");
        }

        [Test]
        public void SendTo_Negative_Test()
        {
            bool actualIsDone = senderAc.SendTo(recipientAc, -100);

            Assert.IsFalse(actualIsDone, $"Перевод выполнен, ожидалось что не будет выполнен");
            Assert.AreEqual(1000, senderAc.Balance);
            Assert.AreEqual(1000, recipientAc.Balance);
        }

        [Test]
        public void SendTo_MoreThan_Test()
        {
            bool actualIsDone = senderAc.SendTo(recipientAc, 2000);

            Assert.IsFalse(actualIsDone, $"Перевод выполнен, ожидалось что не будет выполнен");
            Assert.AreEqual(1000, senderAc.Balance);
            Assert.AreEqual(1000, recipientAc.Balance);
        }

        [Test]
        public void SendTo_Zero_Test()
        {
            bool actualIsDone = senderAc.SendTo(recipientAc, 0);

            Assert.IsFalse(actualIsDone, $"Перевод выполнен, ожидалось что не будет выполнен");
            Assert.AreEqual(1000, senderAc.Balance);
            Assert.AreEqual(1000, recipientAc.Balance);
        }
    }
}
